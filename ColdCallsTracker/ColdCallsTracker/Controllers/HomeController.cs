using System.Collections.Generic;
using ColdCallsTracker.Code.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace ColdCallsTracker.Controllers
{
    public class HomeController : AuthAppControllerBase
    {
        public HomeController(IHostingEnvironment hostingEnvironment) : base(hostingEnvironment)
        {
        }

        public IActionResult Index()
        {
            var allStates = this.Service.State.All();
            return View(allStates);
        }

        

    }
}
