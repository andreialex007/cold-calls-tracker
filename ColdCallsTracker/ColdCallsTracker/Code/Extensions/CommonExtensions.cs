﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
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

    }
}
