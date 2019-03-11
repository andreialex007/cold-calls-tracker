using System.Collections.Generic;
using ColdCallsTracker.Code.Data.ViewModels._Common;

namespace ColdCallsTracker.Code.Data.ViewModels
{
    public class CompanyEditItem : ViewModelBase
    {
        public string Name { get; set; }
        public string ActivityType { get; set; }
        public string WebSites { get; set; }
        public string Remarks { get; set; }

        public List<PhoneEditItem> Phones { get; set; } = new List<PhoneEditItem>();
        public List<CallRecordItem> Records { get; set; } = new List<CallRecordItem>();
        public List<QuoteItem> Quotes { get; set; } = new List<QuoteItem>();

        public int? StateId { get; set; }
        public string State { get; set; }


    }
}