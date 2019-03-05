using System.Collections.Generic;
using System.Linq;
using ColdCallsTracker.Code.Data.Models;
using ColdCallsTracker.Code.Data.ViewModels._Common;

namespace ColdCallsTracker.Code.Data.ViewModels
{
    public class QuoteTemplateItem : ViewModelBase
    {
        public string Name { get; set; }

        public List<CostingTemplateItem> CostingTemplates { get; set; }

        public double Total
        {
            get
            {
                var uiTemplates = this.CostingTemplates
                    .Where(x => x.Category == CostingCategoryEnum.Ui)
                    .Where(x => x.Total == null);

                return 0;
            }
        }
    }
}
