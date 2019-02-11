using System;
using System.Collections.Generic;

namespace ColdCallsTracker.Code.Data.ViewModels
{
    public class CompanyListItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ActivityType { get; set; }
        public string WebSites { get; set; }
        public string Remarks { get; set; }

        public int? StateId { get; set; }
        public string State { get; set; }

        public string PhoneNumbers => string.Join(";", PhoneNumbersList);
        public List<string> PhoneNumbersList { get; set; } = new List<string>();

        public DateTime LastCallRecordDate { get; set; }
    }

    public class CompanySearchParameters : SearchParametersBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ActivityType { get; set; }
        public string WebSites { get; set; }
        public string Remarks { get; set; }

        public int? StateId { get; set; }

        public string PhoneNumbers { get; set; }

        public DateTime LastCallRecordDateFrom { get; set; }
        public DateTime LastCallRecordDateTo { get; set; }
    }

    public class SearchParametersBase
    {
        public string OrderBy { get; set; }
        public bool IsAsc { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
    }
}
