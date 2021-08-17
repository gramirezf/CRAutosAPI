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
using System.Collections.Specialized;
using System.Text;
using System.IO;
using System.Web;
using System.Text.RegularExpressions;

namespace CRAutosAPI.Scrapers
{
    public class CRAutosSearchScraper
    {

        private readonly IOptions<CRAutos> _config;

        public CRAutosSearchScraper(IOptions<CRAutos> config)
        {
            _config = config;
        }

        public string GetSearchResult(SearchRequest carSearch)
        {            
            WebClient crAutosWebClient = new WebClient();
            string page = carSearch.PageNumber <= 0 ? "1" : carSearch.PageNumber.ToString();
            crAutosWebClient.QueryString = new NameValueCollection() { { "p", page } };
            NameValueCollection postData = new NameValueCollection(){
                { "brand",  carSearch.Brand.ToString() },
                { "modelstr", String.IsNullOrEmpty(carSearch.Model)?String.Empty:carSearch.Model},
                { "style", carSearch.Style.ToString() },
                { "fuel", carSearch.Fuel.ToString() },
                { "trans", carSearch.Transmition.ToString() },
                { "financed", carSearch.Financed.ToString() },
                { "recibe", carSearch.ReceiveCar.ToString() },
                { "province",carSearch.Province.ToString() },
                { "doors",carSearch.Doors.ToString() },
                { "yearfrom", carSearch.YearFrom < 1960 ? "1960" : carSearch.YearFrom.ToString() },
                { "yearto", ((carSearch.YearTo < carSearch.YearFrom) || 
                             (carSearch.YearTo < 1960)) ? DateTime.Now.Year.ToString() : carSearch.YearTo.ToString() },
                { "pricefrom", carSearch.PriceFrom < 100000 ? "100000" : carSearch.PriceFrom.ToString() },
                { "priceto", ((carSearch.PriceTo < carSearch.PriceFrom) ||
                              (carSearch.PriceTo < 100000))? "200000000" : carSearch.PriceTo.ToString() },
                { "orderby", carSearch.OrderBy.ToString() },
                { "newused", carSearch.NewOrUsed.ToString() },
                { "lformat", "0" },
                { "1", "1"}
            };

            return Encoding.UTF8.GetString(crAutosWebClient.UploadValues(String.Format("{0}",_config.Value.SearchCarUrl), postData));           
        }

        public List<SearchResult> ScrapData(SearchRequest search)
        {
            string usedCarsUrl = _config.Value.UsedCarsUrl;
            string resultHtml = GetSearchResult(search);
            return ExtractData(resultHtml);

            //return JsonConvert.SerializeObject(sectionList, Newtonsoft.Json.Formatting.Indented);
        }

        public List<SearchResult> ExtractData(string html)
        {
            var dataSet = new List<SearchResult>();
            SearchResult searchResult;
            List<CarAttribute> attributes;
            StringWriter dataWriter = new StringWriter();
            var doc = new HtmlDocument();            
            doc.LoadHtml(html);
            var resultNodes = doc.DocumentNode.SelectNodes(String.Format("//div[@class='{0}']", "inventory"));
            if (resultNodes != null)
            {
                foreach (var carNode in resultNodes)
                {
                    searchResult = new SearchResult();
                    attributes = new List<CarAttribute>();
                    searchResult.ResultTitle = carNode.SelectSingleNode(".//div[@class='title']").InnerText.TrimEnd();
                    searchResult.DefaultPictureLink = carNode.SelectSingleNode(".//iframe[@class='preview']").Attributes["src"].Value;
                    var optionsNode = carNode.SelectSingleNode(".//table[@class='options-primary']");
                    foreach (var option in optionsNode.ChildNodes)
                    {
                        if (option.Name == "tr")
                        {
                            if (!String.IsNullOrWhiteSpace(option.InnerText))
                            {
                                dataWriter.GetStringBuilder().Clear();
                                HttpUtility.HtmlDecode(Regex.Replace(option.InnerText, @"\s+", ""), dataWriter);
                                attributes.Add(new CarAttribute() { AttributeText = dataWriter.ToString() });
                            }
                        }

                    }
                    optionsNode = carNode.SelectSingleNode(".//table[@class='options-secondary']");
                    foreach (var option in optionsNode.ChildNodes)
                    {
                        if (option.Name == "tr")
                        {
                            if (!String.IsNullOrWhiteSpace(option.InnerText))
                            {
                                dataWriter.GetStringBuilder().Clear();
                                HttpUtility.HtmlDecode(Regex.Replace(option.InnerText, @"\s+", ""), dataWriter);
                                attributes.Add(new CarAttribute() { AttributeText = dataWriter.ToString() });
                            }
                        }

                    }
                    searchResult.Attributes = attributes;
                    dataWriter.GetStringBuilder().Clear();
                    HttpUtility.HtmlDecode(Regex.Replace(carNode.SelectSingleNode(".//div[@class='price']").InnerText, @"\s+", ""), dataWriter);
                    searchResult.PriceColones = dataWriter.ToString();
                    
                    dataSet.Add(searchResult);
                }
            }
            return dataSet;
        }

    }
}
