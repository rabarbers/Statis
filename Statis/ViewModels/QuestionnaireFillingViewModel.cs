using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Windows;
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
    public class QuestionnaireFillingViewModel: ViewModelBase
    {
        private readonly QuestionnaireAdministrativeServiceClient _service;
        private FilledQuestionnaire _filledModel;
        private Questionnaire _model;
        private string _questionnaireToEdit;
        

        private readonly ObservableCollection<QuestionViewModel> _questions = new ObservableCollection<QuestionViewModel>();


        public DelegateCommand SaveFilledQuestionnaire { get; private set; }
        

        public QuestionnaireFillingViewModel()
        {
            _service = new QuestionnaireAdministrativeServiceClient();
            _service.GetQuestionnaireCompleted += ProxyGetQuestionnaireCompleted;
            _service.OpenAsync();

            SaveFilledQuestionnaire = new DelegateCommand(() =>
            {
                var user = Application.Current.Resources["user"] as string;
                if (user != null)
                {
                    //_service.StoreQuestionnaireAsync(user, _model);
                }
            });
        }


        public string Name
        {
            get { return _model != null ? _model.Name : string.Empty; }
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
                        _questions.Add(new TextQuestionViewModel((TextQuestion)question));
                    }
                    if (question is ImgChoiceQuestion)
                    {
                        _questions.Add(new ImgChoiceQuestionViewModel((ImgChoiceQuestion)question));
                    }
                }
            }
            OnNotifyPropertyChanged("Name");
            OnNotifyPropertyChanged("Questions");
        }

        public void ProxyGetQuestionnaireCompleted(object sender, GetQuestionnaireCompletedEventArgs1 e)
        {
            _model = e.Result;
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
