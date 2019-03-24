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

        public string LastCallRecordDateFrom { get; set; }
        public string LastCallRecordDateTo { get; set; }
    }
}