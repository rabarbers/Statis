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
    public class ImgChoiceQuestionViewModel : QuestionViewModel
    {
        private readonly ImgChoiceQuestion _model;
        private readonly ObservableCollection<ChoiceViewModel> _choices = new ObservableCollection<ChoiceViewModel>();

        public ImgChoiceQuestionViewModel(ImgChoiceQuestion model)
            : base(model)
        {
            _model = model;

            foreach (var choice in _model.ChoiceList)
            {
                if(choice is TextChoice)
                {
                    _choices.Add(new TextChoiceViewModel((TextChoice)choice));
                }
                //if (choice is NumberChoice)
                //{
                //    _choices.Add(new NumberChoiceViewModel((NumberChoice)choice));
                //}
                //if (choice is ImgChoice)
                //{
                //    _choices.Add(new TextChoiceViewModel((ImgChoice)choice));
                //}
            }

        }

        public ObservableCollection<ChoiceViewModel> Choices
        {
            get { return _choices; }
        }
    }
}
