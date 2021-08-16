using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRAutosAPI.Models
{
    public class SiteElement : ISectionData
    {
        public int Id { get; set; }
        public string Value { get; set; }
    }
}
