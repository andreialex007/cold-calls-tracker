using System.Collections.Generic;

namespace ColdCallsTracker.Code.Data
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ActivityType { get; set; }
        public string WebSites { get; set; }
        public string Remarks { get; set; }

        public List<Phone> Phones { get; set; } = new List<Phone>();

        public int? StateId { get; set; }
        public State State { get; set; }
    }
}
