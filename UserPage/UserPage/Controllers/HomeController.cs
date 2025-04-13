using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using CoinGeckoAPI.Models;
using CoinGeckoAPI.Services;
using System.Threading.Tasks;

namespace UserPage.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var username = "Crypto";

            var jsonData = awaitCoinGeckoService.GetCoinDataAsync("bitcoin"); //fix this later
            CoinGeckoService.Savejson(jsonData, "bitcoin.json"); //fix this later
            CoinGeckoService.ConvertJsonToXml("bitcoin.json", "bitcoin.xml"); //fix this later
            var model = new UserModel
            {
                username = username,
                CoinDataJson = jsonData
            };
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}