﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="CSE_445_Project.Login" %>

<!DOCTYPE html>

<html xmlns="https://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login - CryptoBro42</title>
</head>
<body>
    <h2>Login</h2>
    <form runat="server">
        <div>
            Username: <asp:TextBox ID="txtUsername" runat="server" />
        </div>
        <div>
           Password: <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" />
        </div>
        <div>
            <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" />
            <asp:Button ID="btnSignup" runat="server" Text="Sign Up" PostBackUrl="~/Signup.aspx" OnClick="btnSignup_Click" />
        </div>
        <div style="color:red;">
            <asp:Label ID="lblMessage" runat="server" />
        </div>
    </form>
</body>
</html>