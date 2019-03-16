using System.ComponentModel;

namespace ColdCallsTracker.Code.Data.Models
{
    public enum CostingCategoryEnum
    {
        [Description("UI (Верстка, Фронт, Бэк)")]
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

        [Description("Копирайтинг")]
        Copyrighting = 8,

        [Description("СЕО")]
        SEO = 9,

        [Description("Контекстная реклама")]
        ContextAd = 10,

        [Description("Тех. поддержка")]
        TechnicalSupport = 11,

        [Description("Информ. поддержка")]
        InformationSupport = 12
    }
}