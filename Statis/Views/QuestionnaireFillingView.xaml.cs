using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Statis.ViewModels;

namespace Statis.Views
{
    public partial class QuestionnaireFillingView : Page
    {
        public QuestionnaireFillingView()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var s = e.Uri.OriginalString;
            s = s.Remove(0, "/QuestionnaireFillingView".Length);
            if (s.Length > 0)
            {
                s = s.Remove(0, 1);
                ((QuestionnaireFillingViewModel)LayoutRoot.DataContext).EditQuestionnaire(s);
            }
        }
    }
}
