using System.Collections.Generic;

namespace ColdCallsTracker.Code.Data.Models
{
    public class Phone
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string Remarks { get; set; }


        public int CompanyId { get; set; }
        public Company Company { get; set; }

        public List<CallRecord> CallRecords { get; set; } = new List<CallRecord>();
    }
}