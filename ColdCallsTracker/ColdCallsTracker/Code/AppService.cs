using System;
using ColdCallsTracker.Code.Data;
using ColdCallsTracker.Code.Services;

namespace ColdCallsTracker.Code
{
    public class AppService : IDisposable
    {
        private readonly AppDbContext _appDbContext;

        public PhoneService Phone { get; set; }
        public CompanyService Company { get; set; }
        public CallRecordService CallRecord { get; set; }
        public StateService State { get; set; }
        public SystemSettingService SystemSetting { get; set; }

        public AppService(AppDbContext db)
        {
            Phone = new PhoneService(db, this);
            Company = new CompanyService(db, this);
            CallRecord = new CallRecordService(db, this);
            State = new StateService(db, this);
            SystemSetting = new SystemSettingService(db, this);

            _appDbContext = db;
        }

        public void Dispose()
        {
            _appDbContext.Dispose();
        }
    }
}
