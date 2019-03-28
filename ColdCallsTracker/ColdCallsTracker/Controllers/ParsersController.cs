using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using ColdCallsTracker.Code.Data.Models;
using ColdCallsTracker.Code.Extensions;
using ColdCallsTracker.Code.Utils;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

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
            var sourceFolder = @"C:\yandex-organizations\е";
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

        private static readonly object _locker = new object();

        public ActionResult TwoGis()
        {

            return null;

            if (Monitor.TryEnter(_locker))
            {
                Thread.Sleep(5_000);

                //  var path = @"C:\мебель на заказ.har";
                var files = Directory.GetFiles(@"C:\2gis\new12", "*.har");
                foreach (var file in files)
                {
                    Debug.WriteLine("path=" + file);
                    var companies = TwoGisOrgParser.ParseFile(file);
                    var inserted = 0;
                    foreach (var company in companies)
                    {
                        if (Service.Company.SaveCompanyFromExport(company))
                        {
                            inserted++;
                        };
                    }

                    Debug.WriteLine("inserted=" + inserted);
                }
                Monitor.Exit(_locker);
                return Content("");
            }

            return Content("");
        }

    }
}
