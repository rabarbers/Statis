using System;
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
            throw new NotImplementedException();
        }
    }
}
