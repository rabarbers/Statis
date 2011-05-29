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
using Statis.StatisServices;

namespace Statis.ViewModels
{
    public class TextAnswerViewModel: TextQuestionViewModel
    {
        private readonly TextAnswer _answerModel;

        public TextAnswerViewModel(TextQuestion question, TextAnswer answer): base(question)
        {
            _answerModel = answer;
        }

        public string Answer
        {
            get { return _answerModel.AnswerText; }
            set
            {
                if (_answerModel.AnswerText != value)
                {
                    _answerModel.AnswerText = value;
                    OnNotifyPropertyChanged("Answer");
                }
            }
        }
    }
}
