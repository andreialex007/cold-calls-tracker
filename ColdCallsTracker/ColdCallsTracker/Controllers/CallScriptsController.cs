using ColdCallsTracker.Code.Data.Models;
using ColdCallsTracker.Code.Data.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace ColdCallsTracker.Controllers
{
    public class CallScriptsController : AuthAppControllerBase
    {
        public CallScriptsController(IHostingEnvironment hostingEnvironment) : base(hostingEnvironment)
        {
        }

        [HttpGet]
        public ActionResult Index()
        {
            var items = Service.CallScript.All();
            return View("~/Pages/CallScripts/Index.cshtml", items);
        }

        [HttpGet]
        public ActionResult New()
        {
            var item = Service.CallScript.New();
            return RedirectToAction("Edit", new { id = item.Id });
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var item = Service.CallScript.Get(id);
            return View("~/Pages/CallScripts/Edit.cshtml", item);
        }

        [HttpGet]
        public ActionResult Load(int id)
        {
            var item = Service.CallScript.Get(id);
            return Json(item);
        }

        [HttpPost]
        public ActionResult Edit([FromForm] CallScriptItem item)
        {
            Service.CallScript.SetName(item.Id, item.Name);
            return RedirectToAction("Edit", new { id = item.Id });
        }

        [HttpPost]
        public ActionResult EditQuestion([FromBody] CallQuestionItem item)
        {
            this.Service.CallQuestion.Save(item);
            return Json(item);
        }

        [HttpPost]
        public ActionResult EditAnswer([FromBody] CallAnswerItem item)
        {
            this.Service.CallAnswer.Save(item);
            return Json(item);
        }

        [HttpGet]
        public ActionResult DeleteAnswer(int id)
        {
            this.Service.CallAnswer.DeleteById<CallAnswer>(id);
            return Content("Ok");
        }

        [HttpGet]
        public ActionResult DeleteQuestion(int id)
        {
            this.Service.CallQuestion.Delete(id);
            return Content("Ok");
        }

        [HttpGet]
        public ActionResult DeleteScript(int id)
        {
            this.Service.CallScript.Delete(id);
            return RedirectToAction("Index");
        }

    }
}
