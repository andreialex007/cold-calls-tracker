using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ColdCallsTracker.Code.Data;
using ColdCallsTracker.Code.Data.Models;
using ColdCallsTracker.Code.Data.ViewModels;
using ColdCallsTracker.Code.Extensions;

namespace ColdCallsTracker.Code.Services
{
    public class CallRecordService : ServiceBase
    {
        public CallRecordService(AppDbContext db, AppService appService) : base(db, appService)
        {
        }

        public void AddRecord(CallRecordItem item)
        {
            item.GetValidationErrors().ThrowIfHasErrors();

            var dbItem = new CallRecord();

            if (item.Id != 0)
            {
                dbItem = Db.CallRecords.Single(x => x.Id == item.Id);
            }
            else
            {
                dbItem.DateCreate = DateTime.Now;
                Db.CallRecords.Add(dbItem);
            }

            dbItem.DateModify = DateTime.Now;
            dbItem.Content = item.Content;
            dbItem.PhoneId = item.PhoneId;

            Db.SaveChanges();

            item.Id = dbItem.Id;
        }


        public CallRecordItem AddRecord(int phoneId, string content)
        {
            var callRecordItem = new CallRecordItem
            {
                Id = 0,
                Content = content,
                PhoneId = phoneId
            };

            AddRecord(callRecordItem);
            callRecordItem.Phone = Db.Phones.Single(x => x.Id == phoneId).Number;
            return callRecordItem;
        }
    }
}
