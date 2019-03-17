using System;
using System.Collections.Generic;
using System.Linq;
using ColdCallsTracker.Code.Data;
using ColdCallsTracker.Code.Data.Models;
using ColdCallsTracker.Code.Data.ViewModels;
using ColdCallsTracker.Code.Extensions;

namespace ColdCallsTracker.Code.Services
{
    public class CostingService : ServiceBase
    {
        public CostingService(AppDbContext db, AppService appService) : base(db, appService)
        {
        }

        public void AddCostingFromTemplate(int templateId, int quoteId)
        {
            var templateItem = this.App.CostingTemplate.Get(templateId);
            var items = new List<CostingTemplateItem> { templateItem };
            items.CalcTotalForCostingTemplates();
            App.Costing.Save(new CostingItem
            {
                Total = templateItem.Total,
                Cost = templateItem.Cost,
                CategoryId = templateItem.CategoryId,
                Multiplier = 1,
                Name = templateItem.Name,
                Qty = templateItem.Qty,
                QuoteId = quoteId,
                Unit = templateItem.Unit,
                DateModify = DateTime.Now,
                DateCreate = DateTime.Now
            });
        }

        public void Save(CostingItem uiItem)
        {
            uiItem.GetValidationErrors().ThrowIfHasErrors();

            var dbItem = new Costing();

            if (uiItem.Id == 0)
            {
                dbItem = new Costing();
                Db.Costings.Add(dbItem);
            }
            else
            {
                dbItem = Db.Costings.Single(x => x.Id == uiItem.Id);
            }

            dbItem.Name = uiItem.Name;
            dbItem.Total = uiItem.Total;
            dbItem.Multiplier = uiItem.Multiplier;
            dbItem.Qty = uiItem.Qty;
            dbItem.QuoteId = uiItem.QuoteId;
            dbItem.Unit = uiItem.Unit;
            dbItem.CategoryId = uiItem.CategoryId;
            dbItem.Cost = uiItem.Cost;

            dbItem.DateModify = DateTime.Now;

            Db.SaveChanges();

            uiItem.Id = dbItem.Id;
            uiItem.DateModify = dbItem.DateModify;
            uiItem.DateCreate = dbItem.DateCreate;
        }

        public void Delete(int id)
        {
            Db.Delete<Costing>(x => x.Id == id);
            Db.SaveChanges();
        }

        public int GetQuoteIdByCosting(int id)
        {
            return Db.Costings.Single(x => x.Id == id).QuoteId;
        }
    }
}
