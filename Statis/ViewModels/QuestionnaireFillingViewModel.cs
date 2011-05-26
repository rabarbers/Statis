using System;
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
        //private Questionnaire _model;
        private string _questionnaireToEdit;
        
        
        public DelegateCommand SaveQuestionnaire { get; private set; }
        

        public QuestionnaireFillingViewModel()
        {
            _service = new QuestionnaireAdministrativeServiceClient();
            _service.OpenAsync();
        }
    }
}
