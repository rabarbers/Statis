﻿using System;
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
    public class ChoiceViewModel : ViewModelBase
    {
        private Choice _choiceModel;

        

        public ChoiceViewModel(Choice choiceModel)
        {
            _choiceModel = choiceModel;
        }


    }
}
