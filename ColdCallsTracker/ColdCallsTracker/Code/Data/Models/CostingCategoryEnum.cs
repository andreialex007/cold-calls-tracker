using System.ComponentModel;

namespace ColdCallsTracker.Code.Data.Models
{
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