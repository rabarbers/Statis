using System.Collections.ObjectModel;
using System.Windows;
using Microsoft.Practices.Prism.Commands;
using Statis.StatisServices;

namespace Statis.ViewModels
{
    public class ReviewViewModel: ViewModelBase
    {
        private readonly QuestionnaireAdministrativeServiceClient _service;
        private readonly ObservableCollection<string> _questionnaires = new ObservableCollection<string>();
        private string _selectedQuestionnaireName;
        private string _messageToSend;
        
        public DelegateCommand DeleteQuestionnaire { get; private set; }
        public DelegateCommand SendQuestionnaireToRespondents { get; private set; }

        public ReviewViewModel()
        {
            _service = new QuestionnaireAdministrativeServiceClient();
            _service.OpenCompleted += delegate
                                          {
                                              var user = Application.Current.Resources["user"] as string;
                                              if(user != null)
                                              {
                                                  _service.GetUserQuestionnaireListAsync(user);
                                              }
                                          };
            _service.GetUserQuestionnaireListCompleted += ProxyGetUserQuestionnaireListCompleted;
            _service.DeleteQuestionnaireCompleted += ProxyDeleteQuestionnaireCompleted;
            _service.SendQuestionnaireToRespondentsCompleted += ProxySendQuestionnaireToRespondentsCompleted;
            
            DeleteQuestionnaire = new DelegateCommand(() =>
                                                          {
                                                              var user = Application.Current.Resources["user"] as string;
                                                              if (user != null && SelectedQuestionnaireName != null)
                                                              {
                                                                  _service.DeleteQuestionnaireAsync(user, SelectedQuestionnaireName);
                                                              }
                                                          });

            SendQuestionnaireToRespondents = new DelegateCommand(() =>
            {
                var user = Application.Current.Resources["user"] as string;
                if (user != null && SelectedQuestionnaireName != null)
                {
                    var message = MessageToSend ?? "";
                    var questionnarieAddress = @"localhost//StatisTestPage.html#/CreateQuestionnaireView/" + SelectedQuestionnaireName;
                    
                    _service.SendQuestionnaireToRespondentsAsync(user, message.Replace("<questionnaire>", questionnarieAddress));
                }
            });

            _service.OpenAsync();
        }

        private static void ProxySendQuestionnaireToRespondentsCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            MessageBox.Show("Message send...");
        }
        
        private void ProxyGetUserQuestionnaireListCompleted(object sender, GetUserQuestionnaireListCompletedEventArgs1 e)
        {
            _questionnaires.Clear();
            foreach (var questionnaire in e.Result)
            {
                _questionnaires.Add(questionnaire);
            }
            OnNotifyPropertyChanged("QuestionnaireViewUri");
        }
        private void ProxyDeleteQuestionnaireCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            _questionnaires.Remove(SelectedQuestionnaireName);
        }

        
        
        
        public string SelectedQuestionnaireName
        {
            get { return _selectedQuestionnaireName; }
            set
            {
                if (_selectedQuestionnaireName != value)
                {
                    _selectedQuestionnaireName = value;
                    OnNotifyPropertyChanged("SelectedQuestionnaireName");
                    OnNotifyPropertyChanged("QuestionnaireViewUri");
                }
            }
        }

        public string MessageToSend
        {
            get { return _messageToSend; }
            set
            {
                if (_messageToSend != value)
                {
                    _messageToSend = value;
                    OnNotifyPropertyChanged("MessageToSend");
                }
            }
        }

        public ObservableCollection<string> MyQuestionnaires
        {
            get { return _questionnaires; }
        }

        public string QuestionnaireViewUri
        {
            get { return SelectedQuestionnaireName != null ? @"/CreateQuestionnaireView/" + SelectedQuestionnaireName : @"/CreateQuestionnaireView"; }
        }
    }
}
