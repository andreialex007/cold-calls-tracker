using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ColdCallsTracker.Code.Data;
using ColdCallsTracker.Code.Data.Models;

namespace ColdCallsTracker.Code.Services
{
    public class StateService
    {
        protected AppDbContext Db { get; }
        protected AppService App { get; }

        public StateService(AppDbContext db, AppService appService)
        {
            this.Db = db;
        }

        public List<State> All()
        {
            return Db.States.OrderBy(x => x.Name).ToList();
        }
    }
}
