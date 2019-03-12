﻿using System;
using System.Collections.Generic;
using System.Linq;
using ColdCallsTracker.Code.Data;
using ColdCallsTracker.Code.Data.Models;
using ColdCallsTracker.Code.Data.ViewModels;
using ColdCallsTracker.Code.Exceptions;
using ColdCallsTracker.Code.Extensions;
using Microsoft.EntityFrameworkCore;

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
            dbItem.DateModify = DateTime.Now;

            Db.SaveChanges();

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
                        CompanyId = x.CompanyId
                    })
                    .FirstOrDefault(x => x.Id == id);
                return item;
            }

            return editItem;
        }

        public (List<QuoteItem> items, int total, int filtered) Search(QuoteSearchParameters parameters)
        {
            var query = Db.Quotes
                .Select(x => new QuoteItem
                {
                    Id = x.Id,
                    Name = x.Name,
                    CompanyId = x.CompanyId,
                    CompanyName = x.Company.Name
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

            var newQuote = new Quote();
            newQuote.Name = template.Name;
            newQuote.CompanyId = companyId;
            newQuote.CustomDesign = template.CustomDesign;
            Db.Quotes.Add(newQuote);
            Db.SaveChanges();

            foreach (var relation in template.QuoteCostingRelations)
            {
                var costing = new Costing();
                costing.Name = relation.CostingTemplate.Name;
                costing.Qty = relation.CostingTemplate.Qty;
                costing.Cost = relation.CostingTemplate.Cost;
                costing.Multiplier = relation.Multiplier;
                costing.Total = relation.CostingTemplate.Total;
                costing.CategoryId = relation.CostingTemplate.CategoryId;
                costing.QuoteId = newQuote.Id;
                costing.Unit = relation.CostingTemplate.Unit;
                Db.Costings.Add(costing);
            }

            Db.SaveChanges();
            return this.App.Company.Edit(companyId).Quotes.Single(x => x.Id == newQuote.Id);
        }


        public void Delete(int id)
        {
            Db.Delete<Quote>(x => x.Id == id);
            Db.SaveChanges();
        }
    }
}
