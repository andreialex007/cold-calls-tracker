using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ColdCallsTracker.Code.Data.Models
{
    public class QuoteTemplate : EntityBase
    {
        public string Name { get; set; }

        public bool CustomDesign { get; set; }

        public List<QuoteTemplateCostingTemplate> CostingTemplates { get; set; }
    }


    public class QuoteTemplateCostingTemplate
    {
        public int QuoteTemplateId { get; set; }
        public QuoteTemplate QuoteTemplate { get; set; }

        public int CostingTemplateId { get; set; }
        public CostingTemplate CostingTemplate { get; set; }
    }
}
