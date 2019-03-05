using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ColdCallsTracker.Code.Data.ViewModels._Common;

namespace ColdCallsTracker.Code.Data.ViewModels
{
    public class SystemSettingsItem : ViewModelBase
    {
        [Required]
        public string Code { get; set; }
        [Required]
        public string Value { get; set; }
    }
}
