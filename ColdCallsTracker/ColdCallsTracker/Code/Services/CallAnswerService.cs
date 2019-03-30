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
    public class CallAnswerService : ServiceBase
    {
        public CallAnswerService(AppDbContext db, AppService appService) : base(db, appService)
        {
        }



        public void Save(CallAnswerItem uiAnswer)
        {
            CallAnswer dbAnswer;
            if (uiAnswer.Id == 0)
            {
                dbAnswer = new CallAnswer();
                Db.CallAnswers.Add(dbAnswer);
            }
            else
            {
                dbAnswer = Db.CallAnswers.Single(x => x.Id == uiAnswer.Id);
            }

            dbAnswer.Text = uiAnswer.Text;
            dbAnswer.FromQuestionId = uiAnswer.FromQuestionId;
            dbAnswer.ToQuestionId = uiAnswer.ToQuestionId;
            Db.SaveChanges();
            uiAnswer.Id = dbAnswer.Id;
        }


    }
}
