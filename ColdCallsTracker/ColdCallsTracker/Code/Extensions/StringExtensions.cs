using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace ColdCallsTracker.Code.Extensions
{
    public static class StringExtensions
    {
        public static bool HasValue(this string input) => !(string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input));
        public static DateTime? ToDateTime(this string input, string format = "dd.MM.yyyy HH:mm:ss")
        {
            DateTime dt;
            if (DateTime.TryParseExact(input,
                format,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out dt))
            {
                return dt;
            };
            return null;
        }
    }
}
