using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace SecurityUtils.Services
{

    public class PasswordHasher : ICryptoService
    {
     
        public string Hash(string plainText)
        {

            byte[] salt = new byte[HashingOptions.SaltSize];
            using (var rng = new RNGCryptoServiceProvider())
                rng.GetBytes(salt);

       
            var pbkdf2 = new Rfc2898DeriveBytes(
                plainText,
                salt,
                HashingOptions.Iterations);

            byte[] subkey = pbkdf2.GetBytes(HashingOptions.KeySize);

            byte[] outputBytes = new byte[salt.Length + subkey.Length];
            Buffer.BlockCopy(salt, 0, outputBytes, 0, salt.Length);
            Buffer.BlockCopy(subkey, 0, outputBytes, salt.Length, subkey.Length);
            return Convert.ToBase64String(outputBytes);
        }

        public bool Verify(string plainText, string hashedValue)
        {
            
            byte[] decoded = Convert.FromBase64String(hashedValue);

            int saltSize = HashingOptions.SaltSize;
            int subkeySize = HashingOptions.KeySize;
            if (decoded.Length != saltSize + subkeySize)
                return false;

            byte[] salt = new byte[saltSize];
            Buffer.BlockCopy(decoded, 0, salt, 0, saltSize);

            byte[] storedSubkey = new byte[subkeySize];
            Buffer.BlockCopy(decoded, saltSize, storedSubkey, 0, subkeySize);

            var pbkdf2 = new Rfc2898DeriveBytes(
                plainText,
                salt,
                HashingOptions.Iterations);

            byte[] generatedSubkey = pbkdf2.GetBytes(subkeySize);

            for (int i = 0; i < subkeySize; i++)
                if (storedSubkey[i] != generatedSubkey[i])
                    return false;

            return true;
        }
    }
}
