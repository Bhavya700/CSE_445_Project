<%@ Page Language="C#" AutoEventWireup="true" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Linq" %>
<%@ Import Namespace="System.Xml.Linq" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Staff Maintenance</title>
    <style>
        body       { font-family: Segoe UI, Arial, sans-serif; margin:40px; }
        h2         { margin-bottom:20px; }
        .grid      { width:100%; border-collapse:collapse; margin-bottom:25px; }
        .grid th, .grid td { border:1px solid #ccc; padding:8px; text-align:left; }
        .grid th    { background:#f0f0f0; }
        label       { display:inline-block; width:90px; margin-right:10px; }
        input       { padding:6px; margin-right:15px; }
        button      { padding:6px 14px; }
        #lblMsg     { margin-left:10px; }
    </style>
</head>
<body>
<form id="form1" runat="server">
    <h2>Staff Maintenance</h2>

    <!-- GridView with delete support -->
    <asp:GridView ID="gvAdmins" runat="server"
                  CssClass="grid"
                  AutoGenerateColumns="False"
                  DataKeyNames="Username"
                  OnRowDeleting="gvAdmins_RowDeleting">
        <Columns>
            <asp:BoundField    DataField="Username" HeaderText="Username" />
            <asp:BoundField    DataField="Password" HeaderText="Password (stored)" />
            <asp:CommandField ShowDeleteButton="True" />
        </Columns>
    </asp:GridView>

    <!-- add‑staff form -->
    <label for="txtUser">Username:</label>
    <asp:TextBox ID="txtUser" runat="server" />

    <label for="txtPwd">Password:</label>
    <asp:TextBox ID="txtPwd" runat="server" TextMode="Password" />

    <asp:Button ID="btnAdd" runat="server" Text="Add Staff" OnClick="btnAdd_Click" />
    <asp:Label  ID="lblMsg" runat="server" />

</form>

<script runat="server">
    /* ---------- helpers ---------- */
    string XmlPath => Server.MapPath("~/App_Data/Staff.xml");

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) BindGrid();
    }

    void BindGrid()
    {
        var dt = new DataTable();
        dt.Columns.Add("Username");
        dt.Columns.Add("Password");

        if (System.IO.File.Exists(XmlPath))
        {
            var xdoc = XDocument.Load(XmlPath);
            foreach (var adm in xdoc.Root.Elements("Admin"))
                dt.Rows.Add((string)adm.Element("Username"),
                             (string)adm.Element("Password"));
        }
        gvAdmins.DataSource = dt;
        gvAdmins.DataBind();
    }

    /* ---------- add staff ---------- */
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        lblMsg.ForeColor = System.Drawing.Color.Red;
        lblMsg.Text = "";

        var user = txtUser.Text.Trim();
        var pwd  = txtPwd.Text.Trim();
        if (user == "" || pwd == "")
        {
            lblMsg.Text = "Username and password required.";
            return;
        }

        var xdoc = System.IO.File.Exists(XmlPath)
                   ? XDocument.Load(XmlPath)
                   : new XDocument(new XElement("Admins"));

        bool exists = xdoc.Root.Elements("Admin")
                        .Any(a => (string)a.Element("Username") == user);
        if (exists)
        {
            lblMsg.Text = "Username already exists.";
            return;
        }

        xdoc.Root.Add(
            new XElement("Admin",
                new XElement("Username", user),
                new XElement("Password", pwd)));
        xdoc.Save(XmlPath);

        txtUser.Text = txtPwd.Text = "";
        BindGrid();
        lblMsg.ForeColor = System.Drawing.Color.Green;
        lblMsg.Text = $"Added '{user}'.";
    }

    /* ---------- delete staff ---------- */
    protected void gvAdmins_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        var user = gvAdmins.DataKeys[e.RowIndex].Value.ToString();

        var xdoc = XDocument.Load(XmlPath);
        var target = xdoc.Root.Elements("Admin")
                     .FirstOrDefault(a => (string)a.Element("Username") == user);
        if (target != null)
        {
            target.Remove();
            xdoc.Save(XmlPath);
        }

        BindGrid();
        lblMsg.ForeColor = System.Drawing.Color.Green;
        lblMsg.Text = $"Removed '{user}'.";
        e.Cancel = true;                 // skip GridView default delete
    }
</script>
</body>
</html>
<!-- Written by Virrajith -->