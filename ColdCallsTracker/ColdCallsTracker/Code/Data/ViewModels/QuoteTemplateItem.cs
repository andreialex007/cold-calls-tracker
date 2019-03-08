using System.Collections.Generic;
using System.Linq;
using ColdCallsTracker.Code.Data.Models;
using ColdCallsTracker.Code.Data.ViewModels._Common;
using ColdCallsTracker.Code.Extensions;
using ColdCallsTracker.Code.Utils;

namespace ColdCallsTracker.Code.Data.ViewModels
{
    public class QuoteTemplateItem : ViewModelBase
    {
        public string Name { get; set; }

        public List<CostingTemplateItem> AvaliableCostingTemplates { get; set; } = new List<CostingTemplateItem>();
        public List<QuoteTemplateCostingTemplate> QuoteCostingRelations { get; set; } = new List<QuoteTemplateCostingTemplate>();

        public bool CustomDesign { get; set; }

        public double CustomDesignTotal { get; set; }

        public void Recalc()
        {
            this.AvaliableCostingTemplates.CalcTotalForCostingTemplates();
            var total = this.Total;
        }

        public double Total
        {
            get
            {
                var costingTemplates = this.QuoteCostingRelations.Select(x => new CostingTemplateItem(x.CostingTemplate)).ToList();

                costingTemplates.CalcTotalForCostingTemplates();
                var withMultiplier = costingTemplates
                    .Select(x => new
                    {
                        item = x,
                        Multiplier =
                            QuoteCostingRelations.FirstOrDefault(c => c.CostingTemplateId == x.Id)?.Multiplier ?? 1
                    });
                var totalPrice = withMultiplier.Sum(x => (x.item.Total ?? 0) * x.Multiplier);

                var uiTotal = withMultiplier.Where(x => x.item.CategoryId == (int)CostingCategoryEnum.Ui).Sum(x => (x.item.Total ?? 0) * x.Multiplier);

                if (this.CustomDesign)
                {
                    var customDesignTotal = ((GlobalVariables.CustomDesignMarkup) * uiTotal);
                    this.CustomDesignTotal = customDesignTotal;
                    totalPrice += customDesignTotal;
                };

                return totalPrice;
            }
        }
    }
}
