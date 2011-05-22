﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace StatisServiceContracts
{
    #region Map interfaces

    /// <summary></summary>
    [ServiceContract]
    //[ServiceKnownType(typeof(Map))]
    public interface IQuestionnaireService
    {
        [OperationContract]
        Questionnaire GetQuestionnaire(string questionnaireName);
        [OperationContract]
        void StoreQuestionnaire(Questionnaire questionnaire);
        [OperationContract]
        void DeleteQuestionnaire(string questionnaireName);
    }

    /// <summary></summary>
    [ServiceContract]
    public interface IQuestionnaireAdministrativeService : IQuestionnaireService
    {
        
    }

    #endregion
}