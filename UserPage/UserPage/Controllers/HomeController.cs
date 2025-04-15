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

        public async Task<ActionResult> Index(string coinName = "bitcoin", string currency = "usd")
        {
            string username = "CryptoBro42";

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

    }
}


