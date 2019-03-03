using ColdCallsTracker.Code.Data.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace ColdCallsTracker.Controllers
{
    public class CostingTemplatesController : AppControllerBase
    {
        public CostingTemplatesController(IHostingEnvironment hostingEnvironment) : base(hostingEnvironment)
        {
        }

        [HttpGet]
        public ActionResult Index() => View("~/Pages/Companies/Index.cshtml");

        [HttpGet]
        public ActionResult Edit(int? id) => View("~/Pages/Companies/Edit.cshtml");

    }
}
