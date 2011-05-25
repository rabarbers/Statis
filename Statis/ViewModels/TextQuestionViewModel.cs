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
    public class TextQuestionViewModel : QuestionViewModel
    {
        private TextQuestion _model;

        public TextQuestionViewModel(TextQuestion model): base(model)
        {
            _model = model;
        }
    }
}
