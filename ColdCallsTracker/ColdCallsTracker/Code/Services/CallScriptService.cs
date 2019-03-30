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
    public class CallScriptService : ServiceBase
    {
        public CallScriptService(AppDbContext db, AppService appService) : base(db, appService)
        {
        }

        public List<CallScriptItem> All()
        {
            return Db.CallScripts
                 .Select(x => new CallScriptItem
                 {
                     Name = x.Name,
                     Id = x.Id
                 })
                 .ToList();
        }

        public CallScriptItem New()
        {
            var newScript = new CallScript
            {
                DateCreate = DateTime.Now,
                DateModify = DateTime.Now,
                Name = "Новый скрипт"
            };
            Db.CallScripts.Add(newScript);
            Db.SaveChanges();

            return Get(newScript.Id);
        }

        public CallScriptItem Get(int id)
        {
            var includableQueryable = Db.CallScripts.Include(x => x.CallQuestions);

            var item = includableQueryable
                .Select(x => new CallScriptItem
                {
                    Id = x.Id,
                    Name = x.Name,
                    DateModify = x.DateModify,
                    DateCreate = x.DateCreate,
                    CallQuestions = x.CallQuestions.OrderBy(e => e.Text).Select(q => new CallQuestionItem
                    {
                        Id = q.Id,
                        Text = q.Text,
                        CallScriptId = q.CallScriptId,
                        CallAnswers = q.CallAnswers.Select(a => new CallAnswerItem
                        {
                            Id = a.Id,
                            Text = a.Text,
                            FromQuestionId = a.FromQuestionId,
                            ToQuestionId = a.ToQuestionId
                        }).OrderBy(f => f.Text).ToList()
                    }).ToList()
                })
                .FirstOrDefault(x => x.Id == id);

            return item;
        }

        public void SetName(int id, string name)
        {
            if (!name.HasValue())
                throw new Exception("name must be provided");

            var script = Db.CallScripts.Single(x => x.Id == id);
            script.Name = name;
            Db.SaveChanges();
        }

        public void Delete(int id)
        {
            var scriptItem = this.App.CallScript.Get(id);
            foreach (var question in scriptItem.CallQuestions)
                this.App.CallQuestion.Delete(question.Id);

            this.DeleteById<CallScript>(scriptItem.Id);
        }

    }
}
