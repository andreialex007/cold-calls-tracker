using System.ComponentModel.DataAnnotations;
using ColdCallsTracker.Code.Data.Models;
using ColdCallsTracker.Code.Data.ViewModels._Common;

namespace ColdCallsTracker.Code.Data.ViewModels
{
    public class CostingTemplateItem : ViewModelBase
    {
        public CostingTemplateItem()
        {

        }

        public CostingTemplateItem(CostingTemplate template)
        {
            this.Id = template.Id;
            this.Name = template.Name;
            this.Unit = template.Unit;
            this.CategoryId = template.CategoryId;
            this.Qty = template.Qty;
            this.Cost = template.Cost;
            this.Total = template.Total;
        }

        [Required]
        public string Name { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]

        public int Unit { get; set; }
        public UnitEnum UnitEnum => (UnitEnum)Unit;
        public string UnitName => Utils.EnumUtil.DescriptionAttr(UnitEnum);

        public int CategoryId { get; set; }
        public CostingCategoryEnum Category => (CostingCategoryEnum)CategoryId;
        public string CategoryName => Utils.EnumUtil.DescriptionAttr(Category);

        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        public double Qty { get; set; }
        public double? Cost { get; set; }
        public double? Total { get; set; }
    }
}
