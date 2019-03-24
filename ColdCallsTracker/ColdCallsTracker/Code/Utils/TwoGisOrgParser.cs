using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using ColdCallsTracker.Code.Data.Models;
using ColdCallsTracker.Code.Extensions;
using Newtonsoft.Json.Linq;

namespace ColdCallsTracker.Code.Utils
{
    public static class TwoGisOrgParser
    {
        public static List<Company> ParseFile(string xhrFilePath)
        {
            var allText = System.IO.File.ReadAllText(xhrFilePath, Encoding.UTF8);
            var jObject = JObject.Parse(allText);
            var elements = (jObject["log"]["entries"] as JArray).ToList();
            var list = elements
                .Where(x => (x["request"] as JObject).GetValue("url").ToString().Contains("?viewpoint1="))
                .Where(x => (x["request"] as JObject).GetValue("method").ToString() == "GET")
                .SelectMany(x => (JArray)JObject.Parse(x["response"]["content"]["text"].ToString())["result"]["items"])
                .Where(x => (x as JObject)["adm_div"].Any(r => r["name"].ToString() == "Краснодар"))
                .Select(x => GetCompany(x as JObject))
                .Where(x => x != null)
                .ToList();

            var withoutWeebsites = list.Where(x => !x.WebSites.HasValue()).ToList();

            return list;
        }

        private static Company GetCompany(JObject jObject)
        {
            try
            {
                var name = jObject["name"].ToString();
                var category = jObject["rubrics"].First()["name"].ToString();
                var address = jObject["address_name"] == null ? "" : jObject["address_name"].ToString();
                var contacts = jObject["contact_groups"]
                    .SelectMany(f => f["contacts"])
                    .ToList();

                var phones = contacts.Where(x => x["type"].ToString() == "phone").Select(x => x["value"].ToString()).ToList();
                var webSites = contacts.Where(x => x["type"].ToString() == "website").Select(x => x["url"].ToString()).ToList();

                return new Company
                {
                    ActivityType = category,
                    Address = address,
                    Name = name,
                    Phones = phones
                        .Where(x => FixPhone(x).HasValue())
                        .Select(p => new Phone
                        {
                            Number = FixPhone(p),
                            Remarks = "Общий"
                        }).ToList(),
                    WebSites = string.Join(" ", webSites),
                    Remarks = "Из 2gis"
                };
            }
            catch (Exception e)
            {
                return null;
            }
        }


        private static string FixPhone(string input)
        {
            if (input == null)
                return string.Empty;
            if (input.StartsWith("+7"))
                input = "8" + input.Substring(2);
            input = Regex.Replace(input, "[^.0-9]", "");
            input = input.Insert(9, "-");
            input = input.Insert(7, "-");
            input = input.Insert(4, " ");
            input = input.Insert(4, ")");
            input = input.Insert(1, "(");
            input = input.Insert(1, " ");

            return input;

        }

    }
}
