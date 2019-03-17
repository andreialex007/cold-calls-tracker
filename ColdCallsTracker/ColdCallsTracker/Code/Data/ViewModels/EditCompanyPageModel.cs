using System.Collections.Generic;

namespace ColdCallsTracker.Code.Data.ViewModels
{
    public class EditCompanyPageModel
    {
        public List<QuoteTemplateItem> QuoteTemplates { get; set; } = new List<QuoteTemplateItem>();
        public List<CostingTemplateItem> CostingTemplates { get; set; } = new List<CostingTemplateItem>();
    }
}
