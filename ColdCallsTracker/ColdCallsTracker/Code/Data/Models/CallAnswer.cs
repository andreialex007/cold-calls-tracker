using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ColdCallsTracker.Code.Data.Models
{
    public class CallAnswer : EntityBase
    {
        public string Text { get; set; }

        [ForeignKey("FromQuestion")]
        public int? FromQuestionId { get; set; }
        public CallQuestion FromQuestion { get; set; }

        [ForeignKey("ToQuestion")]
        public int? ToQuestionId { get; set; }
        public CallQuestion ToQuestion { get; set; }

    }
}