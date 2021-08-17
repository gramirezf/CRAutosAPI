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
    public class SearchCarController : ControllerBase
    {
        private readonly ILogger<SearchCarController> _logger;
        private readonly IOptions<CRAutos> _config;
        private readonly CRAutosSearchScraper _scraper;
        public SearchCarController(IOptions<CRAutos> config, ILogger<SearchCarController> logger)
        {
            _logger = logger;
            _config = config;
            _scraper = new CRAutosSearchScraper(_config);
        }

        [HttpPost]
        public IEnumerable<SearchResult> Search(SearchRequest CarSearch)
        {
            return _scraper.ScrapData(CarSearch);
            
        }
    }
}
