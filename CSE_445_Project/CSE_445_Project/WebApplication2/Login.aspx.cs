using System;
using System.Xml;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

// *** limiter ***  (helper lives in LoginLimiter.cs)
using WebApplication2; 

using WebApplication2.Services;

namespace CSE_445_Project
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e) { }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            /* ---------- limiter pre‑check ---------- */
            // *** limiter ***
            string lockMsg = LoginLimiter.Check(username);
            if (lockMsg != null)
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = lockMsg;      // e.g. “Account locked until 3:27 PM”
                return;
            }
            /* -------------------------------------- */

            /* ---------- staff (Admins.xml) ---------- */
            var adminDoc = new XmlDocument();
            adminDoc.Load(Server.MapPath("~/App_Data/Admins.xml"));
            var adminNode = adminDoc.SelectSingleNode(
                $"/Admins/Admin[Username='{username}' and Password='{password}']");

            if (adminNode != null)
            {
                Session["User"] = username;
                FormsAuthentication.SetAuthCookie(username, false);

                // *** limiter ***  — reset counters on success
                LoginLimiter.Clear(username);

                Response.Redirect("Staff.aspx");
                return;
            }

            /* ---------- members via AuthService ---------- */
            var authService = new AuthService();
            bool isMemberValid = authService.Authenticate(username, password);

            if (isMemberValid)
            {
                Session["User"] = username;
                FormsAuthentication.SetAuthCookie(username, false);

                // *** limiter ***
                LoginLimiter.Clear(username);

                Response.Redirect("Member.aspx");
            }
            else
                // *** limiter ***
                LoginLimiter.RecordFailure(username);

            int left = LoginLimiter.AttemptsLeft(username);        // <‑‑ NEW

            lblMessage.ForeColor = System.Drawing.Color.Red;
            if (left == 0)
                lblMessage.Text = "Account locked for 15 minutes.";
            else
                lblMessage.Text = $"Invalid username or password!  ({left} attempt{(left == 1 ? "" : "s")} left)";
        }

        protected void btnSignup_Click(object sender, EventArgs e)
        {
            Response.Redirect("Signup.aspx");
        }
    }
}
