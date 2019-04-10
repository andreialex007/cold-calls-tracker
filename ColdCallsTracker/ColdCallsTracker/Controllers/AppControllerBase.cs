using ColdCallsTracker.Code;
using ColdCallsTracker.Code.Data;
using ColdCallsTracker.Code.Exceptions;
using ColdCallsTracker.Code.Extensions;
using ColdCallsTracker.Code.Utils;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ColdCallsTracker.Controllers
{
    public class AppControllerBase : Controller
    {
        protected AppService Service { get; set; }
        private readonly IHostingEnvironment _hostingEnvironment;
        public bool IsLoggedIn => HttpContext.Session.GetString("LoggedIn") == "true";

        public static string AdminPassword { get; set; }
        public static string AdminUserName { get; set; }

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

    [Auth]
    public class AuthAppControllerBase : AppControllerBase
    {
        public AuthAppControllerBase(IHostingEnvironment hostingEnvironment) : base(hostingEnvironment)
        {
        }
    }
}
