using System;
using System.Collections.Generic;
using System.Linq;
using ColdCallsTracker.Code.Data;
using ColdCallsTracker.Code.Data.Models;
using ColdCallsTracker.Code.Data.ViewModels;
using ColdCallsTracker.Code.Exceptions;
using ColdCallsTracker.Code.Extensions;
using ColdCallsTracker.Code.Utils;

namespace ColdCallsTracker.Code.Services
{
    public class QuoteService : ServiceBase
    {
        public QuoteService(AppDbContext db, AppService appService)
            : base(db, appService)
        { }

        public void Save(QuoteItem item)
        {
            item.GetValidationErrors().ThrowIfHasErrors();

            var dbItem = new Quote();

            if (item.Id != 0)
            {
                dbItem = Db.Quotes.Single(x => x.Id == item.Id);
            }
            else
            {
                dbItem.DateCreate = DateTime.Now;
                Db.Quotes.Add(dbItem);
            }

            dbItem.CompanyId = item.CompanyId;
            dbItem.Name = item.Name;
            dbItem.CustomDesign = item.CustomDesign;
            dbItem.DateModify = DateTime.Now;

            Db.SaveChanges();

            this.App.Company.RefreshDateModify(dbItem.CompanyId.Value);

            item.Id = dbItem.Id;
        }

        public void Rename(int quoteId, string newName)
        {
            if (!newName.HasValue())
                throw new ValidationException("Name required");

            var quote = new Quote { Id = quoteId };
            Db.Quotes.Attach(quote);
            quote.Name = newName;
            Db.SaveChanges();
        }

        public QuoteItem EmptyQuote(int companyId)
        {
            var quote = new Quote();
            quote.CompanyId = companyId;
            quote.Name = "Новая смета работ";
            quote.DateModify = DateTime.Now;
            quote.DateCreate = DateTime.Now;
            Db.Quotes.Add(quote);
            Db.SaveChanges();

            this.App.Company.RefreshDateModify(companyId);

            return Edit(quote.Id);
        }

        public QuoteItem Edit(int? id)
        {
            var editItem = new QuoteItem();

            if (id != 0)
            {
                var item = Db.Quotes
                    .Select(x => new QuoteItem
                    {
                        Id = x.Id,
                        Name = x.Name,
                        CompanyName = x.Company.Name,
                        CompanyId = x.CompanyId,
                        CustomDesign = x.CustomDesign
                    })
                    .FirstOrDefault(x => x.Id == id);
                return item;
            }

            return editItem;
        }

        public QuoteItem Get(int? id)
        {
            var companyId = this.Db.Quotes.Single(x => x.Id == id).CompanyId;
            return this.App.Company.Edit(companyId).Quotes.Single(x => x.Id == id);
        }

        public void SetDesign(int id, bool customDesign)
        {
            var quoteItem = this.Get(id);
            quoteItem.CustomDesign = customDesign;
            this.Save(quoteItem);
        }

        public (List<QuoteItem> items, int total, int filtered) Search(QuoteSearchParameters parameters)
        {
            var query = Db.Quotes
                .Select(x => new QuoteItem
                {
                    Id = x.Id,
                    Name = x.Name,
                    CompanyId = x.CompanyId,
                    CompanyName = x.Company.Name,
                    CustomDesign = x.CustomDesign
                });

            var total = query.Count();

            if (parameters.Id.HasValue)
                query = query.Where(x => x.Id == parameters.Id);

            if (parameters.CompanyId.HasValue)
                query = query.Where(x => x.CompanyId == parameters.CompanyId);

            if (parameters.Name.HasValue())
                query = query.Where(x => x.Name.ToLower().Contains(parameters.Name.ToLower()));

            var filtered = query.Count();

            var items = query
                .OrderBy(parameters.OrderBy, parameters.IsAsc)
                .TakePage(parameters.Skip.Value, parameters.Take.Value)
                .ToList();

            return (items, total, filtered);
        }

        public QuoteItem AddQuoteFromTemplate(int templateId, int companyId)
        {
            var template = this.App.QuoteTemplate.Get(templateId);
            template.Recalc();

            var newQuote = new Quote();
            newQuote.Name = template.Name;
            newQuote.CompanyId = companyId;
            newQuote.CustomDesign = template.CustomDesign;
            Db.Quotes.Add(newQuote);
            Db.SaveChanges();

            foreach (var relation in template.QuoteCostingRelations
                .OrderBy(x => ((CostingCategoryEnum)x.CostingTemplate.CategoryId).DescriptionAttr())
                .ThenBy(x => x.CostingTemplate.Name))
            {
                var costing = new Costing();
                costing.Name = relation.CostingTemplate.Name;
                costing.Multiplier = relation.Multiplier;
                costing.Qty = relation.CostingTemplate.Qty;
                costing.CategoryId = relation.CostingTemplate.CategoryId;
                costing.QuoteId = newQuote.Id;
                costing.Unit = relation.CostingTemplate.Unit;

                var calculatedTemplate = template.AvaliableCostingTemplates.Single(c => c.Id == relation.CostingTemplateId);
                costing.Cost = Math.Round(calculatedTemplate.Cost ?? 0);
                costing.Total = Math.Round(calculatedTemplate.Total ?? 0);

                Db.Costings.Add(costing);
            }

            Db.SaveChanges();

            this.App.Company.RefreshDateModify(companyId);

            return this.App.Company.Edit(companyId).Quotes.Single(x => x.Id == newQuote.Id);
        }


        public void Delete(int id)
        {
            var quote = Db.Quotes.Single(x => x.Id == id);
            var companyId = quote.CompanyId.Value;

            Db.Quotes.Remove(quote);
            Db.SaveChanges();

            this.App.Company.RefreshDateModify(companyId);
        }
    }
}
