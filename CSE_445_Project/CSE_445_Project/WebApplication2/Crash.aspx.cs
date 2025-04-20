using System;
using System.Web.UI;

namespace WebApplication2
{
    public partial class Crash : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Grab the last exception 
            Exception ex = Server.GetLastError();

            if (ex != null)
            {
                // Show only the message 
                lblError.Text = Server.HtmlEncode(ex.Message);


                // Clear the error so ASP.NET doesn’t re‑throw it
                Server.ClearError();
            }
            else
            {
                lblError.Text = "No additional error information is available.";
            }
        }
    }
}
