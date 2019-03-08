using System.Collections.Generic;
using ColdCallsTracker.Code.Data.Models;
using ColdCallsTracker.Code.Data.ViewModels;
using ColdCallsTracker.Code.Extensions;
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
            items.ForEach(x => x.Recalc());
            return View("~/Pages/QuoteTemplates/Index.cshtml", items);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var item = Service.QuoteTemplate.Get(id);
            item.Recalc();
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
            var item = Service.QuoteTemplate.Get(relation.QuoteTemplateId);
            return Json(new
            {
                total = item.Total,
                totalHours = item.TotalHours
            });
        }

        [HttpPost]
        public ActionResult DeleteRelation([FromBody] QuoteTemplateCostingTemplate relation)
        {
            Service.QuoteTemplate.RemoveRelation(relation);
            var item = Service.QuoteTemplate.Get(relation.QuoteTemplateId);
            return Json(new
            {
                total = item.Total,
                totalHours = item.TotalHours
            });
        }

        [HttpPost]
        public ActionResult SetMultiplier([FromBody] QuoteTemplateCostingTemplate relation)
        {
            Service.QuoteTemplate.SetMultiplier(relation);
            var item = Service.QuoteTemplate.Get(relation.QuoteTemplateId);
            return Json(new
            {
                total = item.Total,
                totalHours = item.TotalHours
            });
        }

        [HttpGet]
        public ActionResult SetCustomDesign(int id, bool isCustomDesign)
        {
            var item = Service.QuoteTemplate.Get(id);
            item.CustomDesign = isCustomDesign;
            Service.QuoteTemplate.Edit(item);
            return Json(new
            {
                total = item.Total,
                customDesignTotal = item.CustomDesignTotal,
                totalHours = item.TotalHours
            });
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            Service.QuoteTemplate.Remove(id);
            return RedirectToAction("Index");
        }
    }
}
