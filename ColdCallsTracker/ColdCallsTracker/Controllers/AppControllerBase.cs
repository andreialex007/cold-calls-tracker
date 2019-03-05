using ColdCallsTracker.Code;
using ColdCallsTracker.Code.Data;
using ColdCallsTracker.Code.Exceptions;
using ColdCallsTracker.Code.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

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

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            try
            {
                base.OnActionExecuting(context);
            }
            catch (ValidationException e)
            {
                var errorsView = this.RenderViewAsync("~/Views/Shared/_Errors.cshtml", e.Errors).Result;
                context.Result = new JsonResult(new
                {
                    errorsView
                });
            }

            
        }
    }
}
