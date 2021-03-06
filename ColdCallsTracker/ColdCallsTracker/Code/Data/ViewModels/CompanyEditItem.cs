﻿using System.Collections.Generic;
using ColdCallsTracker.Code.Data.ViewModels._Common;
using ColdCallsTracker.Code.Utils;

namespace ColdCallsTracker.Code.Data.ViewModels
{
    public class CompanyEditItem : ViewModelBase
    {
        public string Name { get; set; }
        public string ActivityType { get; set; }
        public string Address { get; set; }
        public string WebSites { get; set; }
        public string Remarks { get; set; }

        public List<PhoneEditItem> Phones { get; set; } = new List<PhoneEditItem>();
        public List<CallRecordItem> Records { get; set; } = new List<CallRecordItem>();
        public List<QuoteItem> Quotes { get; set; } = new List<QuoteItem>();

        public int? StateId { get; set; }
        public string State
        {
            get
            {
                if (StateId == null)
                    return string.Empty;
                return ((CompanyStateEnum)StateId).DescriptionAttr();
            }
        }

        public string SearchLink
        {
            get
            {
                var searchLink = "https://www.google.com/search?q=";
                searchLink += System.Net.WebUtility.UrlEncode($" {this.Name} {this.Address}");
                return searchLink;
            }
        }



    }
}