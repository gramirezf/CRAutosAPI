using CRAutosAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRAutosAPI.Scrapers
{
    public interface IDataScraper
    {
        string GetSite(string sectionUrl);
        List<ISectionData> ExtractData(string html, string dataName);
        public List<ISectionData> ScrapData(string sectionName);

    }
}
