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
    public class CallQuestionService : ServiceBase
    {
        public CallQuestionService(AppDbContext db, AppService appService) : base(db, appService)
        {
        }

        public void Save(CallQuestionItem uiQuestion)
        {
            CallQuestion dbQuestion;
            if (uiQuestion.Id == 0)
            {
                dbQuestion = new CallQuestion();
                Db.CallQuestions.Add(dbQuestion);
            }
            else
            {
                dbQuestion = Db.CallQuestions.Single(x => x.Id == uiQuestion.Id);
            }

            dbQuestion.Text = uiQuestion.Text;
            dbQuestion.CallScriptId = uiQuestion.CallScriptId;
            Db.SaveChanges();
            uiQuestion.Id = dbQuestion.Id;
        }

    }
}
