using System;
using ColdCallsTracker.Code.Data;
using ColdCallsTracker.Code.Data.Models;
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
        public CostingTemplateService CostingTemplate { get; set; }
        public QuoteTemplateService QuoteTemplate { get; set; }
        public CallScriptService CallScript { get; set; }
        public CallAnswerService CallAnswer { get; set; }
        public CallQuestionService CallQuestion { get; set; }
        public QuoteService Quote { get; set; }
        public CostingService Costing { get; set; }

        public AppService(AppDbContext db)
        {
            Phone = new PhoneService(db, this);
            Company = new CompanyService(db, this);
            CallRecord = new CallRecordService(db, this);
            State = new StateService(db, this);
            SystemSetting = new SystemSettingService(db, this);
            CostingTemplate = new CostingTemplateService(db, this);
            QuoteTemplate = new QuoteTemplateService(db, this);
            Quote = new QuoteService(db, this);
            Costing = new CostingService(db, this);
            CallScript = new CallScriptService(db, this);
            CallQuestion = new CallQuestionService(db, this);
            CallAnswer = new CallAnswerService(db, this);

            _appDbContext = db;
        }

        public void Dispose()
        {
            _appDbContext.Dispose();
        }
    }
}
