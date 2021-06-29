using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Net;

namespace BitcoinPriceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BitcoinPriceIndexController : ControllerBase
    {
        private static readonly BitcoinPriceIndex _prices;

        static BitcoinPriceIndexController()
        {
            _prices = new BitcoinPriceIndex { };

        }

        [HttpGet]
        public BitcoinPriceIndex GetPrices()
        {
            WebClient Client = new WebClient();
            string PricesString = Client.DownloadString("https://api.coindesk.com/v1/bpi/currentprice.json");
            JObject PricesObject = JObject.Parse(PricesString);

            _prices.USD = PricesObject["bpi"]["USD"]["rate"].ToString();
            _prices.GBP = PricesObject["bpi"]["GBP"]["rate"].ToString();
            _prices.EUR = PricesObject["bpi"]["EUR"]["rate"].ToString();
            _prices.LastUpdated = PricesObject["time"]["updated"].ToString();
            _prices.Disclaimer = PricesObject["disclaimer"].ToString();

            return (_prices);
        }
    }
}
