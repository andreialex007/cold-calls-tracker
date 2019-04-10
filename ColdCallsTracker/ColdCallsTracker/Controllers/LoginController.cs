using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ColdCallsTracker.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using ColdCallsTracker.Code.Extensions;
using Microsoft.AspNetCore.Http;

namespace ColdCallsTracker.Controllers
{
    public class LoginController : AppControllerBase
    {
        public LoginController(IHostingEnvironment hostingEnvironment) : base(hostingEnvironment)
        {
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View("Index", new LoginModel());
        }

        [HttpPost]
        public ActionResult Index(LoginModel loginModel)
        {
            if (AdminUserName == loginModel.UserName && AdminPassword == loginModel.Password)
            {
                HttpContext.Session.SetString("LoggedIn", "true");
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "Неверный логин или пароль";
            return View("Index", loginModel);
        }

        [HttpGet]
        public ActionResult LogOut()
        {
            HttpContext.Session.SetString("LoggedIn", "");
            return RedirectToAction("Index", "Login");
        }
    }
}
