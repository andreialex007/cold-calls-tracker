using System.Collections.Generic;

namespace ColdCallsTracker.Code.Data.Models
{
    public class QuoteTemplate : EntityBase
    {
        public string Name { get; set; }

        public List<QuoteTemplateCostingTemplate> CostingTemplates { get; set; }
    }

    public class QuoteTemplateCostingTemplate
    {
        public int CostingTemplateId { get; set; }
        public CostingTemplate CostingTemplate { get; set; }

        public int QuoteTemplateId { get; set; }
        public QuoteTemplate QuoteTemplate { get; set; }
    }
}
