using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using ColdCallsTracker.Code.Data.Models;
using ColdCallsTracker.Code.Extensions;
using ColdCallsTracker.Code.Utils;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium.Chrome;

namespace ColdCallsTracker.Controllers
{
    public class ParsersController : AppControllerBase
    {
        public ParsersController(IHostingEnvironment hostingEnvironment) : base(hostingEnvironment)
        {
        }

        public ActionResult Yandex()
        {
            var sourceFolder = @"C:\yandex-organizations\Г";
            var htmlFiles = Directory.GetFiles(sourceFolder, "*.html");

            var allCompanies = new List<Company>();
            foreach (var htmlFile in htmlFiles)
            {
                var text = System.IO.File.ReadAllText(htmlFile);
                var companies = YandexOrganizationsParser.ParsePage(text);
                allCompanies.AddRange(companies);
            }

            var withoutSite = allCompanies.Where(x => !x.WebSites.HasValue()).ToList();

            for (var i = 0; i < allCompanies.Count; i++)
            {
                var company = allCompanies[i];
                Service.Company.SaveCompanyFromExport(company);
            }


            return Content("");
        }

        public ActionResult TwoGis()
        {
            //  var path = @"C:\мебель на заказ.har";
            var path = @"C:\детлагеря.har";
            var allText = System.IO.File.ReadAllText(path, Encoding.UTF8);
            var jObject = JObject.Parse(allText);
            var elements = (jObject["log"]["entries"] as JArray).ToList();
            var list = elements
                .Where(x => (x["request"] as JObject).GetValue("url").ToString().Contains("/items?viewpoint1="))
                .Where(x => (x["request"] as JObject).GetValue("method").ToString() == "GET")
                .SelectMany(x => (JArray)JObject.Parse(x["response"]["content"]["text"].ToString())["result"]["items"])
                .Where(x => (x as JObject)["adm_div"].Any(r => r["name"].ToString() == "Краснодар"))
                .Select(x => GetCompany(x as JObject))
                .Where(x => x != null)
                .ToList();

            var withoutWeebsites = list.Where(x => !x.WebSites.HasValue()).ToList();


            return Content("");
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
                    Phones = phones.Select(p => new Phone
                    {
                        Number = p
                    }).ToList(),
                    WebSites = string.Join(" ", webSites)
                };
            }
            catch (Exception e)
            {
                return null;
            }
        }


    }
}
