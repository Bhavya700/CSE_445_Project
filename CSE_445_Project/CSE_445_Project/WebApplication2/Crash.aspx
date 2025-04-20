<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Crash.aspx.cs" Inherits="WebApplication2.Crash" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <title>Something Went Wrong</title>
    <style> body { font-family:Segoe UI, sans-serif; padding:40px; } h2 { color:#c00; } </style>
</head>
<body>
    <h2>We're sorry—an error occurred.</h2>
    <asp:Label ID="lblError" runat="server" ForeColor="DarkRed" />
    <p>Please <a href="Login.aspx">return to the login page</a> or try again later.</p>
</body>
</html>
