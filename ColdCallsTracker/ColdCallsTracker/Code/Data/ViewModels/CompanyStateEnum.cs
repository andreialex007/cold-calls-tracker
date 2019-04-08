using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace ColdCallsTracker.Code.Data.ViewModels
{
    public enum CompanyStateEnum
    {
        [Description("Черновик")]
        Draft = 0,

        [Description("В процесе")]
        InProgress = 1,

        [Description("Успех")]
        Success = 2,

        [Description("Отказ")]
        Decline = 3,

        [Description("Неподходит")]
        NotSuitable = 4,
    }
}
