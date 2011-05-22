using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Statis.StatisServices;

namespace Statis.ViewModels
{
    public class CreateQuestionnaireViewModel : ViewModelBase
    {
        public QuestionnaireAdministrativeServiceClient _service;
        private Questionnaire _model;

        private readonly ObservableCollection<QuestionViewModel> _questions = new ObservableCollection<QuestionViewModel>();
        
        public CreateQuestionnaireViewModel()
        {
            //_service = new QuestionnaireAdministrativeServiceClient();
            //_service.OpenCompleted += delegate
            //                              {
            //                                  _service.GetQuestionnaireAsync("Test");
            //                              };
            //_service.GetQuestionnaireCompleted += proxy_GetQuestionnaireCompleted;

            //_service.OpenAsync();
        }

        public void proxy_GetQuestionnaireCompleted(object sender, GetQuestionnaireCompletedEventArgs1 e)
        {
            _model = e.Result;

            _questions.Clear();
            if (_model != null)
            {
                foreach (var question in _model.Questions)
                {
                    if(question is TextQuestion)
                    {
                        _questions.Add(new TextQuestionViewModel((TextQuestion)question));
                    }
                    if (question is ChoiceQuestion)
                    {
                        //_questions.Add(new TextQuestionViewModel((ChoiceQuestion)question));
                    }
                    
                    
                }
            }

        }


        #region Properties

        public string Name
        {
            get { return _model != null ? _model.Name : string.Empty; }
            set
            {
                if (_model != null && _model.Name != value)
                {
                    _model.Name = value;
                    OnNotifyPropertyChanged("Name");
                }
            }
        }


        public ObservableCollection<QuestionViewModel> Questions
        {
            get { return _questions; }
        }

        //public ObservableCollection<> People
        //{
        //    get
        //    {
        //        return _People;
        //    }
        //    set
        //    {
        //        if (_People != value)
        //        {
        //            _People = value;
        //            OnNotifyPropertyChanged("People");
        //        }
        //    }

        //}

        #endregion

    }
}
