using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Configuration;
using Microsoft.Extensions.Options;
using CRAutosAPI.Configuration;
using CRAutosAPI.Models;
using HtmlAgilityPack;
using Newtonsoft.Json;
using System.Text;
using System.IO;
using System.Web;

namespace CRAutosAPI.Scrapers
{
    public class CRAutosDataScraper : IDataScraper
    {

        private readonly IOptions<CRAutos> _config;

        public CRAutosDataScraper(IOptions<CRAutos> config)
        {
            _config = config;
        }

        public string GetSite(string siteUrl)
        {
            
            WebClient crAutosWebClient = new WebClient();            
            return crAutosWebClient.DownloadString(siteUrl);            
        }

        public List<ISectionData> ScrapData(string sectionName)
        {
            string usedCarsUrl = _config.Value.UsedCarsUrl;
            string siteHtml = GetSite(usedCarsUrl);

            return ExtractData(siteHtml, sectionName);

            //return JsonConvert.SerializeObject(sectionList, Newtonsoft.Json.Formatting.Indented);
        }

        public List<ISectionData> ExtractData(string html, string sectionName)
        {
            var dataSet = new List<ISectionData>();
            var doc = new HtmlDocument();
            StringWriter dataWriter = new StringWriter();
            doc.LoadHtml(html);
            var formNode = doc.DocumentNode.SelectNodes(String.Format("//select[@name='{0}']", sectionName));
            foreach (var item in formNode.First().ChildNodes)
            {
                if (item.Name == "option")
                {
                    dataWriter.GetStringBuilder().Clear();
                    HttpUtility.HtmlDecode(item.InnerText, dataWriter);
                    dataSet.Add(new SiteElement() { Id = Int32.Parse(item.Attributes.First().Value),Value= dataWriter.ToString() }) ;
                }
            }
            return dataSet;
        }

    }
}
