using System.Collections.Generic;

namespace ColdCallsTracker.Code.Data.Models
{
    public class CallScript : EntityBase
    {
        public string Name { get; set; }
        public List<CallQuestion> CallQuestions { get; set; }
    }
}
