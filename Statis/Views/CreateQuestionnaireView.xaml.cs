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
using System.Windows.Shapes;
using System.Windows.Navigation;
using Statis.StatisServices;
using Statis.ViewModels;

namespace Statis.Views
{
    public partial class CreateQuestionnaireView : Page
    {
        public CreateQuestionnaireView()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            var context = (CreateQuestionnaireViewModel) ((Grid)Content).DataContext;

            context._service = new QuestionnaireAdministrativeServiceClient();
            context._service.OpenCompleted += delegate
                                          {
                                              context._service.GetQuestionnaireAsync("Test");
                                          };
            context._service.GetQuestionnaireCompleted += context.proxy_GetQuestionnaireCompleted;

            context._service.OpenAsync();
        }
    }
}
