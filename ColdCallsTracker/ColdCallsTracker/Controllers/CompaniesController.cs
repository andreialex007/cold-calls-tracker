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
        public ActionResult Index() => View("~/Pages/Companies/Index.cshtml");

        [HttpGet]
        public ActionResult Edit(int? id) => View("~/Pages/Companies/Edit.cshtml");

        [HttpGet]
        public ActionResult Load(int id)
        {
            var company = Service.Company.Edit(id);
            return Json(company);
        }

        [HttpPost]
        public ActionResult Save([FromBody] CompanyEditItem item)
        {
            Service.Company.Save(item);
            return Json(item);
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
