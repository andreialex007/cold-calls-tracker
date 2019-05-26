using System;
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
        public string Description { get; set; }

        public List<CostingTemplateItem> AvaliableCostingTemplates { get; set; } = new List<CostingTemplateItem>();
        public List<QuoteTemplateCostingTemplate> QuoteCostingRelations { get; set; } = new List<QuoteTemplateCostingTemplate>();

        public bool CustomDesign { get; set; }

        public double CustomDesignTotal { get; set; }

        public double TotalHoursWithFixes => TotalHours + (TotalHours * (GlobalVariables.FixesPercentage / 100));

        public double TotalHours
        {
            get
            {
                var totalHours = this.QuoteCostingRelations
                    .Where(x => x.CostingTemplate.Unit == (int)UnitEnum.Hours)
                    .Sum(x => x.Multiplier * x.CostingTemplate.Qty);

                if (CustomDesign)
                {
                    var uiHours = this.QuoteCostingRelations
                        .Where(x => x.CostingTemplate.Unit == (int)UnitEnum.Hours)
                        .Where(x => x.CostingTemplate.CategoryId == (int)CostingCategoryEnum.Category1)
                        .Sum(x => x.Multiplier * x.CostingTemplate.Qty);


                    var designHours = (uiHours * GlobalVariables.ExtraMarkup);
                    totalHours += designHours;
                }

                return Math.Round(totalHours);
            }
        }

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

                var uiTotal = withMultiplier.Where(x => x.item.CategoryId == (int)CostingCategoryEnum.Category1).Sum(x => (x.item.Total ?? 0) * x.Multiplier);

                if (this.CustomDesign)
                {
                    var customDesignTotal = ((GlobalVariables.ExtraMarkup) * uiTotal);
                    this.CustomDesignTotal = customDesignTotal;
                    totalPrice += customDesignTotal;
                };

                return totalPrice;
            }
        }
    }
}
