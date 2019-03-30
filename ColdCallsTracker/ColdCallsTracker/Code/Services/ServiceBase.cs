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

        public void DeleteById<T>(int id) where T : class
        {
            var el = this.Db.Set<T>().Find(id);
            this.Db.Set<T>().Remove(el);
            Db.SaveChanges();
        }
    }
}
