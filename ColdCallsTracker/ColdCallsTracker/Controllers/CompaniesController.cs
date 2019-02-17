using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ColdCallsTracker.Code.Data.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace ColdCallsTracker.Controllers
{
    public class CompaniesController : AppControllerBase
    {
        public CompaniesController(IHostingEnvironment hostingEnvironment) : base(hostingEnvironment)
        {
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View("~/Pages/Companies/Index.cshtml");
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            var company = Service.Company.Edit(id);

            return View("~/Pages/Companies/Index.cshtml", company);
        }

        [HttpPost]
        public ActionResult Search([FromBody] CompanySearchParameters parameters)
        {
            var (items, total, filtered) = Service.Company.Search(parameters);
            return Json(new
            {
                items,
                total,
                filtered
            });
        }
    }
}
