using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using SecurityUtils.Services;
using WebApplication2.Services;

namespace CSE_445_Project
{
    public partial class Signup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnCreateAccount_Click(object sender, EventArgs e)
        {
            // 1. Read input
            string email = txtNewUsername.Text.Trim();
            string password = txtNewPassword.Text;

            // 2. Call your AuthService
            var authService = new AuthService();
            bool success = authService.Register(email, password);

            // 3. Show result
            if (success)
            {
                lblSignupMsg.ForeColor = System.Drawing.Color.Green;
                lblSignupMsg.Text = "Account created successfully!";
                Response.Redirect("Login.aspx");
            }
            else
            {
                lblSignupMsg.ForeColor = System.Drawing.Color.Red;
                lblSignupMsg.Text = "That email is already registered.";
            }

        }
    }
}