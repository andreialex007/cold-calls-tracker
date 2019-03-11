using System;
using System.Linq;
using ColdCallsTracker.Code.Data;
using ColdCallsTracker.Code.Data.ViewModels;
using ColdCallsTracker.Code.Extensions;

namespace ColdCallsTracker.Code.Services
{
    public class CostingService : ServiceBase
    {
        public CostingService(AppDbContext db, AppService appService) : base(db, appService)
        {
        }

        public void Save(CostingItem item)
        {
            item.GetValidationErrors().ThrowIfHasErrors();

            var dbItem = Db.Costings.Single(x => x.Id == item.Id);

            dbItem.Name = item.Name;
            dbItem.Total = item.Total;
            dbItem.Multiplier = item.Multiplier;
            dbItem.Qty = item.Qty;
            dbItem.QuoteId = item.QuoteId;
            dbItem.Unit = item.Unit;
            dbItem.CategoryId = item.CategoryId;
            dbItem.Cost = item.Cost;

            dbItem.DateModify = DateTime.Now;

            Db.SaveChanges();

            item.Id = dbItem.Id;
            item.DateModify = dbItem.DateModify;
            item.DateCreate = dbItem.DateCreate;
        }
    }
}
