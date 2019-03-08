namespace ColdCallsTracker.Code.Utils
{
    public class GlobalVariables
    {
        /// <summary>
        /// Количество отработанных часов в день
        /// </summary>
        public const int WorkingHoursPerDay = 6;

        /// <summary>
        /// Средняя ЗП по региону (тыс. руб.)
        /// </summary>
        public const int AverageSalaryInRegion = 40_000;

        /// <summary>
        /// Поправка по ЗП (Коэффициент)
        /// </summary>
        public const double SalaryFix = 3.2;

        /// <summary>
        /// Рабочих дней в месяце (шт)
        /// </summary>
        public const int WorkingDaysPerMonth = 21;

        /// <summary>
        /// ЗП с поправкой на многочисленные накладные расходы
        /// </summary>
        public static readonly double SalaryWithFix = SalaryFix * AverageSalaryInRegion;

        /// <summary>
        /// Кол-во рабочих часов в месяц
        /// </summary>
        public static readonly int WorkingHoursPerMonth = WorkingHoursPerDay * WorkingDaysPerMonth;

        /// <summary>
        /// Средняя зарплата в час
        /// </summary>
        public static readonly double AverageSalaryPerHour = SalaryWithFix / WorkingHoursPerMonth;

        /// <summary>
        /// Доплата за кастомный дизайн
        /// </summary>
        public const double CustomDesignMarkup = 0.3;
    }
}
