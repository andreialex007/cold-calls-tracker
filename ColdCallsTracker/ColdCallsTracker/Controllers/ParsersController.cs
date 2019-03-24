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

            return null;

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
            return null;

            //  var path = @"C:\мебель на заказ.har";
            var path = @"C:\2gis\стоматологии.har";
            var companies = TwoGisOrgParser.ParseFile(path);
            foreach (var company in companies)
            {
                Service.Company.SaveCompanyFromExport(company);
            }

            return Content("");
        }

    }
}
