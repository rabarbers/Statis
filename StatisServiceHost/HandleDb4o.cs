﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Db4objects.Db4o;
using Db4objects.Db4o.Linq;
using StatisServiceContracts;
using User = StatisServiceContracts.User;

namespace StatisServiceHost
{
    /// <summary>
    /// HandleDb4o handles all data exchange with the database
    /// </summary>
    public class HandleDb4o
    {
        public readonly static string StoreYapFileName = Path.Combine("store.yap");
        private static IObjectContainer _database;
        private static readonly object _sync = new object();

        public static IObjectContainer Database
        {
            get
            {
                lock(_sync)
                {
                    return _database ?? (_database = GetDb(StoreYapFileName));
                }
            }
        }

        private static IObjectContainer GetDb(string dbFileName)
        {
            var config = Db4oEmbedded.NewConfiguration();
            config.Common.ObjectClass(typeof(Analyst)).CascadeOnUpdate(true);
            config.Common.ObjectClass(typeof(Administrator)).CascadeOnUpdate(true);
            config.Common.ObjectClass(typeof(Questionnaire)).CascadeOnDelete(true);
            config.Common.ObjectClass(typeof(FilledQuestionnaire)).CascadeOnDelete(true);
            return Db4oEmbedded.OpenFile(config, dbFileName);
        }


        /// <summary>
        /// Ensure we return a copy of the result from DB40
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

        public static Questionnaire GetUserQuestionnaire(string userName, string questionnaireName)
        {
            var loggedInUser =
                (from Analyst user in Database
                 where user.UserName == userName
                 select user).FirstOrDefault();
            
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

            if (loggedInUser != null)
            {
                var questionnaires =
                    (from Questionnaire q in loggedInUser.Questionnaires
                     select q.Name).ToList();

                var trustedAnalyst =
                    (from Analyst user in Database
                     where user.TrustedAnalysts.Contains(loggedInUser)
                     select user);

                foreach (var trusted in trustedAnalyst)
                {
                    questionnaires.AddRange(trusted.Questionnaires.Select(n => n.Name));
                }

                return questionnaires;
            }

            return new List<string>();
        }
    

