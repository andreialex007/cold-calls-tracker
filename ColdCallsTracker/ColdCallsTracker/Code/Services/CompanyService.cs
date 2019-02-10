using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ColdCallsTracker.Code.Data;

namespace ColdCallsTracker.Code.Services
{
    public class CompanyService : ServiceBase
    {
        public CompanyService(AppDbContext db, AppService appService) : base(db, appService)
        {
        }
    }
}
