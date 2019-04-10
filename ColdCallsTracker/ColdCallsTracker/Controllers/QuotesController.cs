using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace ColdCallsTracker.Controllers
{
    public class QuotesController : AuthAppControllerBase
    {
        public QuotesController(IHostingEnvironment hostingEnvironment)
            : base(hostingEnvironment)
        {
        }

        public ActionResult Index()
        {
            return null;
        }
    }
}
