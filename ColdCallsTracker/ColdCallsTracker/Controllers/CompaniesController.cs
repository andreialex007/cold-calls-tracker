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

        [HttpPost]
        public ActionResult Search(CompanySearchParameters parameters)
        {
            var (items, total) = Service.Company.Search(parameters);
            return Json(new
            {
                items,
                total
            });
        }
    }
}
