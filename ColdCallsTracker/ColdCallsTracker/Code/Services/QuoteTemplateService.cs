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
            var includableQueryable = Db.QuoteTemplates
                .Include(x => x.CostingTemplates)
                .ThenInclude(x => x.CostingTemplate)
                .ToList();


            var items = Db.QuoteTemplates
                .Select(x => new QuoteTemplateItem
                {
                    Id = x.Id,
                    Name = x.Name,
                    CustomDesign = x.CustomDesign
                })
                .OrderBy(x => x.Name)
                .ToList();


            var allCostings = this.App.CostingTemplate.All();
            items.ForEach(x => x.AvaliableCostingTemplates = allCostings);
            items.ForEach(x => x.QuoteCostingRelations =
                (includableQueryable.FirstOrDefault(t => t.Id == x.Id)?.CostingTemplates ?? new List<QuoteTemplateCostingTemplate>()));

            items.ForEach(x => x.Recalc());
            return items;
        }

        public QuoteTemplateItem New()
        {
            var newTemplate = new QuoteTemplate
            {
                DateCreate = DateTime.Now,
                DateModify = DateTime.Now,
                CustomDesign = false,
                Name = "Новый шаблон"
            };
            Db.QuoteTemplates.Add(newTemplate);
            Db.SaveChanges();

            return Get(newTemplate.Id);
        }

        public QuoteTemplateItem Get(int id)
        {
            var includableQueryable = Db.QuoteTemplates
                .Include(x => x.CostingTemplates)
                .ThenInclude(x => x.CostingTemplate);

            var dbItem = includableQueryable.Single(x => x.Id == id);


            var item = includableQueryable
                  .Select(x => new QuoteTemplateItem
                  {
                      Id = x.Id,
                      Name = x.Name,
                      DateModify = x.DateModify,
                      DateCreate = x.DateCreate,
                      CustomDesign = x.CustomDesign
                  })
                  .FirstOrDefault(x => x.Id == id);
            item.QuoteCostingRelations = dbItem.CostingTemplates;
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
            dbItem.CustomDesign = item.CustomDesign;
            dbItem.DateModify = DateTime.Now;

            Db.SaveChanges();

            item.Id = dbItem.Id;
            item.DateModify = dbItem.DateModify;
            item.DateCreate = dbItem.DateCreate;
        }

        public void RemoveRelation(QuoteTemplateCostingTemplate uiRelation)
        {
            var quoteTemplate = Db.QuoteTemplates
                .Include(x => x.CostingTemplates)
                .ThenInclude(x => x.CostingTemplate)
                .Single(x => x.Id == uiRelation.QuoteTemplateId);

            var itemToRemove = quoteTemplate.CostingTemplates.Single(x => x.CostingTemplateId == uiRelation.CostingTemplateId);
            quoteTemplate.CostingTemplates.Remove(itemToRemove);
            Db.SaveChanges();
        }

        public void SetMultiplier(QuoteTemplateCostingTemplate uiRelation)
        {
            var quoteTemplate = Db.QuoteTemplates
                .Include(x => x.CostingTemplates)
                .ThenInclude(x => x.CostingTemplate)
                .Single(x => x.Id == uiRelation.QuoteTemplateId);

            var itemToRemove = quoteTemplate.CostingTemplates.Single(x => x.CostingTemplateId == uiRelation.CostingTemplateId);
            itemToRemove.Multiplier = uiRelation.Multiplier;
            Db.SaveChanges();
        }

        public void AddRelation(QuoteTemplateCostingTemplate uiRelation)
        {
            var quoteTemplate = Db.QuoteTemplates
                .Include(x => x.CostingTemplates)
                .ThenInclude(x => x.CostingTemplate)
                .Single(x => x.Id == uiRelation.QuoteTemplateId);

            quoteTemplate.CostingTemplates.Add(uiRelation);
            Db.SaveChanges();
        }

        public void AppendData(QuoteTemplateItem item)
        {
            item.AvaliableCostingTemplates = this.App.CostingTemplate.All();
        }

        public void Remove(int id)
        {
            var entity = Db.QuoteTemplates.First(x => x.Id == id);
            Db.QuoteTemplates.Remove(entity);
            Db.SaveChanges();
        }
    }
}
