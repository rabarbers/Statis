using System;
using System.Collections.Generic;
using System.ServiceModel.Activation;
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
        /*public string GetUserMail(string userName)
        
        {
            return HandleDb4o.GetUserMail(userName);
        }*/

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

        public bool RegisterUser(string userName, string password, string firstName, string lastName, string email)
        {
            var user = new Analyst(userName, firstName, lastName, email, password);
            return HandleDb4o.RegisterAnalyst(user);
        }
        
        public bool AuthenticateUser(string userName, string password)
        {
            return HandleDb4o.AuthenticateUser(userName, password);
        }

        public void SendQuestionnaireToRespondents(string currentUserName, string userMail, string text, string questionnaireName)
        {
            var recipients = GetUserRespondents(currentUserName);
            // var userMail = GetUserMail(currentUserName);
            foreach (var recipientEmail in recipients)
            {
                try
                {
                    System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
                    message.To.Add(recipientEmail);
                    message.From = new System.Net.Mail.MailAddress(userMail);
                    message.Body = text;
                    message.Subject = "Aicinājums aizpildīt anketu " + questionnaireName;
                    System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("smtp.gmail.com");
                    smtp.Send(message);
                }
                catch 
                {
                    
                }
            }
        }
    }
}
