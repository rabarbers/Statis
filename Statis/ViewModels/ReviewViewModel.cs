using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Windows;
using System.Windows.Browser;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Practices.Prism.Commands;
using Statis.StatisServices;

namespace Statis.ViewModels
{
    public class ReviewViewModel: ViewModelBase
    {
        private readonly QuestionnaireAdministrativeServiceClient _service;
        private readonly ObservableCollection<string> _questionnaires = new ObservableCollection<string>();
        private string _selectedQuestionnaireName;
        
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

            

            DeleteQuestionnaire = new DelegateCommand(() =>
                                                          {
                                                              if (SelectedQuestionnaireName != null)
                                                              {
                                                                  _service.DeleteQuestionnaireAsync(
                                                                      SelectedQuestionnaireName);
                                                              }
                                                          });
            //AddImgSingleChoiceQuestion = new DelegateCommand(() =>
            //{
            //    var question = new ImgChoiceQuestion
            //                       {
            //                           QuestionId = Guid.NewGuid(),
            //                           Img = null,
            //                           ChoiceList = new ObservableCollection<Choice>()
            //                       };
            //    for (var i = 0; i < _imgQuestionNumberOfSingleChoices; i++)
            //    {
            //        question.ChoiceList.Add(new TextChoice());
            //    }
            //    _model.Questions.Add(question);
            //    Update();
            //});

            _service.OpenAsync();
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
