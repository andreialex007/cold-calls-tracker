using ColdCallsTracker.Code.Data.Models;
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

        [HttpGet]
        public ActionResult New()
        {
            var item = Service.QuoteTemplate.New();
            return RedirectToAction("Edit", new { id = item.Id });
        }

        [HttpPost]
        public ActionResult Edit([FromForm] QuoteTemplateItem item)
        {
            Service.QuoteTemplate.Edit(item);
            return RedirectToAction("Edit", new { id = item.Id });
        }

        [HttpPost]
        public ActionResult AddRelation([FromBody] QuoteTemplateCostingTemplate relation)
        {
            Service.QuoteTemplate.AddRelation(relation);
            return Json(new { result = "Ok" });
        }

        [HttpPost]
        public ActionResult DeleteRelation([FromBody] QuoteTemplateCostingTemplate relation)
        {
            Service.QuoteTemplate.RemoveRelation(relation);
            return Json(new { result = "Ok" });
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            Service.QuoteTemplate.Remove(id);
            return RedirectToAction("Index");
        }
    }
}
