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
        public List<CostingTemplateItem> CostingTemplates { get; set; } = new List<CostingTemplateItem>();

        public bool CustomDesign { get; set; }

        public double Total
        {
            get
            {
                var uiTemplates = this.CostingTemplates
                    .Where(x => x.Category == CostingCategoryEnum.Ui)
                    .Where(x => x.Total == null)
                    .Where(x => x.Cost == null);

                var uiTotal = 0.0;
                foreach (var templateItem in uiTemplates)
                {
                    templateItem.Cost = GlobalVariables.AverageSalaryPerHour;
                    templateItem.Total = templateItem.Cost * templateItem.Qty;
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
