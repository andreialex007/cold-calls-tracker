using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace ColdCallsTracker.Controllers
{
    public class ScriptController : AppControllerBase
    {
        public ScriptController(IHostingEnvironment hostingEnvironment) : base(hostingEnvironment)
        {
        }


        [HttpGet]
        public ActionResult Index()
        {
            var scriptText = Service.SystemSetting.GetSetting("ScriptText");

            return View("~/Pages/Script/Index.cshtml", model: scriptText);
        }

        [HttpPost]
        public ActionResult SaveScript(string content)
        {
            Service.SystemSetting.SetSetting("ScriptText", content);
            return Content("");
        }
    }
}
