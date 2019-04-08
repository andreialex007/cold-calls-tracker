using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using ColdCallsTracker.Code.Extensions;
using ColdCallsTracker.Code.Utils;

namespace ColdCallsTracker.Code.Data.ViewModels
{
    public class CompanyListItem
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string Address { get; set; }

        [Required]
        public string ActivityType { get; set; }
        public string WebSites { get; set; }

        public string WebSitesLinks
        {
            get
            {
                if (!WebSites.HasValue())
                    return string.Empty;

                var sites = WebSites.Split(" ");
                var links = sites.Select(x => string.Format("<a target='blank' href='{0}' >{0}</a>", x)).ToList();

                return string.Join(" ", links);
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

        public string Remarks { get; set; }

        public int? StateId { get; set; }
        public string State
        {
            get
            {
                if (StateId == null)
                    return null;
                return ((CompanyStateEnum)StateId).DescriptionAttr();
            }
        }

        public string PhoneNumbers => string.Join(";", PhoneNumbersList);
        public List<string> PhoneNumbersList { get; set; } = new List<string>();

        public DateTime LastCallRecordDate { get; set; }
        public string LastCallRecordDateStr => LastCallRecordDate.ToString("dd.MM.yyyy HH:mm:ss");
    }
}
