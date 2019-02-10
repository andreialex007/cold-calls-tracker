using System;

namespace ColdCallsTracker.Code.Data
{
    public class CallRecord
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }


        public int PhoneId { get; set; }
        public Phone Phone { get; set; }
    }
}