using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using ColdCallsTracker.Code.Data.Models;
using ColdCallsTracker.Code.Data.ViewModels;
using ColdCallsTracker.Code.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ColdCallsTracker.Code.Extensions
{
    public static class CommonExtensions
    {
        public static string ToJson(this object value)
        {
            var cultureInfo = new CultureInfo("en-US");
            cultureInfo.NumberFormat.NumberDecimalSeparator = ".";
            var settings = new JsonSerializerSettings
            {
                Culture = cultureInfo,
                Converters = new List<JsonConverter>
                {
                    new IsoDateTimeConverter { DateTimeFormat = "dd.MM.yyyy" }
                },
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,

            };
            var serializeObject = JsonConvert.SerializeObject(value, settings);
            return serializeObject;
        }


        public static void CalcTotalForCostingTemplates(this IEnumerable<CostingTemplateItem> costingTemplates)
        {
            var uiTemplates = costingTemplates.Where(x => x.Cost == null);

            var uiTotal = 0.0;
            foreach (var templateItem in uiTemplates)
            {
                templateItem.Cost = GlobalVariables.AverageSalaryPerHour;
                templateItem.Total = templateItem.Cost * templateItem.Qty;
                uiTotal += (templateItem.Total ?? 0);
            }
        }

    }
}
