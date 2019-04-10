using ColdCallsTracker.Code.Data.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace ColdCallsTracker.Controllers
{
    public class CostingTemplatesController : AuthAppControllerBase
    {
        public CostingTemplatesController(IHostingEnvironment hostingEnvironment) : base(hostingEnvironment)
        {
        }

        [HttpGet]
        public ActionResult Index()
        {
            var items = Service.CostingTemplate.All();

            return View("~/Pages/CostingTemplates/Index.cshtml", items);
        }

        [HttpPost]
        public ActionResult Save([FromBody] CostingTemplateItem item)
        {
            Service.CostingTemplate.Save(item);
            return Json(item);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            Service.CostingTemplate.Remove(id);
            return Json(new { result = "OK" });
        }
    }
}
