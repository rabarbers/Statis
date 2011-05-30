using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.ServiceModel.Activation;
using System.Text;
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

        public Questionnaire GetUserQuestionnaire(string userName, string questionnaireName)
        {
            return HandleDb4o.GetUserQuestionnaire(userName, questionnaireName);
        }

        public void StoreQuestionnaire(string userName, Questionnaire questionnaire)
        {
            HandleDb4o.StoreQuestionnaire(userName, questionnaire);
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

        public void StoreFilledQuestionnaire(FilledQuestionnaire filled)
        {
            HandleDb4o.StoreFilledQuestionnaire(filled);
        }

        public IEnumerable<FilledQuestionnaireRecord> GetFilledQuestionnaireList(string userName, string questionnaireName)
        {
            return HandleDb4o.GetFilledQuestionnaireList(userName, questionnaireName);
        }

        public FilledQuestionnaire GetFilledQuestionnaire(string userName, Guid id)
        {
            return HandleDb4o.GetFilledQuestionnaire(userName, id);
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

        public void SendQuestionnaireToRespondents(string currentUserName, string text, string questionnaireName)
        {
            var recipients = GetUserRespondents(currentUserName);
            var userEmail = HandleDb4o.GetUserEmail(currentUserName);
            
            foreach (var recipientEmail in recipients)
            {
                var message = new MailMessage();
                message.To.Add(recipientEmail);
                message.From = new MailAddress(userEmail);
                message.Body = text;
                message.BodyEncoding = Encoding.UTF8;
                message.IsBodyHtml = true;
                message.Priority = MailPriority.Normal;
                message.Subject = "Aicinājums aizpildīt anketu " + questionnaireName;

                var client = new SmtpClient("smtp.gmail.com", 587);

                client.Credentials = new NetworkCredential("YOUR EMAIL", "YOUR PASSWORD");
                client.EnableSsl = true;
                
                try
                {
                    client.Send(message);
                }
                catch (SmtpException)
                {
                    //Catch errors...
                }
            }
        }
    }
}
