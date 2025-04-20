using System;
using System.Collections.Concurrent;


public static class LoginLimiter
{
    private const int MAX_ATTEMPTS = 5;
    private static readonly TimeSpan WINDOW = TimeSpan.FromMinutes(2);
    private static readonly TimeSpan LOCK_TIME = TimeSpan.FromMinutes(2);

    private class Info
    {
        public int FailCount;
        public DateTime FirstFailUtc;
        public DateTime? LockedUntilUtc;
    }

    // Thread‑safe map: username → info
    private static readonly ConcurrentDictionary<string, Info> _state =
        new ConcurrentDictionary<string, Info>(StringComparer.OrdinalIgnoreCase);

    /// <summary>returns null if allowed; otherwise a human message</summary>
    public static string Check(string username)
    {
        if (!_state.TryGetValue(username, out var info))
            return null;

        // Already locked?
        if (info.LockedUntilUtc is DateTime until && until > DateTime.UtcNow)
            return $"Account locked until {until.ToLocalTime():t}";

        return null;
    }

    /// <summary>Call AFTER a failed password.</summary>
    public static void RecordFailure(string username)
    {
        var now = DateTime.UtcNow;
        var i = _state.GetOrAdd(username, _ => new Info { FirstFailUtc = now });

        // slide the window
        if (now - i.FirstFailUtc > WINDOW)
        {
            i.FirstFailUtc = now;
            i.FailCount = 0;
        }

        if (++i.FailCount >= MAX_ATTEMPTS)
        {
            i.LockedUntilUtc = now + LOCK_TIME;
            i.FailCount = 0;               // reset for next window
        }
    }
    // *** how many tries still available ***
    public static int AttemptsLeft(string username)
    {
        if (!_state.TryGetValue(username, out var info))
            return MAX_ATTEMPTS;                  // user hasn’t failed yet

        if (info.LockedUntilUtc is DateTime until && until > DateTime.UtcNow)
            return 0;                             // locked

        return Math.Max(0, MAX_ATTEMPTS - info.FailCount);
    }
    /// <summary>Call on successful login to clear counters.</summary>
    public static void Clear(string username) => _state.TryRemove(username, out _);
}