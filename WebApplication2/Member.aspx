<%@ Page Language="C#" Async="true" AutoEventWireup="true" CodeBehind="Member.aspx.cs" Inherits="WebApplication2.Member" %>


<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>IBG Crypto Search - Member Area</title>
    <style>
        body {
            font-family: Arial;
            margin: 0;
            padding: 0;
        }

        header {
            background-color: #1e1e1e;
            color: white;
            padding: 20px;
            display: flex;
            justify-content: space-between;
            align-items: center;
        }

        .username {
            font-size: 14px;
        }

        .signout {
            margin-left: 10px;
            color: #ffcccc;
            text-decoration: none;
        }

        .container {
            padding: 30px;
        }

        label {
            display: inline-block;
            width: 120px;
            font-weight: bold;
        }

        input[type="text"] {
            padding: 5px;
            width: 200px;
            margin-bottom: 10px;
        }

        .search-btn {
            margin-top: 10px;
            padding: 7px 15px;
            background-color: #007acc;
            color: white;
            border: none;
            cursor: pointer;
        }

        .result {
            margin-top: 20px;
            padding: 10px;
            background-color: #f4f4f4;
            border: 1px solid #ccc;
        }

        footer {
            text-align: center;
            padding: 20px;
            margin-top: 40px;
            background-color: #efefef;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">

        <!-- HEADER -->
        <header>
            <h2>Welcome to IBG Crypto Search, <asp:Label ID="lblUsername" runat="server" Text="User" />!</h2>
            <div class="username">
                <asp:Label ID="lblUserCorner" runat="server" Text="User" /> |
                <asp:LinkButton ID="btnSignOut" runat="server" CssClass="signout" OnClick="btnSignOut_Click">Sign out</asp:LinkButton>
            </div>
        </header>

        <!-- CONTENT AREA -->
        <div class="container">
            <h3>Search Crypto</h3>

            <asp:Label runat="server" AssociatedControlID="txtCryptoName">Crypto Name:</asp:Label>
            <asp:TextBox ID="txtCryptoName" runat="server" /><br />

            <asp:Label runat="server" AssociatedControlID="txtCurrency">Currency:</asp:Label>
            <asp:TextBox ID="txtCurrency" runat="server" /><br />

            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="search-btn" OnClick="btnSearch_Click" />

            <!-- XML Result -->
            <div class="result">
                <asp:Literal ID="litResult" runat="server"></asp:Literal>
            </div>
        </div>

        <!-- FOOTER -->
        <footer>
            <p>- Made by Gavin Fiedler</p>
        </footer>

    </form>
</body>
</html>
