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
            this.App = appService;
        }
    }
}
