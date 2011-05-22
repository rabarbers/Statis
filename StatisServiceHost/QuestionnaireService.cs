using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StatisServiceContracts;

namespace StatisServiceHost
{
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
            throw new NotImplementedException();
        }
    }
}
