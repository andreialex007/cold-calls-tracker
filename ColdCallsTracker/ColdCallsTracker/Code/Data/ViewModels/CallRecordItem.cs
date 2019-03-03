using System.ComponentModel.DataAnnotations;
using ColdCallsTracker.Code.Data.ViewModels._Common;

namespace ColdCallsTracker.Code.Data.ViewModels
{
    public class CallRecordItem : ViewModelBase
    {
        [Required]
        public string Content { get; set; }
        public int PhoneId { get; set; }
        public string Phone { get; set; }
    }
}