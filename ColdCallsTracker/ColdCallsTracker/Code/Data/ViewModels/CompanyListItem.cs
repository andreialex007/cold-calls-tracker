﻿using System;
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
}
