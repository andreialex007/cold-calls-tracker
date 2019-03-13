using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using ColdCallsTracker.Code.Data.Models;
using ColdCallsTracker.Code.Data.ViewModels._Common;
using ColdCallsTracker.Code.Utils;

namespace ColdCallsTracker.Code.Data.ViewModels
{
    public class QuoteItem : ViewModelBase
    {
        [Required]
        public string Name { get; set; }

        public int? CompanyId { get; set; }
        public string CompanyName { get; set; }
        public CompanyEditItem Company { get; set; }

        public bool CustomDesign { get; set; }

        public List<CostingItem> Costings { get; set; } = new List<CostingItem>();
        public bool Opened { get; set; }

        public double CustomDesignTotal
        {
            get
            {
                var uiTotal = Costings.Where(x => x.CategoryId == (int)CostingCategoryEnum.Ui).Sum(x => x.MultiplierTotal);
                if (this.CustomDesign)
                {
                    var customDesignTotal = ((GlobalVariables.CustomDesignMarkup) * uiTotal);
                    return customDesignTotal;
                }
                return 0;
            }
        }

        public double CustomDesignTotalHrs
        {
            get
            {
                var uiTotal = Costings.Where(x => x.CategoryId == (int)CostingCategoryEnum.Ui).Sum(x => x.Qty * x.Multiplier);
                if (this.CustomDesign)
                {
                    var customDesignTotal = ((GlobalVariables.CustomDesignMarkup) * uiTotal);
                    return customDesignTotal;
                }
                return 0;
            }
        }


        public double TotalHours
        {
            get
            {
                var hrs = Costings.Where(x=>x.Unit == (int) UnitEnum.Hours).Sum(x => x.Qty * x.Multiplier);
                return Math.Round(hrs + CustomDesignTotalHrs);
            }
        }


        public double Total => Costings.Sum(x => x.MultiplierTotal) + CustomDesignTotal;
    }
}
