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
    public class TextChoiceViewModel : ChoiceViewModel
    {
        private TextChoice _model;

        public TextChoiceViewModel(TextChoice model)
            : base(model)
        {
            _model = model;
        }

        public string Text
        {
            get { return _model.Option; }
            set
            {
                if (_model.Option != value)
                {
                    _model.Option = value;
                    OnNotifyPropertyChanged("Text");
                }
            }
        }
    }
}
