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
    public class UsersViewModel: ViewModelBase
    {
        private readonly QuestionnaireAdministrativeServiceClient _service;
        private readonly ObservableCollection<string> _users = new ObservableCollection<string>();
        private readonly ObservableCollection<string> _respondents = new ObservableCollection<string>();
        private string _newRespondent = "e-pasts";
        private string _newAnalyst = "lietotājvārds";


        public UsersViewModel()
        {
            _service = new QuestionnaireAdministrativeServiceClient();
            _service.OpenCompleted += delegate
                                          {
                                              if (Application.Current.Resources.Contains("user"))
                                              {
                                                  var user = Application.Current.Resources["user"] as string;
                                                  _service.GetUserRespondentsAsync(user);
                                              }
                                          };
            _service.GetUserRespondentsCompleted += ProxyGetUserRespondentsCompleted;
            _service.OpenAsync();
        }

        void ProxyGetUserRespondentsCompleted(object sender, GetUserRespondentsCompletedEventArgs1 e)
        {
            _respondents.Clear();
            foreach (var user in e.Result)
            {
                _respondents.Add(user);
            }
        }


        public ObservableCollection<string> Users
        {
            get { return _users; }
        }

        public ObservableCollection<string> Respondents
        {
            get { return _respondents; }
        }

        public string NewRespondent
        {
            get { return _newRespondent; }
            set
            {
                if (_newRespondent != value)
                {
                    _newRespondent = value;
                    OnNotifyPropertyChanged("NewRespondent");
                }
            }
        }

        public string NewAnalyst
        {
            get { return _newAnalyst; }
            set
            {
                if (_newAnalyst != value)
                {
                    _newAnalyst = value;
                    OnNotifyPropertyChanged("NewAnalyst");
                }
            }
        }
    }
}
