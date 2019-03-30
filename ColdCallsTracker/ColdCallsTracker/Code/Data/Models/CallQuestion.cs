using System.Collections.Generic;

namespace ColdCallsTracker.Code.Data.Models
{
    public class CallQuestion : EntityBase
    {
        public string Text { get; set; }

        public int CallScriptId { get; set; }
        public CallScript CallScript { get; set; }


        public List<CallAnswer> CallAnswers { get; set; } = new List<CallAnswer>();
        public List<CallAnswer> FromCallAnswers { get; set; } = new List<CallAnswer>();
    }
}