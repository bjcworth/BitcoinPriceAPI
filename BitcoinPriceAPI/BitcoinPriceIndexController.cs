using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;

using System.Net.Http;

namespace BitcoinPriceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BitcoinPriceIndexController : ControllerBase
    {
        private static BitcoinPriceIndex _prices;

        static BitcoinPriceIndexController()
        {
            _prices = new BitcoinPriceIndex { };

        }

        [HttpGet]
        public async Task<BitcoinPriceIndex> GetPricesAsync()
        {
            HttpClient client = new HttpClient();

            string url = "https://api.coindesk.com/v1/bpi/currentprice.json";

            string pricesString = "";


            using (HttpResponseMessage response = await client.GetAsync(url))
            {
                using(HttpContent content = response.Content)
                {
                    pricesString = await content.ReadAsStringAsync();

                    JObject pricesJson = JObject.Parse(pricesString);

                    _prices.USD = pricesJson["bpi"]["USD"]["rate"].ToString();
                    _prices.GBP = pricesJson["bpi"]["GBP"]["rate"].ToString();
                    _prices.EUR = pricesJson["bpi"]["EUR"]["rate"].ToString();
                    _prices.LastUpdated = pricesJson["time"]["updated"].ToString();
                    _prices.Disclaimer = pricesJson["disclaimer"].ToString();
                } 
            } 
            return _prices;
        }
    }
}