        public static void StoreQuestionnaire(string userName, Questionnaire questionnaireToStore)
        {
            var db = Database;
            
            var loggedInUser =
                (from Analyst user in db
                 where user.UserName == userName
                 select user).FirstOrDefault();

            if (loggedInUser != null)
            {
                var q = GetQuestionnaire(questionnaireToStore.Name);
                loggedInUser.Questionnaires.Remove(q);
                DeleteQuestionnaire(db, questionnaireToStore.Name);
                loggedInUser.Questionnaires.Add(questionnaireToStore);
                db.Store(loggedInUser);
                db.Commit();
            }
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

        public static string GetUserEmail(string userName)
        {
            var loggedInUser =
                (from Analyst user in Database
                 where user.UserName == userName
                 select user.Email).FirstOrDefault();
            return loggedInUser;
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

        public static IEnumerable<string> GetUserRespondents(string userName)
        {
            var loggedInUser =
                (from Analyst user in Database
                 where user.UserName == userName
                 select user).FirstOrDefault();

            if (loggedInUser != null && loggedInUser.Respondents != null)
            {
                var questionnaires =
                    (from User respondent in loggedInUser.Respondents
                     select respondent.Email).ToList();
                return questionnaires;
            }
            return new List<string>();
        }

        // handling analyst users
        public static bool AddAnalyst(string currentUserName, string analystUserName)
        {
            var loggedInUser =
                (from Administrator user in Database
                 where user.UserName == currentUserName
                 select user).FirstOrDefault();

            var analystUser =
                (from Analyst user in Database
                 where user.UserName == analystUserName
                 select user).FirstOrDefault();

            if (loggedInUser != null && analystUser != null)
            {
                if(loggedInUser.TrustedAnalysts == null)
                {
                    loggedInUser.TrustedAnalysts = new List<Analyst>();
                }
                
                loggedInUser.TrustedAnalysts.Add(analystUser);
                Database.Store(loggedInUser);
                return true;
            }
            return false;
        }

        public static void RemoveAnalyst(string currentUserName, string analystUserName)
        {
            var loggedInUser =
                (from Analyst user in Database
                 where user.UserName == currentUserName
                 select user).FirstOrDefault();

            var analystUser =
                (from Analyst user in Database
                 where user.UserName == analystUserName
                 select user).FirstOrDefault();

            if (loggedInUser != null && analystUser != null)
            {
                if (loggedInUser.TrustedAnalysts != null)
                {
                    loggedInUser.TrustedAnalysts.Remove(analystUser);
                    Database.Store(loggedInUser);
                }
            }
        }

        public static bool AddRespondent(string currentUserName, string respondentEmail)
        {
            var loggedInUser =
                (from Analyst user in Database
                 where user.UserName == currentUserName
                 select user).FirstOrDefault();

            var respondentUser =
                (from User user in Database
                 where user.Email == respondentEmail
                 select user).FirstOrDefault();

            if (loggedInUser != null)
            {
                if (loggedInUser.Respondents == null)
                {
                    loggedInUser.Respondents = new List<User>();
                }

                if (respondentUser != null && !loggedInUser.Respondents.Where(n => n.Email == respondentEmail).Any())
                {
                    loggedInUser.Respondents.Add(respondentUser);
                    Database.Store(loggedInUser);
                    return true;
                }

                if (respondentUser == null)
                {
                    var u = new User(respondentEmail);
                    loggedInUser.Respondents.Add(u);
                    var db = Database;
                    db.Store(u);
                    db.Store(loggedInUser);
                    return true;
                }
            }
            return false;
        }

        public static void RemoveRespondent(string currentUserName, string respondentEmail)
        {
            var loggedInUser =
                (from Analyst user in Database
                 where user.UserName == currentUserName
                 select user).FirstOrDefault();

            var respondentUser =
                (from User user in Database
                 where user.Email == respondentEmail
                 select user).FirstOrDefault();

            if (loggedInUser != null)
            {
                if (loggedInUser.Respondents != null)
                {
                    loggedInUser.Respondents.Remove(respondentUser);
                    Database.Store(loggedInUser);
                }
            }
        }

        public static void StoreFilledQuestionnaire(FilledQuestionnaire filled)
        {
            var questionnaire = GetFilledQuestionnaire(filled.Id);
            if (questionnaire != null)
            {
                Database.Delete(questionnaire);
            }
            filled.FillingTime = DateTime.Now;
            Database.Store(filled);
        }

        public static IEnumerable<FilledQuestionnaireRecord> GetFilledQuestionnaireList(string userName, string questionnaireName)
        {
            var questionnaireQuery =
                    from FilledQuestionnaire q in Database
                    where q.QuestionnaireName == questionnaireName
                    select new FilledQuestionnaireRecord { Id = q.Id, FillingTime = q.FillingTime };
            return questionnaireQuery.ToList();
        }

        private static FilledQuestionnaire GetFilledQuestionnaire(Guid id)
        {
            var questionnaireQuery =
                    (from FilledQuestionnaire q in Database
                     where q.Id == id
                     select q);
            return questionnaireQuery.FirstOrDefault();
        }

        public static FilledQuestionnaire GetFilledQuestionnaire(string userName, Guid id)
        {
            var questionnaireQuery =
                    (from FilledQuestionnaire q in Database
                     where q.Id == id
                     select q);
            return questionnaireQuery.FirstOrDefault();
        }

        public static bool RegisterAnalyst(Analyst analyst)
        {
            var alreadyRegisteredUser =
            (from Analyst user in Database
             where user.UserName == analyst.UserName
             select user).FirstOrDefault();

            if (alreadyRegisteredUser == null)
            {
                Database.Store(analyst);
                return true;
            }

            return false;
        }

        public static bool AuthenticateUser(string userName, string password)
        {
            var loggedInUserPassword =
            (from Analyst user in Database
             where user.UserName == userName
             select user.Password).FirstOrDefault();

            return loggedInUserPassword == password;
        }

        // gets admin email
        /* 
        internal static string GetUserMail(string userName)
        {
            var loggedInUserMail =
                (from User user in Database
                 where user.userName == userName
                 select user.Email).FirstOrDefault();
            return loggedInUserMail;
        }*/

        /// <summary>
        /// Creates some test data
        /// </summary>
        /// <param name="dbFileName"></param>
        public static void LoadTestData(string dbFileName)
        {
            var questionnaire1 = new Questionnaire("Q1", "Test questionnaire");
            
            var choiceOpt1 = new TextChoice("Yes", 1);
            var choiceOpt2 = new TextChoice("No", 2);
            var choiceOpt3 = new TextChoice("Maybe", 3);
            var choiceList1 = new List<Choice>();
            choiceList1.Add(choiceOpt1);
            choiceList1.Add(choiceOpt2);
            choiceList1.Add(choiceOpt3);

            var question1 = new ChoiceQuestion("Is this a test?", choiceList1);
            question1.QuestionId = Guid.NewGuid();
            
            questionnaire1.Questions.Add(question1);

            var choiceOpt4 = new NumberChoice(1, 1);
            var choiceOpt5 = new NumberChoice(2, 2);
            var choiceOpt6 = new NumberChoice(100, 3);

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
            
            var choiceOpt7 = new TextChoice("Yes, why not", 1);
            var choiceOpt8 = new TextChoice("I don't care", 2);
            var choiceOpt9 = new TextChoice("No, it looks ugly", 3);

            var choiceList3 = new List<Choice>();
            choiceList3.Add(choiceOpt7);
            choiceList3.Add(choiceOpt8);
            choiceList3.Add(choiceOpt9);

            var question5 = new ImgChoiceQuestion("Should it be this logo?", img, choiceList3);
            question5.QuestionId = Guid.NewGuid();

            questionnaire2.Questions.Add(question5);

            var filledQuestionnaire1 = new FilledQuestionnaire(questionnaire1);
            var answer1 = new SingleChoiceAnswer
                              {
                                  Choice = 1
                              };
            var answer2 = new SingleChoiceAnswer
                              {
                                  Choice = 3
                              };
            filledQuestionnaire1.Answers.Add(answer1);
            filledQuestionnaire1.Answers.Add(answer2);

            var filledQuestionnaire2 = new FilledQuestionnaire(questionnaire2);
            var answer3 = new TextAnswer("I love testing");
            var answer4 = new TextAnswer("Definitely");
            var answer5 = new SingleChoiceAnswer
                              {
                                  Choice = 3
                              };

            filledQuestionnaire2.Answers.Add(answer3);
            filledQuestionnaire2.Answers.Add(answer4);
            filledQuestionnaire2.Answers.Add(answer5);

            var admin = new Administrator("jb", "Jānis", "Bērziņš", "mentor@delfi.lv", "go");
            var questionnaires = new List<Questionnaire>();
            questionnaires.Add(questionnaire1);
            questionnaires.Add(questionnaire2);
            admin.Questionnaires = questionnaires;


            var user2 = new Analyst("SysAnal", "A", "B", "test@test.lv", "go");

            admin.TrustedAnalysts = new List<Analyst>();
            admin.TrustedAnalysts.Add(user2);

            admin.Respondents = new List<User>();
            
            var db = GetDb(dbFileName);
            db.Store(admin);
            db.Store(user2);
            //db.Store(questionnaire1);
            //db.Store(questionnaire2);
            //db.Store(filledQuestionnaire1);
            //db.Store(filledQuestionnaire2);
            db.Close();
        }
    }
}                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  
