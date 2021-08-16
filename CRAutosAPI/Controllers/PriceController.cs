using CRAutosAPI.Configuration;
using CRAutosAPI.Models;
using CRAutosAPI.Scrapers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRAutosAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PriceController : ControllerBase
    {
        private readonly ILogger<PriceController> _logger;
        private readonly IOptions<CRAutos> _config;
        private readonly IScraper _brandScraper;
        public PriceController(IOptions<CRAutos> config, ILogger<PriceController> logger, IScraper brandScraper)
        {
            _logger = logger;
            _config = config;
            _brandScraper = brandScraper;
        }


        [HttpGet]
        public IEnumerable<ISectionData> Get()
        {
            return _brandScraper.ScrapData("pricefrom");
        }

        [HttpGet("From")]
        public IEnumerable<ISectionData> GetFrom()
        {
            return _brandScraper.ScrapData("pricefrom");
        }

        [HttpGet("To")]
        public IEnumerable<ISectionData> GetTo()
        {
            return _brandScraper.ScrapData("priceto");
        }
    }
}
