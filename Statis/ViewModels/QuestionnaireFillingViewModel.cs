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
            _service.StoreFilledQuestionnaireCompleted += delegate
                                                              {
                                                                  System.Windows.Browser.HtmlPage.Window.Navigate(new Uri("/StatisTestPage.html#/Home", UriKind.Relative));
                                                              };
            _service.OpenAsync();

            SaveFilledQuestionnaire = new DelegateCommand(() => _service.StoreFilledQuestionnaireAsync(_filledModel));
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
                    if (question is ImgChoiceQuestion)
                    {
                        var answer = new SingleChoiceAnswer();
                        _questions.Add(new ImgChoiceAnswerViewModel((ImgChoiceQuestion)question, answer));
                        _filledModel.Answers.Add(answer);
                    }
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
                                   QuestionnaireName = _model != null ? _model.Name : "",
                                   Answers = new ObservableCollection<Answer>()
                               };
            Update();
            OnNotifyPropertyChanged("Name");
        }

        public void EditQuestionnaire(string questionnaireName)
        {
            _service.GetQuestionnaireAsync(questionnaireName);
        }
    }
}
