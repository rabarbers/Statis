using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace StatisServiceContracts
{
    #region Statis service interfaces

    /// <summary></summary>
    [ServiceContract]
    [ServiceKnownType(typeof(Questionnaire))]
    public interface IQuestionnaireService
    {
        [OperationContract]
        Questionnaire GetQuestionnaire(string questionnaireName);
        [OperationContract(IsOneWay = true)]
        void StoreQuestionnaire(Questionnaire questionnaire);
        [OperationContract(IsOneWay = true)]
        void DeleteQuestionnaire(string questionnaireName);
        [OperationContract]
        IEnumerable<string> GetUserQuestionnaireList(string userName);
        [OperationContract]
        IEnumerable<string> GetUserAnalysts(string userName);
        [OperationContract]
        IEnumerable<string> GetUserRespondents(string userName);
        [OperationContract]
        bool AddAnalyst(string currentUserName, string analystUserName);
        [OperationContract(IsOneWay = true)]
        void RemoveAnalyst(string currentUserName, string analystUserName);
        [OperationContract]
        bool AddRespondent(string currentUserName, string respondentEmail);
        [OperationContract(IsOneWay = true)]
        void RemoveRespondent(string currentUserName, string respondentEmail);
    }

    /// <summary></summary>
    [ServiceContract]
    public interface IQuestionnaireAdministrativeService : IQuestionnaireService
    {
        
    }

    #endregion
}
