using System.ComponentModel.DataAnnotations;
using ColdCallsTracker.Code.Data.Models;
using ColdCallsTracker.Code.Data.ViewModels._Common;
using ColdCallsTracker.Code.Utils;

namespace ColdCallsTracker.Code.Data.ViewModels
{
    public class CostingItem : ViewModelBase
    {
        [Required]
        public string Name { get; set; }

        public int Unit { get; set; }
        public double Qty { get; set; }
        public double? Cost { get; set; }
        public double Multiplier { get; set; }
        public double? Total { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName => ((CostingCategoryEnum)CategoryId).DescriptionAttr();

        public int QuoteId { get; set; }
        public QuoteItem Quote { get; set; }

        public double MultiplierTotal => (this.Total ?? 0) * this.Multiplier;
    }
}
