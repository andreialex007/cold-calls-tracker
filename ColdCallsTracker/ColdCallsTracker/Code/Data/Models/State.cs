using System.Collections.Generic;

namespace ColdCallsTracker.Code.Data.Models
{
    public class State
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Company> Companies { get; set; } = new List<Company>();
    }
}