using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace CRAutosAPI.Models
{
    public class SearchRequest
    {

        public SearchRequest()
        {
            PageNumber = 1;
            YearFrom = 1960;
            YearTo = DateTime.Now.Year;
            PriceFrom = 100000;
            PriceTo = 200000000;
            Model = String.Empty;
        }

        public int PageNumber { get; set; }
        public int Brand { get; set; } //brand       
        public string Model { get; set; } //modelstr
        public int Style { get; set; }//style
        public int Fuel { get; set; } //fuel
        public int Transmition { get; set; }  //trans
        public int Financed { get; set; } //financed
        public int ReceiveCar { get; set; }//recibe
        public int Province { get; set; } //province
        public int Doors { get; set; } //doors
        public int YearFrom { get; set; } //yearfrom
        public int YearTo { get; set; } //yearto
        public int PriceFrom { get; set; } //pricefrom
        public int PriceTo { get; set; } //priceto
        public int OrderBy { get; set; } //orderby 
        public int NewOrUsed { get; set; } //newused
    }
}
