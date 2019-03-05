using System.ComponentModel;

namespace ColdCallsTracker.Code.Data.Models
{
    public enum UnitEnum
    {
        [Description("Часы")]
        Hours = 1,

        [Description("Штуки")]
        Items = 2
    }
}