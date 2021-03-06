﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ColdCallsTracker.Code.Data;
using ColdCallsTracker.Code.Data.Models;
using ColdCallsTracker.Code.Data.ViewModels;
using ColdCallsTracker.Code.Extensions;
using Microsoft.EntityFrameworkCore;

// ReSharper disable ReplaceWithSingleCallToFirstOrDefault

namespace ColdCallsTracker.Code.Services
{
    public class PhoneService : ServiceBase
    {
        public PhoneService(AppDbContext db, AppService appService) : base(db, appService) { }

        public void Save(PhoneEditItem item)
        {
            item.GetValidationErrors().ThrowIfHasErrors();

            var dbItem = new Phone();

            if (item.Id != 0)
            {
                dbItem = Db.Phones.Single(x => x.Id == item.Id);
            }
            else
            {
                dbItem.DateCreate = DateTime.Now;
                Db.Phones.Add(dbItem);
            }

            dbItem.CompanyId = item.CompanyId;
            dbItem.Number = item.Number;
            dbItem.Remarks = item.Remarks;
            dbItem.DateModify = DateTime.Now;

            Db.SaveChanges();

            this.App.Company.RefreshDateModify(dbItem.CompanyId);

            item.Id = dbItem.Id;
        }

        public Phone FindDuplicate(string number, int currentId)
        {
            var phone = Db.Phones
                .Where(x => x.Id != currentId)
                .Where(x => x.Number == number)
                .Include(x => x.Company)
                .FirstOrDefault();

            return phone;
        }

        public void DeletePhone(int id)
        {
            var phone = Db.Phones
                .Where(x => x.Id == id)
                .Include(x => x.CallRecords)
                .First();

            this.App.Company.RefreshDateModify(phone.CompanyId);

            Db.CallRecords.RemoveRange(phone.CallRecords);
            Db.Phones.Remove(phone);
            Db.SaveChanges();
        }

    }
}
