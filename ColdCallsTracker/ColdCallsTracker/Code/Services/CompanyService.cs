using System.Collections.Generic;
using System.Linq;
using ColdCallsTracker.Code.Data;
using ColdCallsTracker.Code.Data.ViewModels;
using StringExtensions;

namespace ColdCallsTracker.Code.Services
{
    public class CompanyService : ServiceBase
    {
        public CompanyService(AppDbContext db, AppService appService) : base(db, appService)
        {
        }


        public (List<CompanyListItem> items, int total) Search(CompanySearchParameters parameters)
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
                        .SingleOrDefault() ,
                    PhoneNumbersList = x.Phones.Select(n => n.Number).ToList(),
                });

            if (!parameters.Name.IsEmptyOrWhiteSpace())
                query = query.Where(x => x.Name.ToLower().Contains(parameters.Name.ToLower()));


            var items = query.ToList();
            var total = query.Count();

            return (items, total);
        }
    }
}
