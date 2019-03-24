using System;
using System.Collections.Generic;
using System.Linq;
using ColdCallsTracker.Code.Data;
using ColdCallsTracker.Code.Data.Models;
using ColdCallsTracker.Code.Data.ViewModels;
using ColdCallsTracker.Code.Extensions;

namespace ColdCallsTracker.Code.Services
{
    public class CompanyService : ServiceBase
    {
        public CompanyService(AppDbContext db, AppService appService) : base(db, appService) { }


        public void SaveCompanyFromExport(Company company)
        {
            var phone = company.Phones.First();
            var duplicate = this.App.Phone.FindDuplicate(phone.Number, phone.Id);
            if (duplicate != null)
                return;
            var newCompany = new Company
            {
                ActivityType = company.ActivityType,
                Address = company.Address,
                Name = company.Name,
                Remarks = company.Remarks,
                WebSites = company.WebSites
            };
            Db.Companies.Add(newCompany);
            Db.SaveChanges();
            phone.CompanyId = newCompany.Id;
            phone.Remarks = "Из парсинга";
            Db.Phones.Add(phone);
            Db.SaveChanges();
        }

        public CompanyEditItem Edit(int? id)
        {
            var editItem = new CompanyEditItem();

            if (id != 0)
            {
                var company = Db.Companies
                    .Select(x => new CompanyEditItem
                    {
                        Id = x.Id,
                        State = x.State.Name,
                        Name = x.Name,
                        StateId = x.StateId,
                        Remarks = x.Remarks,
                        ActivityType = x.ActivityType,
                        Address = x.Address,
                        WebSites = x.WebSites,
                        Phones = x.Phones
                            .Select(p => new PhoneEditItem
                            {
                                Id = p.Id,
                                Remarks = p.Remarks,
                                DateCreate = p.DateCreate,
                                DateModify = p.DateModify,
                                Number = p.Number,
                                CompanyId = p.CompanyId
                            })
                            .OrderBy(n => n.Number)
                            .ToList(),
                        Records = x.Phones.SelectMany(i => i.CallRecords)
                            .OrderByDescending(s => s.DateCreate)
                            .Select(r => new CallRecordItem
                            {
                                Id = r.Id,
                                Content = r.Content,
                                Phone = r.Phone.Number,
                                DateCreate = r.DateCreate,
                                PhoneId = r.PhoneId
                            })
                            .ToList(),
                        Quotes = x.Quotes
                            .Select(q => new QuoteItem
                            {
                                Id = q.Id,
                                DateModify = q.DateModify,
                                Name = q.Name,
                                CustomDesign = q.CustomDesign,
                                DateCreate = q.DateCreate,
                                CompanyId = q.CompanyId,
                                Costings = q.Costings
                                    .Select(c => new CostingItem
                                    {
                                        Total = c.Total,
                                        Cost = c.Cost,
                                        Id = c.Id,
                                        CategoryId = c.CategoryId,
                                        DateModify = c.DateModify,
                                        Name = c.Name,
                                        Unit = c.Unit,
                                        DateCreate = c.DateCreate,
                                        Qty = c.Qty,
                                        Multiplier = c.Multiplier,
                                        QuoteId = c.QuoteId
                                    })
                                    .OrderBy(s => s.CategoryId)
                                    .ToList()
                            })
                            .ToList()
                    })
                    .FirstOrDefault(x => x.Id == id);


                company.Quotes.ForEach(x => x.Costings = x.Costings.OrderBy(c => c.CategoryName).ThenBy(c => c.Name).ToList());

                return company;
            }


            return editItem;
        }

        public void Save(CompanyEditItem item)
        {
            item.GetValidationErrors().ThrowIfHasErrors();

            var company = new Company();

            if (item.Id != 0)
            {
                company = Db.Companies.Single(x => x.Id == item.Id);
            }
            else
            {
                company.DateCreate = DateTime.Now;
                Db.Companies.Add(company);
            }

            company.StateId = item.StateId;
            company.Name = item.Name;
            company.ActivityType = item.ActivityType;
            company.Address = item.Address;
            company.Remarks = item.Remarks;
            company.WebSites = item.WebSites;
            company.DateModify = DateTime.Now;

            Db.SaveChanges();

            item.Id = company.Id;
        }

        public (List<CompanyListItem> items, int total, int filtered) Search(CompanySearchParameters parameters)
        {
            var query = Db.Companies
                .Select(x => new CompanyListItem
                {
                    Id = x.Id,
                    Name = x.Name,
                    State = x.State.Name,
                    StateId = x.StateId,
                    ActivityType = x.ActivityType,
                    Address = x.Address,
                    Remarks = x.Remarks,
                    WebSites = x.WebSites,
                    LastCallRecordDate = x.Phones.Select(p => p.DateModify)
                        .Union(x.Phones.Select(p => p.DateModify))
                        .Union(x.Quotes.Select(p => p.DateModify))
                        .Union(x.Quotes.SelectMany(w => w.Costings.Select(k => k.DateModify)))
                        .Union(new List<DateTime> { x.DateModify })
                        .Union(x.Phones.SelectMany(r => r.CallRecords.Select(n => n.DateModify)))
                        .OrderByDescending(s => s)
                        .FirstOrDefault(),
                    PhoneNumbersList = x.Phones
                        .Select(n => n.Number)
                        .ToList(),

                });

            var total = query.Count();

            if (parameters.Id.HasValue)
                query = query.Where(x => x.Id == parameters.Id);

            if (parameters.Name.HasValue())
                query = query.Where(x => x.Name.ToLower().Contains(parameters.Name.ToLower()));

            if (parameters.ActivityType.HasValue())
                query = query.Where(x => x.ActivityType.ToLower().Contains(parameters.ActivityType.ToLower()));

            if (parameters.PhoneNumbers.HasValue())
                query = query.Where(x => x.PhoneNumbers.ToLower().Contains(parameters.PhoneNumbers.ToLower()));

            if (parameters.Remarks.HasValue())
                query = query.Where(x => x.Remarks.ToLower().Contains(parameters.Remarks.ToLower()));

            if (parameters.WebSites.HasValue())
                query = query.Where(x => x.WebSites.ToLower().Contains(parameters.WebSites.ToLower()));

            if (parameters.StateId.HasValue)
            {
                if (parameters.StateId == 0) parameters.StateId = null;
                query = query.Where(x => x.StateId == parameters.StateId);
            }

            if (parameters.LastCallRecordDateFrom.HasValue)
                query = query.Where(x => x.LastCallRecordDate >= parameters.LastCallRecordDateFrom);

            if (parameters.LastCallRecordDateTo.HasValue)
                query = query.Where(x => x.LastCallRecordDate >= parameters.LastCallRecordDateTo);

            var filtered = query.Count();


            var items = query
                .OrderBy(parameters.OrderBy, parameters.IsAsc)
                .TakePage(parameters.Skip.Value, parameters.Take.Value)
                .ToList();

            return (items, total, filtered);
        }

        public void Delete(int id)
        {
            var companyEditItem = this.App.Company.Edit(id);
            foreach (var quoteItem in companyEditItem.Quotes)
                this.App.Quote.Delete(quoteItem.Id);

            foreach (var phone in companyEditItem.Phones)
                this.App.Phone.DeletePhone(phone.Id);

            Db.Delete<Company>(x => x.Id == id);
            Db.SaveChanges();
        }
    }
}
