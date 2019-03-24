using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using ColdCallsTracker.Code.Data.Models;
using ColdCallsTracker.Code.Extensions;

namespace ColdCallsTracker.Code.Utils
{
    public static class YandexOrganizationsParser
    {
        public static List<Company> ParsePage(string content)
        {
            var companies = new List<Company>();
            var parser = new HtmlParser();
            var document = parser.ParseDocument(content);
            var elements = document.QuerySelectorAll(".company-snippet").ToList();
            foreach (var element in elements)
            {
                var title = element.QuerySelector(".company-card__title")?.Text();
                var address = element.QuerySelector(".company-card__address")?.Text();
                var phone = FixPhone(element.QuerySelector(".company-card__phone")?.Text());
                if (!phone.HasValue())
                    continue;
                var webSite = element.QuerySelector(".company-card__site")?.Text();
                var category = element.QuerySelector(".company-card__rubrics")?.Text();

                var company = new Company
                {
                    Name = title,
                    ActivityType = category,
                    Phones = new List<Phone> { new Phone { Number = phone } },
                    Address = address,
                    WebSites = webSite,
                    Remarks = "Из яндекс справочника"
                };
                companies.Add(company);
            }


            return companies;
        }

        private static string FixPhone(string input)
        {
            if (input == null)
                return string.Empty;
            if (input.StartsWith("+7"))
            {
                return "8" + input.Substring(2);
            }
            return input;

        }
    }
}
