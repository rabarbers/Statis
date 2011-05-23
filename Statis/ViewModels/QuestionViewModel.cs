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
using Statis.StatisServices;

namespace Statis.ViewModels
{
    public abstract class QuestionViewModel : ViewModelBase
    {
        private readonly Question _model;
        
        protected QuestionViewModel(Question model)
        {
            _model = model;
        }

        public string Question
        {
            get { return _model.Text; }
            set
            {
                if (_model.Text != value)
                {
                    _model.Text = value;
                    OnNotifyPropertyChanged("Question");
                }
            }
        }
    }
}
