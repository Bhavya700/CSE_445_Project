using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace CoinGeckoAPI.Services
{
    public class CryptoService
    {
        private readonly string _apiUrl = "https://api.coingecko.com/api/v3/coins/markets"; //default API URL for coin gecko
        private readonly string _apiKey;
        private readonly HttpClient _httpClient;

        public CryptoService(IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            _apiKey = configuration["CoinGecko:ApiKey"]; // Get API key from appsettings.json (more secure than declaring it here)
        }

        public async Task<List<Dictionary<string, object>>> GetCryptoPriceAsync(string cryptoId, string currency) //call the API service and get information on the crypto in the currency you pass in
        {
            try
            {
                //API url created with passed in information.
                var url = $"{_apiUrl}?vs_currency={currency}&ids={cryptoId}&x_cg_demo_api_key={_apiKey}";

                // send the request and await response 
                var response = await _httpClient.GetStringAsync(url);

                // convert the JSON response from API into a list
                var priceData = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(response);

                //return price data 
                //should i try handling if price data is null/empty? works without it so maybe don't need to 
                return priceData; 
            }
            catch
            {
                return null; // Return null on failure
            }
        }
    }
}
