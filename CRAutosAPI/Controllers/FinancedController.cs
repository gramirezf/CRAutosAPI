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
    public class FinancedController : ControllerBase
    {
        private readonly ILogger<FinancedController> _logger;
        private readonly IOptions<CRAutos> _config;
        private readonly IScraper _brandScraper;
        public FinancedController(IOptions<CRAutos> config, ILogger<FinancedController> logger, IScraper brandScraper)
        {
            _logger = logger;
            _config = config;
            _brandScraper = brandScraper;
        }

        [HttpGet]
        public IEnumerable<ISectionData> Get()
        {
            return _brandScraper.ScrapData("financed");
        }
    }
}
