using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ColdCallsTracker.Code.Data;
using ColdCallsTracker.Code.Data.Models;

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
