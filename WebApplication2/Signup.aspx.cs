using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace CSE_445_Project
{
    public partial class Signup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnCreateAccount_Click(object sender, EventArgs e)
        {
            string username = txtNewUsername.Text;
            string password = txtNewPassword.Text;

            string filePath = Server.MapPath("~/App_Data/Members.xml");
            XmlDocument doc = new XmlDocument();

            try
            {
                doc.Load(filePath);
            }
            catch
            {
                XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", null, null);
                doc.AppendChild(dec);
                XmlElement root = doc.CreateElement("Users");
                doc.AppendChild(root);
            }

            XmlElement newUser = doc.CreateElement("User");

            XmlElement userElem = doc.CreateElement("Username");
            userElem.InnerText = username;

            XmlElement passElem = doc.CreateElement("Password");
            passElem.InnerText = password;

            newUser.AppendChild(userElem);
            newUser.AppendChild(passElem);

            doc.DocumentElement.AppendChild(newUser);
            doc.Save(filePath);

            lblSignupMsg.Text = "Account created successfully!";
        }
    }
}