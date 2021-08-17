﻿using CRAutosAPI.Configuration;
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
    public class OrderByController : ControllerBase
    {
        private readonly ILogger<OrderByController> _logger;
        private readonly IDataScraper _scraper;
        public OrderByController(ILogger<OrderByController> logger, IDataScraper scraper)
        {
            _logger = logger;
            _scraper = scraper;
        }

        [HttpGet]
        public IEnumerable<ISectionData> Get()
        {
            return _scraper.ScrapData("orderby");
        }
    }
}
