using System.Collections.Generic;

namespace ColdCallsTracker.Code.Data.Models
{
    public class Quote : EntityBase
    {
        public string Name { get; set; }

        public int? CompanyId { get; set; }
        public Company Company { get; set; }

        public bool CustomDesign { get; set; }

        public List<Costing> Costings { get; set; } = new List<Costing>();
    }
}
