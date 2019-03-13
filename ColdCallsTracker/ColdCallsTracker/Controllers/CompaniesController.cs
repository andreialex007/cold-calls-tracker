﻿using System.Collections.Generic;
using ColdCallsTracker.Code.Data.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace ColdCallsTracker.Controllers
{
    public class CompaniesController : AppControllerBase
    {
        public CompaniesController(IHostingEnvironment hostingEnvironment) : base(hostingEnvironment)
        {
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View("~/Pages/Companies/Index.cshtml");
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            var items = this.Service.QuoteTemplate.All();
            return View("~/Pages/Companies/Edit.cshtml", items);
        }

        [HttpGet]
        public ActionResult Load(int id)
        {
            var company = Service.Company.Edit(id);
            return Json(company);
        }

        [HttpGet]
        public ActionResult DeletePhone(int id)
        {
            Service.Phone.DeletePhone(id);
            return Json(new { });
        }

        [HttpPost]
        public ActionResult Save([FromBody] CompanyEditItem item)
        {
            if (item.StateId == 0)
                item.StateId = null;
            Service.Company.Save(item);
            return Json(item);
        }

        [HttpPost]
        public ActionResult AddRecord([FromForm] string description, [FromForm] int phoneId)
        {
            var item = Service.CallRecord.AddRecord(phoneId, description);
            return Json(item);
        }

        [HttpPost]
        public ActionResult EditPhone([FromBody] PhoneEditItem item)
        {
            Service.Phone.Save(item);
            return Json(item);
        }

        [HttpGet]
        public ActionResult FindPhoneDuplicate(int id, string phone)
        {
            var duplicate = Service.Phone.FindDuplicate(phone, id);
            if (duplicate != null)
            {
                return Json(new
                {
                    number = duplicate.Number,
                    company = duplicate.Company.Name,
                    duplicate.CompanyId,
                    hasDuplicate = true
                });
            }
            return Json(new
            {
                hasDuplicate = false
            });
        }

        [HttpPost]
        public ActionResult Search([FromBody] CompanySearchParameters parameters)
        {
            var (items, total, filtered) = Service.Company.Search(parameters);
            return Json(new
            {
                items,
                total,
                filtered
            });
        }

        [HttpGet]
        public ActionResult NewEmptyQuote(int companyId)
        {
            var quote = Service.Quote.EmptyQuote(companyId);
            return Json(quote);
        }

        [HttpGet]
        public ActionResult RenameQuote(int quoteId, string name)
        {
            this.Service.Quote.Rename(quoteId, name);
            return Json(new { result = "Ok" });
        }

        [HttpPost]
        public ActionResult SaveCosting([FromBody] CostingItem item)
        {
            this.Service.Costing.Save(item);
            return Json(this.Service.Quote.Get(item.QuoteId));
        }

        [HttpGet]
        public ActionResult ChangeDesign(int quoteId, bool individualDesign)
        {
            this.Service.Quote.SetDesign(quoteId,individualDesign);
            return Json(this.Service.Quote.Get(quoteId));
        }

        [HttpGet]
        public ActionResult DeleteCosting(int id)
        {
            var quoteId = this.Service.Costing.GetQuoteIdByCosting(id);
            this.Service.Costing.Delete(id);
            return Json(this.Service.Quote.Get(quoteId));
        }

        public ActionResult AddQuoteFromTemplate(int templateId, int companyId)
        {
            var templatedQuote = Service.Quote.AddQuoteFromTemplate(templateId, companyId);
            return Json(templatedQuote);
        }

        public ActionResult DeleteQuote(int id)
        {
            Service.Quote.Delete(id);
            return Json(new { result = "Ok" });
        }

    }
}
