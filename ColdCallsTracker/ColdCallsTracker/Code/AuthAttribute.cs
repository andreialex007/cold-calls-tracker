using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ColdCallsTracker.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ColdCallsTracker.Code
{
    public class AuthAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var controller = (AppControllerBase)context.Controller;
            var loggedIn = context.HttpContext.Session.GetString("LoggedIn") == "true";
            if (loggedIn)
            {
                base.OnActionExecuting(context);
            }
            else
            {
                context.Result = controller.RedirectToAction("Index", "Login");
            }
        }
    }
}
