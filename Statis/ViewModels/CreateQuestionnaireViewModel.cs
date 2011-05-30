using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using Microsoft.Practices.Prism.Commands;
using Statis.StatisServices;

namespace Statis.ViewModels
{
    public class CreateQuestionnaireViewModel : ViewModelBase
    {
        private readonly QuestionnaireAdministrativeServiceClient _service;
        private Questionnaire _model;
        private int _imgQuestionNumberOfSingleChoices = 3;
        private int _imgQuestionNumberOfManyChoices = 3;
        private readonly int[] _possibleChoiceNumbers = new [] {2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15};
        public DelegateCommand SaveQuestionnaire { get; private set; }
        public DelegateCommand AddTextQuestion { get; private set; }
        public DelegateCommand AddImgSingleChoiceQuestion { get; private set; }

        private readonly ObservableCollection<QuestionViewModel> _questions = new ObservableCollection<QuestionViewModel>();
        
        public CreateQuestionnaireViewModel()
        {
            _model = new Questionnaire {Questions = new ObservableCollection<Question>()};

            _service = new QuestionnaireAdministrativeServiceClient();
            _service.GetUserQuestionnaireCompleted += ProxyGetQuestionnaireCompleted;
            _service.OpenAsync();

            SaveQuestionnaire = new DelegateCommand(() =>
                                                        {
                                                            var user = Application.Current.Resources["user"] as string;
                                                            if (user != null)
                                                            {
                                                                _service.StoreQuestionnaireAsync(user, _model);
                                                            }
                                                        });
            AddTextQuestion = new DelegateCommand(() =>
                                                      {
                                                          _model.Questions.Add(new TextQuestion {QuestionId = Guid.NewGuid()});
                                                          Update();
                                                      });
            AddImgSingleChoiceQuestion = new DelegateCommand(() =>
            {
                var question = new ImgChoiceQuestion
                                   {
                                       QuestionId = Guid.NewGuid(),
                                       Img = null,
                                       ChoiceList = new ObservableCollection<Choice>()
                                   };
                for (var i = 0; i < _imgQuestionNumberOfSingleChoices; i++)
                {
                    question.ChoiceList.Add(new TextChoice {Index = i + 1});
                }
                _model.Questions.Add(question);
                Update();
            });
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

        public void ProxyGetQuestionnaireCompleted(object sender, GetUserQuestionnaireCompletedEventArgs1 e)
        {
            _model = e.Result;
            Update();
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

        public void EditQuestionnaire(string questionnaireName)
        {
            var user = Application.Current.Resources["user"] as string;
            if (user != null)
            {
                _service.GetUserQuestionnaireAsync(user, questionnaireName);
            }
        }

        public int ImgQuestionNumberOfSingleChoices
        {
            get { return _imgQuestionNumberOfSingleChoices; }
            set
            {
                if (_imgQuestionNumberOfSingleChoices != value)
                {
                    _imgQuestionNumberOfSingleChoices = value;
                    OnNotifyPropertyChanged("ImgQuestionNumberOfSingleChoices");
                }
            }
        }

        public int ImgQuestionNumberOfManyChoices
        {
            get { return _imgQuestionNumberOfManyChoices; }
            set
            {
                if (_imgQuestionNumberOfManyChoices != value)
                {
                    _imgQuestionNumberOfManyChoices = value;
                    OnNotifyPropertyChanged("ImgQuestionNumberOfManyChoices");
                }
            }
        }

        public IEnumerable<int> PossibleChoiceNumbers
        {
            get { return _possibleChoiceNumbers; }
        }


        public ObservableCollection<QuestionViewModel> Questions
        {
            get { return _questions; }
        }

        #endregion

    }
}
