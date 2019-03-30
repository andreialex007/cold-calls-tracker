using System.Collections.Generic;
using ColdCallsTracker.Code.Data.Models;
using ColdCallsTracker.Code.Data.ViewModels._Common;

namespace ColdCallsTracker.Code.Data.ViewModels
{
    public class CallScriptItem : ViewModelBase
    {
        public string Name { get; set; }

        public List<CallQuestionItem> CallQuestions { get; set; } = new List<CallQuestionItem>();
    }

    public class CallQuestionItem : ViewModelBase
    {
        public string Text { get; set; }

        public int CallScriptId { get; set; }
        public CallScriptItem CallScript { get; set; }

        public List<CallAnswerItem> CallAnswers { get; set; } = new List<CallAnswerItem>();
        public List<CallAnswerItem> FromCallAnswers { get; set; } = new List<CallAnswerItem>();

    }

    public class CallAnswerItem : ViewModelBase
    {
        public string Text { get; set; }

        public int? FromQuestionId { get; set; }
        public CallQuestion FromQuestion { get; set; }

        public int? ToQuestionId { get; set; }
        public CallQuestion ToQuestion { get; set; }

    }
}
