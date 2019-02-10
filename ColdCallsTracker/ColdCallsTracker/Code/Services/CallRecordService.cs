using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ColdCallsTracker.Code.Data;

namespace ColdCallsTracker.Code.Services
{
    public class CallRecordService : ServiceBase
    {
        public CallRecordService(AppDbContext db, AppService appService) : base(db, appService)
        {
        }
    }
}
