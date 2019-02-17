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

        public CompanyEditItem Edit(int? id)
        {
            var editItem = new CompanyEditItem();

            if (id != null)
            {
                var company = Db.Companies
                    .Select(x => new CompanyEditItem
                    {
                        State = x.State.Name,
                        Name = x.Name,
                        StateId = x.StateId,
                        // Phones = x.Phones.Select(x=> new)
                    })
                    .Single();


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
                Db.Companies.Add(company);
            }

            company.StateId = item.StateId;
            company.Name = item.Name;
            company.ActivityType = item.ActivityType;
            company.Remarks = item.Remarks;
            company.WebSites = item.WebSites;

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
                    Remarks = x.Remarks,
                    WebSites = x.WebSites,
                    LastCallRecordDate = x.Phones
                        .SelectMany(p => p.CallRecords)
                        .OrderBy(d => d.DateModify)
                        .Select(s => s.DateModify)
                        .SingleOrDefault(),
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
                query = query.Where(x => x.StateId == parameters.StateId);

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
    }
}
