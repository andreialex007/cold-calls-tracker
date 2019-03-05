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
}
