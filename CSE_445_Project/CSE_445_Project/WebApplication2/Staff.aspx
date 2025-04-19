﻿<%@ Page Language="C#" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <title>Staff Page – UI Mock‑up</title>

    <style>
        body       { font-family: Segoe UI, Arial, sans-serif; margin:40px; }
        h2         { margin-bottom:20px; }
        table      { border-collapse: collapse; width: 100%; margin-bottom:25px; }
        th, td     { border: 1px solid #ccc; padding: 8px; text-align: left; }
        th         { background: #f0f0f0; }
        label      { display:inline-block; width:90px; margin-right:10px; }
        input      { padding:6px; margin-right:15px; }
        button     { padding:6px 14px; }
        #msg       { color:red; margin-left:10px; }
    </style>
</head>
<body>
    <h2>Staff Maintenance (Mock‑up)</h2>

    <!-- Static grid with sample data -->
    <table>
        <thead>
            <tr>
                <th>Username</th>
                <th>Password (stored)</th>
            </tr>
        </thead>
        <tbody>
            <tr><td>TA</td><td>Cse445!</td></tr>
            <tr><td>admin</td><td>********</td></tr>
        </tbody>
    </table>

    <!-- Add‑staff form (no functionality yet) -->
    <form>
        <label for="user">Username:</label>
        <input id="user" type="text" placeholder="e.g. jsmith" />

        <label for="pwd">Password:</label>
        <input id="pwd" type="password" placeholder="initial pwd" />

        <button type="button">Add Staff</button>
        <span id="msg"><!-- status message goes here --></span>
    </form>
</body>
</html>