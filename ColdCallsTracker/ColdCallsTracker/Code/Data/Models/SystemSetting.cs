using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ColdCallsTracker.Code.Data.Models
{
    public class SystemSetting : EntityBase
    {
        public string Code { get; set; }
        public string Value { get; set; }
    }
}
