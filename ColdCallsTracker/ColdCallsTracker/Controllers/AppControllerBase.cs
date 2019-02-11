using ColdCallsTracker.Code;
using ColdCallsTracker.Code.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace ColdCallsTracker.Controllers
{
    public class AppControllerBase : Controller
    {
        protected AppService Service { get; set; }
        private readonly IHostingEnvironment _hostingEnvironment;

        public AppControllerBase(IHostingEnvironment hostingEnvironment)
        {
            Service = new AppService(new AppDbContext());
            _hostingEnvironment = hostingEnvironment;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) Service.Dispose();
            base.Dispose(disposing);
        }
    }
}
