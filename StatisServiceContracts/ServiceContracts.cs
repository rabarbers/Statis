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
    [ServiceKnownType(typeof(FilledQuestionnaire))]
    [ServiceKnownType(typeof(Guid))]
    public interface IQuestionnaireService
    {
        [OperationContract]
        Questionnaire GetQuestionnaire(string userName, string questionnaireName);
        [OperationContract(IsOneWay = true)]
        void StoreQuestionnaire(string userName, Questionnaire questionnaire);
        [OperationContract(IsOneWay = true)]
        void DeleteQuestionnaire(string userName, string questionnaireName);
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
        [OperationContract(IsOneWay = true)]
        void StoreFilledQuestionnaire(string userName, FilledQuestionnaire filled);
        [OperationContract]
        FilledQuestionnaire GetFilledQuestionnaire(string userName, Guid id);
        [OperationContract]
        bool AuthenticateUser(string userName, string password);
        [OperationContract(IsOneWay = true)]
        void SendQuestionnaireToRespondents(string currentUserName, string message);
    }

    /// <summary></summary>
    [ServiceContract]
    public interface IQuestionnaireAdministrativeService : IQuestionnaireService
    {
        
    }

    #endregion
}
