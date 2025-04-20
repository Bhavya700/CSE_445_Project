﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Signup.aspx.cs" Inherits="CSE_445_Project.Signup" %>

<!DOCTYPE html>

<html xmlns="https://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <h2>Sign Up</h2>
    <form runat="server">
        <div>
            Username: <asp:TextBox ID="txtNewUsername" runat="server" />
        </div>
        <div>
            Password: <asp:TextBox ID="txtNewPassword" runat="server" TextMode="Password" />
        </div>
        <div>
            <asp:Button ID="btnCreateAccount" runat="server" Text="Create Account" OnClick="btnCreateAccount_Click" />
        </div>
        <div style="color:green;">
            <asp:Label ID="lblSignupMsg" runat="server" />
        </div>
    </form>
</body>
</html>