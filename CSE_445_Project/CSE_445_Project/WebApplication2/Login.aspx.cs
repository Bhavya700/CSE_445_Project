using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using WebApplication2.Services;


namespace CSE_445_Project
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            // 1. Read credentials from textboxes
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            // 2. Check admin/staff credentials (still using plain XML for staff)
            var adminDoc = new XmlDocument();
            adminDoc.Load(Server.MapPath("~/App_Data/Admins.xml"));
            var adminNode = adminDoc.SelectSingleNode(
                $"/Admins/Admin[Username='{username}' and Password='{password}']");

            if (adminNode != null)
            {
                // Staff authenticated
                Session["User"] = username;
                FormsAuthentication.SetAuthCookie(username, false);
                Response.Redirect("Staff.aspx");
                return;
            }

            // 3. Authenticate member using AuthService and hashed passwords
            var authService = new AuthService();
            bool isMemberValid = authService.Authenticate(username, password);

            if (isMemberValid)
            {
                Session["User"] = username;
                FormsAuthentication.SetAuthCookie(username, false);
                Response.Redirect("Member.aspx");
            }
            else
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "Invalid username or password!";
            }

        }

        protected void btnSignup_Click(object sender, EventArgs e)
        {
            Response.Redirect("Signup.aspx");
        }
    }
}

