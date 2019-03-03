using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ColdCallsTracker.Code.Data.Models;

namespace ColdCallsTracker.Code.Data.ViewModels
{
    public class CompanyListItem
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string ActivityType { get; set; }
        public string WebSites { get; set; }
        public string Remarks { get; set; }

        public int? StateId { get; set; }
        public string State { get; set; }

        public string PhoneNumbers => string.Join(";", PhoneNumbersList);
        public List<string> PhoneNumbersList { get; set; } = new List<string>();

        public DateTime LastCallRecordDate { get; set; }
        public string LastCallRecordDateStr => LastCallRecordDate.ToString("dd.MM.yyyy");
    }
}
