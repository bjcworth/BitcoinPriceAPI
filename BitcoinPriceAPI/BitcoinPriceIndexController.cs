using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.Extensions.Configuration;

namespace BitcoinPriceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BitcoinPriceIndexController : ControllerBase
    {
        private static BitcoinPriceIndex _prices;
        private static IConfiguration Configuration;

        public BitcoinPriceIndexController(IConfiguration configuration)
        {
            _prices = new BitcoinPriceIndex { };
            Configuration = configuration;

        }

        [HttpGet]
        public async Task<BitcoinPriceIndex> GetPricesAsync()
        {
            HttpClient client = new HttpClient();

            string url = Configuration["CoinDeskApiUrl"];

            using (HttpResponseMessage response = await client.GetAsync(url))
            {
                using HttpContent content = response.Content;

                string pricesString = await content.ReadAsStringAsync();

                JObject pricesJson = JObject.Parse(pricesString);

                _prices.USD = pricesJson["bpi"]["USD"]["rate"].ToString();
                _prices.GBP = pricesJson["bpi"]["GBP"]["rate"].ToString();
                _prices.EUR = pricesJson["bpi"]["EUR"]["rate"].ToString();
                _prices.LastUpdated = pricesJson["time"]["updated"].ToString();
                _prices.Disclaimer = pricesJson["disclaimer"].ToString();
            } 
            return _prices;
        }
    }
}
