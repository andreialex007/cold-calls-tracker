using System.Collections.Generic;
using ColdCallsTracker.Code.Data.ViewModels._Common;

namespace ColdCallsTracker.Code.Data.ViewModels
{
    public class QuoteTemplateItem : ViewModelBase
    {
        public string Name { get; set; }

        public List<CostingTemplateItem> CostingTemplates { get; set; }
    }
}
