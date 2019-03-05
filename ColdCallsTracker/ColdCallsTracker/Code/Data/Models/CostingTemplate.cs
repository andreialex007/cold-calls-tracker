using System.Collections.Generic;

namespace ColdCallsTracker.Code.Data.Models
{
    public class CostingTemplate : EntityBase
    {
        public string Name { get; set; }

        public int Unit { get; set; }
        public double Qty { get; set; }
        public double? Cost { get; set; }
        public double? Total { get; set; }

        public int CategoryId { get; set; }

        public List<QuoteTemplateCostingTemplate> QuoteTemplates { get; set; }

    }
}