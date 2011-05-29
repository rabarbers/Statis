using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using Microsoft.Practices.Prism.Commands;
using Statis.StatisServices;

namespace Statis.ViewModels
{
    public class QuestionnaireFillingViewModel: ViewModelBase
    {
        private readonly QuestionnaireAdministrativeServiceClient _service;
        private Questionnaire _model;
        private FilledQuestionnaire _filledModel;
        private readonly ObservableCollection<QuestionViewModel> _questions = new ObservableCollection<QuestionViewModel>();
        
        public DelegateCommand SaveFilledQuestionnaire { get; private set; }
        

        public QuestionnaireFillingViewModel()
        {
            _service = new QuestionnaireAdministrativeServiceClient();
            _service.GetQuestionnaireCompleted += ProxyGetQuestionnaireCompleted;
            _service.StoreFilledQuestionnaireCompleted += ProxyStoreFilledQuestionnaireCompleted;
            _service.OpenAsync();

            SaveFilledQuestionnaire = new DelegateCommand(() =>
            {
                var user = Application.Current.Resources["user"] as string;
                if (user != null)
                {
                    _service.StoreFilledQuestionnaireAsync(_filledModel);
                }
            });
        }

        private static void ProxyStoreFilledQuestionnaireCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            System.Windows.Browser.HtmlPage.Window.Navigate(new Uri("/StatisTestPage.html#/Home", UriKind.Relative));
        }


        public string Name
        {
            get { return _model != null ? _model.Name : string.Empty; }
        }

        public ObservableCollection<QuestionViewModel> Questions
        {
            get { return _questions; }
        }

        private void Update()
        {
            if (_model != null)
            {
                _questions.Clear();
                foreach (var question in _model.Questions)
                {
                    if (question is TextQuestion)
                    {
                        var answer = new TextAnswer();
                        _questions.Add(new TextAnswerViewModel((TextQuestion)question, answer));
                        _filledModel.Answers.Add(answer);
                    }
                    //if (question is ImgChoiceQuestion)
                    //{
                    //    _questions.Add(new ImgChoiceQuestionViewModel((ImgChoiceQuestion)question));
                    //}
                }
            }
            OnNotifyPropertyChanged("Questions");
        }

        public void ProxyGetQuestionnaireCompleted(object sender, GetQuestionnaireCompletedEventArgs1 e)
        {
            _model = e.Result;
            _filledModel = new FilledQuestionnaire
                               {
                                   Id = Guid.NewGuid(),
                                   QuestionnaireName = _model.Name,
                                   Answers = new ObservableCollection<Answer>()
                               };
            Update();
        }

        public void EditQuestionnaire(string questionnaireName)
        {
            var user = Application.Current.Resources["user"] as string;
            if (user != null)
            {
                _service.GetQuestionnaireAsync(user, questionnaireName);
            }
        }
    }
}
