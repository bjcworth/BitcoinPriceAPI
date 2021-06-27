using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BitcoinPriceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BitcoinPriceIndexController : ControllerBase
    {
        private static readonly BitcoinPriceIndex _prices;

        private readonly ILogger<BitcoinPriceIndexController> _logger;


        public BitcoinPriceIndexController(ILogger<BitcoinPriceIndexController> logger)
        {
            _logger = logger;
        }
        static BitcoinPriceIndexController()
        {
            _prices = new BitcoinPriceIndex
            {
                USD = "1,044.3345",
                GBP = "22,358.1918",
                EUR = "26,568.8183",
                LastUpdated = "Jun 27, 2021 16:23:00 UTC"
            };

        }

        [HttpGet]
        public BitcoinPriceIndex GetPrices()
        {
            return (_prices);
        }
    }
}
