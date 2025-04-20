using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace CSE_445_Project
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            XmlDocument adminDoc = new XmlDocument();
            adminDoc.Load(Server.MapPath("~/App_Data/Admins.xml"));

            XmlNode adminNode = adminDoc.SelectSingleNode($"/Admins/Admin[Username='{username}' and Password='{password}']");
            if (adminNode != null)
            {
                Session["User"] = username;
                Response.Redirect("Staff.aspx");
            }
            else
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(Server.MapPath("~/App_Data/Members.xml"));

                XmlNode userNode = doc.SelectSingleNode($"/Users/User[Username='{username}' and Password='{password}']");
                if (userNode != null)
                {
                    Session["User"] = username;
                    Response.Redirect("Member.aspx");
                }
                else
                {
                    lblMessage.Text = "Invalid username or password!";
                }
            }

            
        }

        protected void btnSignup_Click(object sender, EventArgs e)
        {

        }
    }
}