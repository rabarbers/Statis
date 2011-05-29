﻿using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace StatisServiceContracts
{
    #region Statis service interfaces

    /// <summary>
    /// ServiceContracts represents the functionality of the Questionnaire 
    /// and User classes
    /// </summary>
    [ServiceContract]
    [ServiceKnownType(typeof(Questionnaire))]
    [ServiceKnownType(typeof(FilledQuestionnaire))]
    [ServiceKnownType(typeof(Guid))]
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
        [OperationContract(IsOneWay = true)]
        void StoreFilledQuestionnaire(FilledQuestionnaire filled);
        [OperationContract]
        FilledQuestionnaire GetFilledQuestionnaire(Guid id);
        [OperationContract]
        bool AuthenticateUser(string userName, string password);
    }

    /// <summary></summary>
    [ServiceContract]
    public interface IQuestionnaireAdministrativeService : IQuestionnaireService
    {
        
    }

    #endregion
}
