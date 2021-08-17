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
    public class YearController : ControllerBase
    {
        private readonly ILogger<BrandController> _logger;
        private readonly IDataScraper _scraper;
        public YearController(ILogger<BrandController> logger, IDataScraper scraper)
        {
            _logger = logger;
            _scraper = scraper;
        }


        [HttpGet]
        public IEnumerable<ISectionData> Get()
        {
            return _scraper.ScrapData("yearfrom");
        }

        [HttpGet("From")]
        public IEnumerable<ISectionData> GetFrom()
        {
            return _scraper.ScrapData("yearfrom");
        }

        [HttpGet("To")]
        public IEnumerable<ISectionData> GetTo()
        {
            return _scraper.ScrapData("yearto");
        }
    }
}
