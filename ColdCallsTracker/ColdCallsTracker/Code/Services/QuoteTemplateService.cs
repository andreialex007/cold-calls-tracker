using System;
using System.Collections.Generic;
using System.Linq;
using ColdCallsTracker.Code.Data;
using ColdCallsTracker.Code.Data.Models;
using ColdCallsTracker.Code.Data.ViewModels;
using ColdCallsTracker.Code.Extensions;
using Microsoft.EntityFrameworkCore;

namespace ColdCallsTracker.Code.Services
{
    public class QuoteTemplateService : ServiceBase
    {
        public QuoteTemplateService(AppDbContext db, AppService appService) : base(db, appService)
        {
        }

        public List<QuoteTemplateItem> All()
        {
            var items = Db.QuoteTemplates
                .Select(x => new QuoteTemplateItem
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .OrderBy(x => x.Name)
                .ToList();

            return items;
        }

        public QuoteTemplateItem Get(int id)
        {
            var item = new QuoteTemplateItem
            {
                DateCreate = DateTime.Now
            };

            if (id != 0)
            {
                item = Db.QuoteTemplates
                    .Include(x => x.CostingTemplates.Select(c => c.CostingTemplate))
                    .Select(x => new QuoteTemplateItem
                    {
                        Id = x.Id,
                        Name = x.Name,
                        DateModify = x.DateModify,
                        DateCreate = x.DateCreate,
                        CustomDesign = x.CustomDesign,
                        CostingTemplates =
                            x.CostingTemplates.ToList()
                    })
                    .Single(x => x.Id == id);
            }

            AppendData(item);
            return item;
        }

        public void Edit(QuoteTemplateItem item)
        {
            item.GetValidationErrors().ThrowIfHasErrors();

            var dbItem = new QuoteTemplate();

            if (item.Id != 0)
            {
                dbItem = Db.QuoteTemplates.Single(x => x.Id == item.Id);
            }
            else
            {
                dbItem.DateCreate = DateTime.Now;
                Db.QuoteTemplates.Add(dbItem);
            }

            dbItem.Name = item.Name;
            dbItem.DateModify = DateTime.Now;

            Db.SaveChanges();

            item.Id = dbItem.Id;
            item.DateModify = dbItem.DateModify;
            item.DateCreate = dbItem.DateCreate;
        }

        public void AppendData(QuoteTemplateItem item)
        {
            item.AvaliableCostingTemplates = this.App.CostingTemplate.All();
        }

        public void Remove(int id)
        {
            var entity = Db.CostingTemplates.Include(x => x.QuoteTemplates).First(x => x.Id == id);
            Db.CostingTemplates.Remove(entity);
            Db.SaveChanges();
        }
    }
}
