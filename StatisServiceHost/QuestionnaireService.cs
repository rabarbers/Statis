﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Activation;
using System.Text;
using StatisServiceContracts;

namespace StatisServiceHost
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class QuestionnaireService: IQuestionnaireAdministrativeService
    {
        public Questionnaire GetQuestionnaire(string userName, string questionnaireName)
        {
            return HandleDb4o.GetQuestionnaire(questionnaireName);
        }

        public void StoreQuestionnaire(string userName, Questionnaire questionnaire)
        {
            HandleDb4o.StoreQuestionnaire(questionnaire);
        }

        public void DeleteQuestionnaire(string userName, string questionnaireName)
        {
            HandleDb4o.DeleteQuestionnaire(questionnaireName);
        }

        public IEnumerable<string> GetUserQuestionnaireList(string userName)
        {
            return HandleDb4o.GetUserQuestionnaireList(userName);
        }

        public IEnumerable<string> GetUserAnalysts(string userName)
        {
            return HandleDb4o.GetUserAnalysts(userName);
        }

        public IEnumerable<string> GetUserRespondents(string userName)
        {
            return HandleDb4o.GetUserRespondents(userName);
        }

        public bool AddAnalyst(string currentUserName, string analystUserName)
        {
            return HandleDb4o.AddAnalyst(currentUserName, analystUserName);
        }

        public void RemoveAnalyst(string currentUserName, string analystUserName)
        {
            HandleDb4o.RemoveAnalyst(currentUserName, analystUserName);
        }

        public bool AddRespondent(string currentUserName, string respondentEmail)
        {
            return HandleDb4o.AddRespondent(currentUserName, respondentEmail);
        }

        public void RemoveRespondent(string currentUserName, string respondentEmail)
        {
            HandleDb4o.RemoveRespondent(currentUserName, respondentEmail);
        }

        public void StoreFilledQuestionnaire(string userName, FilledQuestionnaire filled)
        {
            HandleDb4o.StoreFilledQuestionnaire(filled);
        }

        public FilledQuestionnaire GetFilledQuestionnaire(string userName, Guid id)
        {
            return HandleDb4o.GetFilledQuestionnaire(id);
        }
        
        public bool AuthenticateUser(string userName, string password)
        {
            return HandleDb4o.AuthenticateUser(userName, password);
        }
    }
}
