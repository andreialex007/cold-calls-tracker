using System;
using System.Collections.Generic;

namespace ColdCallsTracker.Code.Data.Models
{
    public class Company : EntityBase
    {
        public string Name { get; set; }
        public string ActivityType { get; set; }
        public string Address { get; set; }
        public string WebSites { get; set; }
        public string Remarks { get; set; }

        public List<Phone> Phones { get; set; } = new List<Phone>();
        public List<Quote> Quotes { get; set; } = new List<Quote>();

        public int? StateId { get; set; }
        
    }
}
