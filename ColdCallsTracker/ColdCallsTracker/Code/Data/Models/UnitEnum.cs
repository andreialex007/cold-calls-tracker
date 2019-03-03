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

    public enum CostingCategoryEnum
    {
        [Description("UI")]
        Ui = 1,

        [Description("Интеграция")]
        Integration = 2,

        [Description("Прочее")]
        Other = 3
    }
}