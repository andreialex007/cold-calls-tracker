using System.ComponentModel;

namespace ColdCallsTracker.Code.Data.Models
{
    public enum CostingCategoryEnum
    {
        [Description("UI (Верстка, Фронтэнд, Бэкэнд)")]
        Ui = 1,

        [Description("Интеграция")]
        Integration = 2,

        [Description("Прочее")]
        Other = 3,

        [Description("Дизайн")]
        Design = 4,

        [Description("Верстка")]
        Markup = 5,

        [Description("Бэкэнд")]
        Backend = 6,

        [Description("Фронтэнд")]
        Frontend = 7,
    }
}