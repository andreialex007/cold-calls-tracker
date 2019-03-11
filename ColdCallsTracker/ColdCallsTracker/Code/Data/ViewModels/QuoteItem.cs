using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ColdCallsTracker.Code.Data.ViewModels._Common;

namespace ColdCallsTracker.Code.Data.ViewModels
{
    public class QuoteItem : ViewModelBase
    {
        [Required]
        public string Name { get; set; }

        public int? CompanyId { get; set; }
        public string CompanyName { get; set; }
        public CompanyEditItem Company { get; set; }

        public List<CostingItem> Costings { get; set; } = new List<CostingItem>();
        public bool Opened { get; set; }
    }
}
