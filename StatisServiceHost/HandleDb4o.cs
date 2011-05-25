using System;
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
        private static IObjectContainer _database;
            //Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "store.yap");

        public static IObjectContainer Database
        {
            get { return _database ?? (_database = GetDb(StoreYapFileName)); }
        }

        private static IObjectContainer GetDb(string dbFileName)
        {
            var config = Db4oEmbedded.NewConfiguration();
            config.Common.ObjectClass(typeof(Questionnaire)).CascadeOnDelete(true);
            return Db4oEmbedded.OpenFile(config, dbFileName);
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
            var questionnaire =
                 (from Questionnaire q in Database
                  where q.Name == questionnaireName
                  select q).FirstOrDefault();

            return questionnaire;
        }

        public static IEnumerable<string> GetUserQuestionnaireList(string userName)
        {
            var loggedInUser =
                (from Analyst user in Database
                 where user.UserName == userName
                 select user).FirstOrDefault();

            var questionnaires =
                (from Questionnaire q in loggedInUser.Questionnaires
                 select q.Name).ToList();

            return questionnaires;
        }

        public static void StoreQuestionnaire(Questionnaire questionnaireToStore)
        {
            var db = Database;
            DeleteQuestionnaire(db, questionnaireToStore.Name);
            db.Store(questionnaireToStore);
            db.Commit();
        }

        public static void DeleteQuestionnaire(IObjectContainer db, string questionnaireName)
        {
            var questionnaires =
                (from Questionnaire q in db
                 where q.Name == questionnaireName
                 select q).ToList();

            foreach (var questionnaire in questionnaires)
            {
                db.Delete(questionnaire);
            }
        }
        public static void DeleteQuestionnaire(string questionnaireName)
        {
            var db = Database;
            DeleteQuestionnaire(db, questionnaireName);
            db.Commit();
        }

        public static IEnumerable<string> GetUserAnalysts(string userName)
        {
            var loggedInUser =
                (from Analyst user in Database
                 where user.UserName == userName
                 select user).FirstOrDefault();

            if (loggedInUser != null)
            {
                var questionnaires =
                    (from Analyst analyst in loggedInUser.TrustedAnalysts
                     select analyst.UserName).ToList();
                return questionnaires;
            }
            return new List<string>();
        }

        public static void LoadTestData(string dbFileName)
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
            question1.QuestionId = Guid.NewGuid();
            
            questionnaire1.Questions.Add(question1);

            var choiceOpt4 = new NumberChoice(1);
            var choiceOpt5 = new NumberChoice(2);
            var choiceOpt6 = new NumberChoice(100);

            var choiceList2 = new List<Choice>();
            choiceList2.Add(choiceOpt4);
            choiceList2.Add(choiceOpt5);
            choiceList2.Add(choiceOpt6);

            var question2 = new ChoiceQuestion("How many tests are there in this application?", choiceList2);
            question2.QuestionId = Guid.NewGuid();
            questionnaire1.Questions.Add(question2);

            var questionnaire2 = new Questionnaire("Q2", "Public opinion on Statis testing");

            var question3 = new TextQuestion("What do you think about testing?");
            question3.QuestionId = Guid.NewGuid();
            questionnaire2.Questions.Add(question3);

            var question4 = new TextQuestion("Do you think Statis should have a logo?");
            question4.QuestionId = Guid.NewGuid();
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
            question5.QuestionId = Guid.NewGuid();

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

            var admin = new Administrator("jb", "Jānis", "Bērziņš", "mentor@delfi.lv");
            var questionnaires = new List<Questionnaire>();
            questionnaires.Add(questionnaire1);
            questionnaires.Add(questionnaire2);
            admin.Questionnaires = questionnaires;


            var user2 = new Analyst("SysAnal", "A", "B", "test@test.lv");

            admin.TrustedAnalysts = new List<Analyst>();
            admin.TrustedAnalysts.Add(user2);
            
            IObjectContainer db = GetDb(dbFileName);
            db.Store(admin);
            //db.Store(questionnaire1);
            //db.Store(questionnaire2);
            //db.Store(filledQuestionnaire1);
            //db.Store(filledQuestionnaire2);
            db.Close();
        }
    }
}