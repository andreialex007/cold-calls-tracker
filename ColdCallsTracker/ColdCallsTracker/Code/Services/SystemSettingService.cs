using System.Linq;
using ColdCallsTracker.Code.Data;

namespace ColdCallsTracker.Code.Services
{
    public class SystemSettingService : ServiceBase
    {
        public SystemSettingService(AppDbContext db, AppService appService) : base(db, appService)
        {
        }

        public string GetSetting(string code)
        {
            return Db.SystemSettings.Single(x => x.Code == code).Value;
        }

        public void SetSetting(string code, string value)
        {
            var systemSetting = Db.SystemSettings.Single(x => x.Code == code);
            systemSetting.Value = value;
            Db.SaveChanges();
        }
    }
}
