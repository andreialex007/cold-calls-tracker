using System;
using System.Collections.Generic;
using System.Linq;
using ColdCallsTracker.Code.Data;
using ColdCallsTracker.Code.Data.Models;
using ColdCallsTracker.Code.Data.ViewModels;
using ColdCallsTracker.Code.Extensions;

namespace ColdCallsTracker.Code.Services
{
    public class CostingTemplateService : ServiceBase
    {
        public CostingTemplateService(AppDbContext db, AppService appService) : base(db, appService)
        {
        }

        public List<CostingTemplateItem> All()
        {
            var items = Db.CostingTemplates.Select(x => new CostingTemplateItem
            {
                Unit = x.Unit,
                Id = x.Id,
                Cost = x.Cost,
                DateCreate = x.DateCreate,
                DateModify = x.DateModify,
                Name = x.Name,
                Qty = x.Qty,
                Total = x.Total,
                CategoryId = x.CategoryId
            })
                .ToList()
                .OrderBy(x => x.CategoryName)
                .ThenBy(x => x.Name)
                .ToList();

            return items;
        }


        public CostingTemplateItem Get(int id)
        {
            return Db.CostingTemplates
               .Select(x => new CostingTemplateItem
               {
                   Unit = x.Unit,
                   Id = x.Id,
                   Cost = x.Cost,
                   DateCreate = x.DateCreate,
                   DateModify = x.DateModify,
                   Name = x.Name,
                   Qty = x.Qty,
                   Total = x.Total,
                   CategoryId = x.CategoryId
               })
               .Single(x => x.Id == id);
        }


        public void Save(CostingTemplateItem item)
        {
            item.GetValidationErrors().ThrowIfHasErrors();

            var dbItem = new CostingTemplate();

            if (item.Id != 0)
            {
                dbItem = Db.CostingTemplates.Single(x => x.Id == item.Id);
            }
            else
            {
                dbItem.DateCreate = DateTime.Now;
                Db.CostingTemplates.Add(dbItem);
            }

            dbItem.Name = item.Name;
            dbItem.Cost = item.Cost;
            dbItem.Qty = item.Qty;
            dbItem.Total = item.Total;
            dbItem.Unit = item.Unit;
            dbItem.CategoryId = item.CategoryId;
            dbItem.DateModify = DateTime.Now;

            Db.SaveChanges();

            item.Id = dbItem.Id;
            item.DateModify = dbItem.DateModify;
            item.DateCreate = dbItem.DateCreate;
        }

        public void Remove(int id)
        {
            var entity = Db.CostingTemplates.First(x => x.Id == id);
            Db.CostingTemplates.Remove(entity);
            Db.SaveChanges();
        }
    }
}
