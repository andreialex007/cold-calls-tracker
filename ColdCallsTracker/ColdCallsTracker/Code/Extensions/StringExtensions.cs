using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ColdCallsTracker.Code.Extensions
{
    public static class StringExtensions
    {
        public static bool HasValue(this string input) => !(string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input));
    }
}
