using System;

namespace ColdCallsTracker.Code.Data.ViewModels
{
    public class CompanySearchParameters : SearchParametersBase
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string ActivityType { get; set; }
        public string WebSites { get; set; }
        public string Remarks { get; set; }

        public int? StateId { get; set; }

        public string PhoneNumbers { get; set; }

        public DateTime? LastCallRecordDateFrom { get; set; }
        public DateTime? LastCallRecordDateTo { get; set; }
    }

    public class QuoteSearchParameters : SearchParametersBase
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public int? CompanyId { get; set; }
    }
}