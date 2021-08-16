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
        private readonly IOptions<CRAutos> _config;
        private readonly IScraper _brandScraper;
        public YearController(IOptions<CRAutos> config, ILogger<BrandController> logger, IScraper brandScraper)
        {
            _logger = logger;
            _config = config;
            _brandScraper = brandScraper;
        }


        [HttpGet]
        public IEnumerable<ISectionData> Get()
        {
            return _brandScraper.ScrapData("yearfrom");
        }

        [HttpGet("From")]
        public IEnumerable<ISectionData> GetFrom()
        {
            return _brandScraper.ScrapData("yearfrom");
        }

        [HttpGet("To")]
        public IEnumerable<ISectionData> GetTo()
        {
            return _brandScraper.ScrapData("yearto");
        }
    }
}
