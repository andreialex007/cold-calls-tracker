using System;
using System.Collections.Generic;
using System.Linq;
using ColdCallsTracker.Code.Data;
using ColdCallsTracker.Code.Data.Models;
using ColdCallsTracker.Code.Data.ViewModels;
using ColdCallsTracker.Code.Extensions;
using Microsoft.EntityFrameworkCore;

namespace ColdCallsTracker.Code.Services
{
    public class CallAnswerService : ServiceBase
    {
        public CallAnswerService(AppDbContext db, AppService appService) : base(db, appService)
        {
        }

      
    }
}
