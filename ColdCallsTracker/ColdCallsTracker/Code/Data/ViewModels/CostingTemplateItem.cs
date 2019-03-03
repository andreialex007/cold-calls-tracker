using System.ComponentModel.DataAnnotations;
using ColdCallsTracker.Code.Data.Models;
using ColdCallsTracker.Code.Data.ViewModels._Common;

namespace ColdCallsTracker.Code.Data.ViewModels
{
    public class CostingTemplateItem : ViewModelBase
    {
        [Required]
        public string Name { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]

        public int Unit { get; set; }
        public UnitEnum UnitEnum => (UnitEnum)Unit;

        public int CategoryId { get; set; }
        public CostingCategoryEnum Category => (CostingCategoryEnum)CategoryId;

        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        public double Qty { get; set; }
        public double? Cost { get; set; }
        public double? Total { get; set; }
    }
}
