using System;
using System.Collections.Generic;
using System.ServiceModel.Activation;
using StatisServiceContracts;

namespace StatisServiceHost
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class QuestionnaireService: IQuestionnaireAdministrativeService
    {
        public Questionnaire GetQuestionnaire(string questionnaireName)
        {
            return HandleDb4o.GetQuestionnaire(questionnaireName);
        }

        public void StoreQuestionnaire(Questionnaire questionnaire)
        {
            HandleDb4o.StoreQuestionnaire(questionnaire);
        }

        public void DeleteQuestionnaire(string questionnaireName)
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

        public void StoreFilledQuestionnaire(FilledQuestionnaire filled)
        {
            HandleDb4o.StoreFilledQuestionnaire(filled);
        }

        public FilledQuestionnaire GetFilledQuestionnaire(Guid id)
        {
            return HandleDb4o.GetFilledQuestionnaire(id);
        }
        
        public bool AuthenticateUser(string userName, string password)
        {
            return true;
        }

        public void SendQuestionnaires(List<string> mailingList, string fromUser, string text, string questName)
        {
            foreach(string mail in mailingList)
            {
                System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
                message.To.Add(mail);
                message.From = new System.Net.Mail.MailAddress(fromUser);
                message.Body = text;
                message.Subject = "Aicinājums aizpildīt anketu " + questName;
                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("smtp.gmail.com");
                smtp.Send(message);
            }
        }
    }
}
