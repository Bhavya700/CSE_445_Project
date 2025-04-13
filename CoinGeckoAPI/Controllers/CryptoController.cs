using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CoinGeckoAPI.Services;
using System.Collections.Generic;

namespace CoinGeckoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CryptoController : ControllerBase
    {
        private readonly CryptoService _cryptoService;

        public CryptoController(CryptoService cryptoService)
        {
            _cryptoService = cryptoService;
        }

        [HttpGet("price")] //HTTP GET 
        public async Task<ActionResult<List<Dictionary<string, object>>>> GetCryptoPrice( 
            [FromQuery] string cryptoId = "bitcoin", //set a generic default of bitcoin and usd currency 
            [FromQuery] string currency = "usd")
        {
            var priceData = await _cryptoService.GetCryptoPriceAsync(cryptoId, currency); //call to get info on our entry data, wait for response

            if (priceData == null || priceData.Count == 0) //check if it's null and say it's invalid 
            {
                return NotFound(new { error = "Invalid request or cryptocurrency not found." });
            }

            return Ok(priceData); //return whatever information you received from the coinGecko API
        }
    }
}
