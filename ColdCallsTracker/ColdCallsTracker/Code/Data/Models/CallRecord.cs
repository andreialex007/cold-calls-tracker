using System;

namespace ColdCallsTracker.Code.Data.Models
{
    public class CallRecord : EntityBase
    { 
        public string Content { get; set; }
        public DateTime Date { get; set; }


        public int PhoneId { get; set; }
        public Phone Phone { get; set; }
    }
}