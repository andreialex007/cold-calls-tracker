using ColdCallsTracker.Code.Data.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace ColdCallsTracker.Controllers
{
    public class QuoteTemplatesController : AppControllerBase
    {
        public QuoteTemplatesController(IHostingEnvironment hostingEnvironment) : base(hostingEnvironment)
        {
        }

        [HttpGet]
        public ActionResult Index()
        {
            var items = Service.QuoteTemplate.All();
            return View("~/Pages/QuoteTemplates/Index.cshtml", items);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var item = Service.QuoteTemplate.Get(id);
            return View("~/Pages/QuoteTemplates/Edit.cshtml", item);
        }

        [HttpPost]
        public ActionResult Edit([FromBody] QuoteTemplateItem item)
        {
            Service.QuoteTemplate.Edit(item);
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
