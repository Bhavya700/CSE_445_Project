using System;
using System.IO;
using System.Web;
using System.Web.Routing;
using System.Web.Optimization;
using System.Xml.Linq;

namespace WebApplication2
{
    public class Global : HttpApplication
    {
        // lock to prevent concurrent file writes
        private static readonly object _logLock = new object();
        // event handler for counting active users is added by Bhavya Patel
        void Application_Start(object sender, EventArgs e)
        {
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            RouteTable.Routes.MapPageRoute(
                "CrashTest", "crash-test", "~/Crash.aspx"
            );
            Application["ActiveUsers"] = 0;
        }
        protected void Session_Start(object sender, EventArgs e)
        {
            Application.Lock();
            Application["ActiveUsers"] = ((int)Application["ActiveUsers"]) + 1;
            Application.UnLock();
        }
        void Application_Error(object sender, EventArgs e)
        {
            try
            {
                Exception ex = Server.GetLastError();
                string path = Server.MapPath("~/App_Data/errorlog.xml");

                lock (_logLock)
                {
                    XDocument doc;
                    if (File.Exists(path))
                    {
                        doc = XDocument.Load(path);
                    }
                    else
                    {
                        doc = new XDocument(new XElement("Errors"));
                    }

                    var errorElem = new XElement("Error",
                        new XAttribute("Time", DateTime.UtcNow.ToString("o")),
                        new XAttribute("Url", Request.Url.ToString()),
                        new XElement("Message", ex.Message),
                        new XElement("StackTrace", ex.StackTrace ?? String.Empty)
                    );

                    doc.Root.Add(errorElem);
                    doc.Save(path);
                }
            }
            catch
            {
                // if logging fails, swallow to avoid throwing in Application_Error
            }
        }
    }
}
