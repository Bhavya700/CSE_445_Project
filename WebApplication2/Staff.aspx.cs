using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Web.Security;
using System.Xml.Linq;

namespace WebApplication3.Staff
{
    public partial class Staff : System.Web.UI.Page
    {
        private readonly string _xmlPath = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/Staff.xml");

        protected void Page_Load(object sender, EventArgs e)
        {
            // Enforce Forms‑auth; redirect if not logged in
            if (!Request.IsAuthenticated)
            {
                Response.Redirect("~/Account/Login.aspx?ReturnUrl=" + Server.UrlEncode(Request.RawUrl));
                return;
            }

            // Optional: only allow users that exist in Staff.xml
            if (!UserIsStaff(User.Identity.Name))
            {
                FormsAuthentication.SignOut();
                Response.Redirect("~/Account/Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                BindGrid();
            }
        }

        private bool UserIsStaff(string username)
        {
            if (!File.Exists(_xmlPath)) return false;
            var doc = XDocument.Load(_xmlPath);
            return doc.Root?.Elements("Staff")
                       .Any(s => (string)s.Attribute("username") == username) ?? false;
        }

        private void BindGrid()
        {
            var table = new DataTable();
            table.Columns.Add("Username");
            table.Columns.Add("Password");

            if (File.Exists(_xmlPath))
            {
                var doc = XDocument.Load(_xmlPath);
                foreach (var s in doc.Root.Elements("Staff"))
                {
                    table.Rows.Add((string)s.Attribute("username"), (string)s.Attribute("password"));
                }
            }

            gvStaff.DataSource = table;
            gvStaff.DataBind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            var user = txtUser.Text.Trim();
            var pwd = txtPwd.Text.Trim();

            if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(pwd))
            {
                lblMsg.Text = "Both fields required.";
                return;
            }

            // TODO: replace with hash/encrypt call in Assignment 6
            var doc = File.Exists(_xmlPath)
                ? XDocument.Load(_xmlPath)
                : new XDocument(new XElement("StaffMembers"));

            if (doc.Root.Elements("Staff")
                     .Any(s => (string)s.Attribute("username") == user))
            {
                lblMsg.Text = "User already exists.";
                return;
            }

            doc.Root.Add(new XElement("Staff",
                          new XAttribute("username", user),
                          new XAttribute("password", pwd)));
            doc.Save(_xmlPath);

            lblMsg.Text = "Added!";
            txtUser.Text = txtPwd.Text = "";
            BindGrid();
        }
    }
}