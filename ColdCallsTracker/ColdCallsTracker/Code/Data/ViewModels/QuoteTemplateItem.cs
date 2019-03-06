using System.Collections.Generic;
using System.Linq;
using ColdCallsTracker.Code.Data.Models;
using ColdCallsTracker.Code.Data.ViewModels._Common;
using ColdCallsTracker.Code.Utils;

namespace ColdCallsTracker.Code.Data.ViewModels
{
    public class QuoteTemplateItem : ViewModelBase
    {
        public string Name { get; set; }

        public List<CostingTemplateItem> AvaliableCostingTemplates { get; set; } = new List<CostingTemplateItem>();
        public List<QuoteTemplateCostingTemplate> CostingTemplates { get; set; } = new List<QuoteTemplateCostingTemplate>();

        public bool CustomDesign { get; set; }

        public double Total
        {
            get
            {
                var uiTemplates = this.CostingTemplates
                    .Where(x => x.CostingTemplate.CategoryId == (int) CostingCategoryEnum.Ui)
                    .Where(x => x.CostingTemplate.Total == null)
                    .Where(x => x.CostingTemplate.Cost == null);

                var uiTotal = 0.0;
                foreach (var templateItem in uiTemplates)
                {
                    templateItem.CostingTemplate.Cost = GlobalVariables.AverageSalaryPerHour;
                    templateItem.CostingTemplate.Total = templateItem.CostingTemplate.Cost * templateItem.CostingTemplate.Qty;
                    uiTotal += (templateItem.Total ?? 0);
                }

                var totalPrice = this.CostingTemplates.Sum(x => x.Total ?? 0);
                if (this.CustomDesign)
                    totalPrice += ((GlobalVariables.CustomDesignMarkup + 1) * uiTotal);

                return totalPrice;
            }
        }
    }
}
