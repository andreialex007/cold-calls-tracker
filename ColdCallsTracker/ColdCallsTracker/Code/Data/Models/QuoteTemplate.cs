using System.Collections.Generic;

namespace ColdCallsTracker.Code.Data.Models
{
    public class QuoteTemplate : EntityBase
    {
        public string Name { get; set; }

        public List<QuoteTemplateCostingTemplate> CostingTemplates { get; set; }
    }
}
