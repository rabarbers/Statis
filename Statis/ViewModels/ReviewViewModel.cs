using System;
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
        private readonly ObservableCollection<FilledQuestionnaireRecord> _filledQuestionnaires = new ObservableCollection<FilledQuestionnaireRecord>();
        private string _selectedQuestionnaireName;
        private FilledQuestionnaireRecord _selectedFilledQuestionnaire;
        private string _messageToSend;
        
        public DelegateCommand DeleteQuestionnaire { get; private set; }
        public DelegateCommand SendQuestionnaireToRespondents { get; private set; }
        public DelegateCommand ViewQuestionnaire { get; private set; }
        public DelegateCommand ViewFilledQuestionnaire { get; private set; }

        public ReviewViewModel()
        {
            MessageToSend = "Lūdzu aizpildiet anketu <a href=\"<QAddress>\"><QName></a>!";
            
            _service = new QuestionnaireAdministrativeServiceClient();
            _service.OpenCompleted += delegate
                                          {
                                              var user = Application.Current.Resources["user"] as string;
                                              if(user != null)
                                              {
                                                  _service.GetUserQuestionnaireListAsync(user);
                                              }
                                          };
            _service.GetFilledQuestionnaireListCompleted += ProxyGetFilledQuestionnaireListCompleted;
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

                    var questionnarieAddress = @"http://localhost:4312/StatisTestPage.html#/QuestionnaireFillingView/" + SelectedQuestionnaireName;

                    _service.SendQuestionnaireToRespondentsAsync(user,
                        message.Replace("<QAddress>", questionnarieAddress).Replace("<QName>", SelectedQuestionnaireName),
                        SelectedQuestionnaireName);
                }
            });
            
            ViewQuestionnaire = new DelegateCommand(
                () =>
                    {
                        var baseAddress = Application.Current.Host.Source.AbsoluteUri.Replace(Application.Current.Host.Source.LocalPath, "");
                        var address = baseAddress + "/StatisTestPage.html#/QuestionnaireFillingView/" + SelectedQuestionnaireName;
                        
                        System.Windows.Browser.HtmlPage.Window.Navigate(new Uri(address));
                    });

            ViewFilledQuestionnaire = new DelegateCommand(() =>
                                                              {
                                                                  //ToDo
                                                              });

            _service.OpenAsync();
        }

        private void ProxyGetFilledQuestionnaireListCompleted(object sender, GetFilledQuestionnaireListCompletedEventArgs1 e)
        {
            _filledQuestionnaires.Clear();
            foreach (var questionnaire in e.Result)
            {
                _filledQuestionnaires.Add(questionnaire);
            }
        }

        private static void ProxySendQuestionnaireToRespondentsCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            MessageBox.Show("Paziņojums nosūtīts!");
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
                    
                    var user = Application.Current.Resources["user"] as string;
                    if(value != null && user != null)
                    {
                        _service.GetFilledQuestionnaireListAsync(user, value);
                    }
                }
            }
        }

        public FilledQuestionnaireRecord SelectedFilledQuestionnaire
        {
            get { return _selectedFilledQuestionnaire; }
            set
            {
                if (_selectedFilledQuestionnaire != value)
                {
                    _selectedFilledQuestionnaire = value;
                    OnNotifyPropertyChanged("SelectedFilledQuestionnaire");
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

        public ObservableCollection<FilledQuestionnaireRecord> FilledQuestionnaires
        {
            get { return _filledQuestionnaires; }
        }

        public string QuestionnaireViewUri
        {
            get { return SelectedQuestionnaireName != null ? @"/CreateQuestionnaireView/" + SelectedQuestionnaireName : @"/CreateQuestionnaireView"; }
        }
    }
}
