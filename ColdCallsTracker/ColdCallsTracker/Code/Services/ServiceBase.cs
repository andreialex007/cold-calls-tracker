using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ColdCallsTracker.Code.Data;

namespace ColdCallsTracker.Code.Services
{
    public class ServiceBase
    {
        protected AppDbContext Db { get; }
        protected AppService App { get; }

        public ServiceBase(AppDbContext db, AppService appService)
        {
            this.Db = db;
        }
    }
}
