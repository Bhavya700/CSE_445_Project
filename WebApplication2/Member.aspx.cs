using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Xml;
using Newtonsoft.Json;
using System.Net.Http;

namespace WebApplication2
{
    public partial class Member : System.Web.UI.Page
    {
        private static readonly HttpClient _httpClient = new HttpClient();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string username = Session["username"] as string;
                lblUsername.Text = username ?? "User";
                lblUserCorner.Text = username ?? "User";
            }
        }

        protected void btnSignOut_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("Login.aspx");
        }

        protected async void btnSearch_Click(object sender, EventArgs e)
        {
            string crypto = txtCryptoName.Text.Trim().ToLower();
            string currency = txtCurrency.Text.Trim().ToLower();

            if (string.IsNullOrEmpty(crypto) || string.IsNullOrEmpty(currency))
            {
                litResult.Text = "<p style='color:orange;'>Please enter both Crypto Name and Currency.</p>";
                return;
            }

            string jsonData = await GetCryptoPrice(crypto, currency);

            if (!string.IsNullOrEmpty(jsonData))
            {
                SaveJson(jsonData, "crypto_price.json");
                ConvertJsonToXml("crypto_price.json", "crypto_price.xml");

                // Display XML on page
                string xmlPath = Server.MapPath("~/App_Data/crypto_price.xml");
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlPath);
                string formattedXml = FormatXml(xmlDoc.OuterXml); // replace xmlResultString with your actual XML string
                litResult.Text = "<pre>" + Server.HtmlEncode(formattedXml) + "</pre>";

            }
            else
            {
                litResult.Text = "<p style='color:red;'>Error fetching data from API.</p>";
            }
        }

        private async Task<string> GetCryptoPrice(string coinName, string currency)
        {
            try
            {
                string apiUrl = $"https://localhost:7012/api/Crypto/price?cryptoId={coinName}&currency={currency}";
                var response = await _httpClient.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
            catch
            {
                return null;
            }
        }

        private void SaveJson(string json, string filename)
        {
            string path = Server.MapPath($"~/App_Data/{filename}");
            System.IO.File.WriteAllText(path, json);
        }

        private void ConvertJsonToXml(string jsonFilename, string xmlFilename)
        {
            string jsonPath = Server.MapPath($"~/App_Data/{jsonFilename}");
            string xmlPath = Server.MapPath($"~/App_Data/{xmlFilename}");
            string json = System.IO.File.ReadAllText(jsonPath);

            // Wrap the array if needed for valid XML
            if (json.TrimStart().StartsWith("["))
            {
                json = $"{{ \"Prices\": {json} }}";
            }

            XmlDocument doc = JsonConvert.DeserializeXmlNode(json, "CryptoData");
            doc.Save(xmlPath);
        }

        //Helper function for XML reading / displaying
        private string FormatXml(string xml)
        {
            try
            {
                var doc = new XmlDocument();
                doc.LoadXml(xml);

                using (var stringWriter = new StringWriter())
                using (var xmlTextWriter = new XmlTextWriter(stringWriter))
                {
                    xmlTextWriter.Formatting = System.Xml.Formatting.Indented;
                    doc.WriteContentTo(xmlTextWriter);
                    xmlTextWriter.Flush();
                    return stringWriter.GetStringBuilder().ToString();
                }
            }
            catch
            {
                return xml; // fallback to raw XML if formatting fails
            }
        }
    }
}
