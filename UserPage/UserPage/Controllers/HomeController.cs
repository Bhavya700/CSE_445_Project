using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Xml;
using UserPage.Models;

namespace UserPage.Controllers
{
    public class HomeController : Controller
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        private string XmlFilePathMembers => Server.MapPath("~/App_Data/Members.xml");
        private string XmlFilePathAdmin => Server.MapPath("~/App_Data/Admins.xml");
        public async Task<ActionResult> Index(string coinName = "bitcoin", string currency = "usd")
        {

            string username = Session["username"] as string;
            string jsonData = await GetCryptoPrice(coinName, currency);

            if (!string.IsNullOrEmpty(jsonData))
            {
                SaveJson(jsonData, "crypto_price.json");
                ConvertJsonToXml("crypto_price.json", "crypto_price.xml");
            }

            var model = new UserViewModel
            {
                Username = username,
                CoinDataJson = jsonData,
                CoinName = coinName,
                Currency = currency
            };

            return View(model);
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

            // Wrap the array into an object for valid XML conversion
            if (json.TrimStart().StartsWith("["))
            {
                json = $"{{ \"Prices\": {json} }}";
            }

            XmlDocument doc = JsonConvert.DeserializeXmlNode(json, "CryptoData");
            doc.Save(xmlPath);
        }




        //Code for Bhavya's Page
        public ActionResult SignOut()
        {
            //clearing session here, but could clear user state as well
            Session.Clear(); 
            return RedirectToAction("LoginView");
        }
        public ActionResult LoginView()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserViewModel model)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(XmlFilePathMembers);

            foreach (XmlNode node in doc.SelectNodes("/Users/User"))
            {
                string storedUsername = node.SelectSingleNode("Username").InnerText;
                string storedPassword = node.SelectSingleNode("Password").InnerText;

                if (model.Username == storedUsername && model.Password == storedPassword)
                {
                    // Redirect to Index after successful login
                    Session["Username"] = storedUsername;
                    return RedirectToAction("Index");
                }
            }

            ViewBag.Message = "Invalid username or password.";
            return View("LoginView");
        }

        public ActionResult SignupView()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Signup(UserViewModel model)
        {
            XmlDocument doc = new XmlDocument();

            if (System.IO.File.Exists(XmlFilePathMembers))
            {
                doc.Load(XmlFilePathMembers);
            }
            else
            {
                XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", null, null);
                doc.AppendChild(dec);
                XmlElement root = doc.CreateElement("Users");
                doc.AppendChild(root);
            }

            XmlElement newUser = doc.CreateElement("User");

            XmlElement userElem = doc.CreateElement("Username");
            userElem.InnerText = model.Username;

            XmlElement passElem = doc.CreateElement("Password");
            passElem.InnerText = model.Password;

            newUser.AppendChild(userElem);
            newUser.AppendChild(passElem);

            doc.DocumentElement.AppendChild(newUser);
            doc.Save(XmlFilePathMembers);

            ViewBag.Message = "Account created successfully!";
            return View("SignupView");
        }
        //End of Bhavya's Code

        //Igris's Code:

    }
}