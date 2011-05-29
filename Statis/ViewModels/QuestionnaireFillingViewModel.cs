using System.Collections.ObjectModel;
using Microsoft.Practices.Prism.Commands;
using Statis.StatisServices;

namespace Statis.ViewModels
{
    public class QuestionnaireFillingViewModel: ViewModelBase
    {
        private readonly QuestionnaireAdministrativeServiceClient _service;
        //private FilledQuestionnaire _filledModel;
        private Questionnaire _model;
        private string _questionnaireToEdit;

        private readonly ObservableCollection<QuestionViewModel> _questions = new ObservableCollection<QuestionViewModel>();
        
        
        public DelegateCommand SaveQuestionnaire { get; private set; }
        

        public QuestionnaireFillingViewModel()
        {
            _service = new QuestionnaireAdministrativeServiceClient();
            _service.OpenAsync();
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
    }
}
