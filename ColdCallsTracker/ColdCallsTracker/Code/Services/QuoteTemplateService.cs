using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ColdCallsTracker.Code.Data;
using ColdCallsTracker.Code.Data.Models;
using ColdCallsTracker.Code.Data.ViewModels;
using ColdCallsTracker.Code.Data.ViewModels._Common;
using ColdCallsTracker.Code.Extensions;
using Microsoft.EntityFrameworkCore;

namespace ColdCallsTracker.Code.Services
{
    public class QuoteTemplateService : ServiceBase
    {
        public QuoteTemplateService(AppDbContext db, AppService appService) : base(db, appService)
        {
        }

        public void Edit(QuoteTemplateItem item)
        {
            
        }

        public void Remove(int id)
        {
           
        }
    }
}
