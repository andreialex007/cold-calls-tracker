using System.ComponentModel.DataAnnotations;
using ColdCallsTracker.Code.Data.ViewModels._Common;

namespace ColdCallsTracker.Code.Data.ViewModels
{
    public class PhoneEditItem : ViewModelBase
    {
        [Required]
        public string Number { get; set; }

        [Required]
        public string Remarks { get; set; }
        public int CompanyId { get; set; }

    }
}