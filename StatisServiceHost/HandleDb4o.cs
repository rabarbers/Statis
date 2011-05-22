﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using Db4objects.Db4o;
using Db4objects.Db4o.Linq;
using StatisServiceContracts;

namespace StatisServiceHost
{
    public class HandleDb4o
    {
        readonly static string StoreYapFileName = Path.Combine(@"D:\", "store.yap");
            //Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "store.yap");

        private static IObjectContainer GetDb()
        {
            return Db4oEmbedded.OpenFile(Db4oEmbedded.NewConfiguration(), StoreYapFileName);
        }


        /// <summary>
        /// Ensure we return a copy of the result from DB40
        /// 
        /// DB40 does not have a distinct implementation by default
        /// To stop it returning the categories assigned to the basket products
        /// Create an equality comparison on the Category name
        /// </summary>
        /// <returns></returns>
        public static Questionnaire GetQuestionnaire(string questionnaireName)
        {
            var db = GetDb();
            try
            {
                var questionnaire =
                (from Questionnaire q in db
                 where q.Name == questionnaireName
                 select q).FirstOrDefault();

                return questionnaire;
            }
            finally
            {
                db.Close();
            }
        }

        public static void StoreQuestionnaire(Questionnaire questionnaireToStore)
        {
            var db = GetDb();
            try
            {
                var questionnaire =
                (from Questionnaire q in db
                 where q.Name == questionnaireToStore.Name
                 select q).FirstOrDefault();

                if(questionnaire != null)
                {
                    questionnaire.Description = questionnaireToStore.Description;
                    questionnaire.Name = questionnaireToStore.Name;
                    questionnaire.Questions.Clear();
                    questionnaire.Questions.AddRange(questionnaireToStore.Questions);
                    db.Store(questionnaire);
                }
                else
                {
                    db.Store(questionnaireToStore);
                }
                db.Commit();
                
            }
            finally
            {
                db.Close();
            }
        }

        public static void LoadTestData()
        {
            var questionnaire1 = new Questionnaire("Q1", "Test questionnaire");
            
            var choiceOpt1 = new TextChoice("Yes");
            var choiceOpt2 = new TextChoice("No");
            var choiceOpt3 = new TextChoice("Maybe");
            var choiceList1 = new List<Choice>();
            choiceList1.Add(choiceOpt1);
            choiceList1.Add(choiceOpt2);
            choiceList1.Add(choiceOpt3);

            var question1 = new ChoiceQuestion("Is this a test?", choiceList1);
            
            questionnaire1.Questions.Add(question1);

            var choiceOpt4 = new NumberChoice(1);
            var choiceOpt5 = new NumberChoice(2);
            var choiceOpt6 = new NumberChoice(100);

            var choiceList2 = new List<Choice>();
            choiceList2.Add(choiceOpt4);
            choiceList2.Add(choiceOpt5);
            choiceList2.Add(choiceOpt6);

            var question2 = new ChoiceQuestion("How many tests are there in this application?", choiceList2);

            var questionnaire2 = new Questionnaire("Q2", "Public opinion on Statis testing");

            var question3 = new TextQuestion("What do you think about testing?");
            questionnaire2.Questions.Add(question3);

            var question4 = new TextQuestion("Do you think Statis should have a logo?");
            questionnaire2.Questions.Add(question4);

            var img = new object();
            img = null;
            
            var choiceOpt7 = new TextChoice("Yes, why not");
            var choiceOpt8 = new TextChoice("I don't care");
            var choiceOpt9 = new TextChoice("No, it looks ugly");

            var choiceList3 = new List<Choice>();
            choiceList3.Add(choiceOpt7);
            choiceList3.Add(choiceOpt8);
            choiceList3.Add(choiceOpt9);

            var question5 = new ImgChoiceQuestion("Should it be this logo?", img, choiceList3);

            questionnaire2.Questions.Add(question5);

            var filledQuestionnaire1 = new FilledQuestionnaire(questionnaire1);
            var answer1 = new ChoiceAnswer(0);
            var answer2 = new ChoiceAnswer(2);
            filledQuestionnaire1.AddAnswer(answer1);
            filledQuestionnaire1.AddAnswer(answer2);

            var filledQuestionnaire2 = new FilledQuestionnaire(questionnaire2);
            var answer3 = new TextAnswer("I love testing");
            var answer4 = new TextAnswer("Definitely");
            var answer5 = new ChoiceAnswer(2);

            filledQuestionnaire2.AddAnswer(answer3);
            filledQuestionnaire2.AddAnswer(answer4);
            filledQuestionnaire2.AddAnswer(answer5);

            IObjectContainer db = GetDb();
            db.Store(questionnaire1);
            db.Store(questionnaire2);
            db.Store(filledQuestionnaire1);
            db.Store(filledQuestionnaire2);
            db.Close();
        }
    }
}