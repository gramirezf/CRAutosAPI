using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRAutosAPI.Models
{
    public class SearchResult
    {
        public string CarId { get; set; }
        public string ResultTitle { get; set; }
        public string DefaultPictureLink { get; set; }
        public List<CarAttribute> Attributes { get; set; }
        public string DetailsLink { get; set; }
        public string PriceColones { get; set; }
    }
}
