using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ColdCallsTracker.Code;
using ColdCallsTracker.Code.Data;
using Microsoft.AspNetCore.Mvc;

namespace ColdCallsTracker.Controllers
{
    public class AppControllerBase : Controller
    {
        protected AppService Service { get; set; }

        public AppControllerBase()
        {
            Service = new AppService(new AppDbContext());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) Service.Dispose();
            base.Dispose(disposing);
        }
    }
}
