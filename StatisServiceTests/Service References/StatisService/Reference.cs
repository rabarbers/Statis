﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.225
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace StatisServiceTests.StatisService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="StatisService.IQuestionnaireService")]
    public interface IQuestionnaireService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IQuestionnaireService/GetQuestionnaire", ReplyAction="http://tempuri.org/IQuestionnaireService/GetQuestionnaireResponse")]
        StatisServiceContracts.Questionnaire GetQuestionnaire(string questionnaireName);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IQuestionnaireService/StoreQuestionnaire")]
        void StoreQuestionnaire(StatisServiceContracts.Questionnaire questionnaire);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IQuestionnaireService/DeleteQuestionnaire")]
        void DeleteQuestionnaire(string questionnaireName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IQuestionnaireService/GetUserQuestionnaireList", ReplyAction="http://tempuri.org/IQuestionnaireService/GetUserQuestionnaireListResponse")]
        string[] GetUserQuestionnaireList(string userName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IQuestionnaireService/GetUserAnalysts", ReplyAction="http://tempuri.org/IQuestionnaireService/GetUserAnalystsResponse")]
        string[] GetUserAnalysts(string userName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IQuestionnaireService/GetUserRespondents", ReplyAction="http://tempuri.org/IQuestionnaireService/GetUserRespondentsResponse")]
        string[] GetUserRespondents(string userName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IQuestionnaireService/AddAnalyst", ReplyAction="http://tempuri.org/IQuestionnaireService/AddAnalystResponse")]
        bool AddAnalyst(string currentUserName, string analystUserName);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IQuestionnaireService/RemoveAnalyst")]
        void RemoveAnalyst(string currentUserName, string analystUserName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IQuestionnaireService/AddRespondent", ReplyAction="http://tempuri.org/IQuestionnaireService/AddRespondentResponse")]
        bool AddRespondent(string currentUserName, string respondentEmail);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IQuestionnaireService/RemoveRespondent")]
        void RemoveRespondent(string currentUserName, string respondentEmail);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IQuestionnaireService/StoreFilledQuestionnaire")]
        void StoreFilledQuestionnaire(StatisServiceContracts.FilledQuestionnaire filled);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IQuestionnaireService/GetFilledQuestionnaire", ReplyAction="http://tempuri.org/IQuestionnaireService/GetFilledQuestionnaireResponse")]
        StatisServiceContracts.FilledQuestionnaire GetFilledQuestionnaire(System.Guid id);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IQuestionnaireServiceChannel : StatisServiceTests.StatisService.IQuestionnaireService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class QuestionnaireServiceClient : System.ServiceModel.ClientBase<StatisServiceTests.StatisService.IQuestionnaireService>, StatisServiceTests.StatisService.IQuestionnaireService {
        
        public QuestionnaireServiceClient() {
        }
        
        public QuestionnaireServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public QuestionnaireServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public QuestionnaireServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public QuestionnaireServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public StatisServiceContracts.Questionnaire GetQuestionnaire(string questionnaireName) {
            return base.Channel.GetQuestionnaire(questionnaireName);
        }
        
        public void StoreQuestionnaire(StatisServiceContracts.Questionnaire questionnaire) {
            base.Channel.StoreQuestionnaire(questionnaire);
        }
        
        public void DeleteQuestionnaire(string questionnaireName) {
            base.Channel.DeleteQuestionnaire(questionnaireName);
        }
        
        public string[] GetUserQuestionnaireList(string userName) {
            return base.Channel.GetUserQuestionnaireList(userName);
        }
        
        public string[] GetUserAnalysts(string userName) {
            return base.Channel.GetUserAnalysts(userName);
        }
        
        public string[] GetUserRespondents(string userName) {
            return base.Channel.GetUserRespondents(userName);
        }
        
        public bool AddAnalyst(string currentUserName, string analystUserName) {
            return base.Channel.AddAnalyst(currentUserName, analystUserName);
        }
        
        public void RemoveAnalyst(string currentUserName, string analystUserName) {
            base.Channel.RemoveAnalyst(currentUserName, analystUserName);
        }
        
        public bool AddRespondent(string currentUserName, string respondentEmail) {
            return base.Channel.AddRespondent(currentUserName, respondentEmail);
        }
        
        public void RemoveRespondent(string currentUserName, string respondentEmail) {
            base.Channel.RemoveRespondent(currentUserName, respondentEmail);
        }
        
        public void StoreFilledQuestionnaire(StatisServiceContracts.FilledQuestionnaire filled) {
            base.Channel.StoreFilledQuestionnaire(filled);
        }
        
        public StatisServiceContracts.FilledQuestionnaire GetFilledQuestionnaire(System.Guid id) {
            return base.Channel.GetFilledQuestionnaire(id);
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="StatisService.IQuestionnaireAdministrativeService")]
    public interface IQuestionnaireAdministrativeService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IQuestionnaireService/GetQuestionnaire", ReplyAction="http://tempuri.org/IQuestionnaireService/GetQuestionnaireResponse")]
        StatisServiceContracts.Questionnaire GetQuestionnaire(string questionnaireName);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IQuestionnaireService/StoreQuestionnaire")]
        void StoreQuestionnaire(StatisServiceContracts.Questionnaire questionnaire);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IQuestionnaireService/DeleteQuestionnaire")]
        void DeleteQuestionnaire(string questionnaireName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IQuestionnaireService/GetUserQuestionnaireList", ReplyAction="http://tempuri.org/IQuestionnaireService/GetUserQuestionnaireListResponse")]
        string[] GetUserQuestionnaireList(string userName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IQuestionnaireService/GetUserAnalysts", ReplyAction="http://tempuri.org/IQuestionnaireService/GetUserAnalystsResponse")]
        string[] GetUserAnalysts(string userName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IQuestionnaireService/GetUserRespondents", ReplyAction="http://tempuri.org/IQuestionnaireService/GetUserRespondentsResponse")]
        string[] GetUserRespondents(string userName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IQuestionnaireService/AddAnalyst", ReplyAction="http://tempuri.org/IQuestionnaireService/AddAnalystResponse")]
        bool AddAnalyst(string currentUserName, string analystUserName);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IQuestionnaireService/RemoveAnalyst")]
        void RemoveAnalyst(string currentUserName, string analystUserName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IQuestionnaireService/AddRespondent", ReplyAction="http://tempuri.org/IQuestionnaireService/AddRespondentResponse")]
        bool AddRespondent(string currentUserName, string respondentEmail);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IQuestionnaireService/RemoveRespondent")]
        void RemoveRespondent(string currentUserName, string respondentEmail);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IQuestionnaireService/StoreFilledQuestionnaire")]
        void StoreFilledQuestionnaire(StatisServiceContracts.FilledQuestionnaire filled);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IQuestionnaireService/GetFilledQuestionnaire", ReplyAction="http://tempuri.org/IQuestionnaireService/GetFilledQuestionnaireResponse")]
        StatisServiceContracts.FilledQuestionnaire GetFilledQuestionnaire(System.Guid id);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IQuestionnaireAdministrativeServiceChannel : StatisServiceTests.StatisService.IQuestionnaireAdministrativeService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class QuestionnaireAdministrativeServiceClient : System.ServiceModel.ClientBase<StatisServiceTests.StatisService.IQuestionnaireAdministrativeService>, StatisServiceTests.StatisService.IQuestionnaireAdministrativeService {
        
        public QuestionnaireAdministrativeServiceClient() {
        }
        
        public QuestionnaireAdministrativeServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public QuestionnaireAdministrativeServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public QuestionnaireAdministrativeServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public QuestionnaireAdministrativeServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public StatisServiceContracts.Questionnaire GetQuestionnaire(string questionnaireName) {
            return base.Channel.GetQuestionnaire(questionnaireName);
        }
        
        public void StoreQuestionnaire(StatisServiceContracts.Questionnaire questionnaire) {
            base.Channel.StoreQuestionnaire(questionnaire);
        }
        
        public void DeleteQuestionnaire(string questionnaireName) {
            base.Channel.DeleteQuestionnaire(questionnaireName);
        }
        
        public string[] GetUserQuestionnaireList(string userName) {
            return base.Channel.GetUserQuestionnaireList(userName);
        }
        
        public string[] GetUserAnalysts(string userName) {
            return base.Channel.GetUserAnalysts(userName);
        }
        
        public string[] GetUserRespondents(string userName) {
            return base.Channel.GetUserRespondents(userName);
        }
        
        public bool AddAnalyst(string currentUserName, string analystUserName) {
            return base.Channel.AddAnalyst(currentUserName, analystUserName);
        }
        
        public void RemoveAnalyst(string currentUserName, string analystUserName) {
            base.Channel.RemoveAnalyst(currentUserName, analystUserName);
        }
        
        public bool AddRespondent(string currentUserName, string respondentEmail) {
            return base.Channel.AddRespondent(currentUserName, respondentEmail);
        }
        
        public void RemoveRespondent(string currentUserName, string respondentEmail) {
            base.Channel.RemoveRespondent(currentUserName, respondentEmail);
        }
        
        public void StoreFilledQuestionnaire(StatisServiceContracts.FilledQuestionnaire filled) {
            base.Channel.StoreFilledQuestionnaire(filled);
        }
        
        public StatisServiceContracts.FilledQuestionnaire GetFilledQuestionnaire(System.Guid id) {
            return base.Channel.GetFilledQuestionnaire(id);
        }
    }
}
