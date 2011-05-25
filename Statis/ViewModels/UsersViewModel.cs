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
using Microsoft.Practices.Prism.Commands;
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
        private string _addingNewRespondent;
        private string _addingNewAnalyst;
        private string _selectedRespondent;
        private string _selectedAnalyst;
        public DelegateCommand AddAnalyst { get; private set; }
        public DelegateCommand AddRespondent { get; private set; }
        public DelegateCommand RemoveRespondent { get; private set; }
        public DelegateCommand RemoveAnalyst { get; private set; }


        public UsersViewModel()
        {
            AddAnalyst = new DelegateCommand(() =>
            {
                var user = Application.Current.Resources["user"] as string;
                if(user != null)
                {
                    _addingNewAnalyst = NewAnalyst;
                    _service.AddAnalystAsync(user, NewAnalyst);
                    NewAnalyst = "";
                }
            });

            RemoveAnalyst = new DelegateCommand(() =>
            {
                var user = Application.Current.Resources["user"] as string;
                if(SelectedAnalyst != null && user != null)
                {
                    _service.RemoveAnalystAsync(user, SelectedAnalyst);
                    _users.Remove(SelectedAnalyst);
                }
            });

            AddRespondent = new DelegateCommand(() =>
            {
                var user = Application.Current.Resources["user"] as string;
                if (user != null)
                {
                    _addingNewRespondent = NewRespondent;
                    _service.AddRespondentAsync(user, _addingNewRespondent);
                    NewRespondent = "";
                }
            });

            RemoveRespondent = new DelegateCommand(() =>
            {
                var user = Application.Current.Resources["user"] as string;
                if (SelectedRespondent != null && user != null)
                {
                    _service.RemoveRespondentAsync(user, SelectedRespondent);
                    _respondents.Remove(SelectedRespondent);
                }
            });
            
            _service = new QuestionnaireAdministrativeServiceClient();
            _service.OpenCompleted += delegate
                                          {
                                              if (Application.Current.Resources.Contains("user"))
                                              {
                                                  var user = Application.Current.Resources["user"] as string;
                                                  _service.GetUserRespondentsAsync(user);
                                                  _service.GetUserAnalystsAsync(user);
                                              }
                                          };
            _service.GetUserRespondentsCompleted += ProxyGetUserRespondentsCompleted;
            _service.GetUserAnalystsCompleted += ProxyGetUserAnalystsCompleted;
            _service.AddAnalystCompleted += ProxyAddAnalystCompleted;
            _service.AddRespondentCompleted += ProxyAddRespondentCompleted;
            _service.OpenAsync();
        }

        void ProxyAddRespondentCompleted(object sender, AddRespondentCompletedEventArgs1 e)
        {
            if (e.Result)
            {
                _respondents.Add(_addingNewRespondent);
            }
        }

        void ProxyAddAnalystCompleted(object sender, AddAnalystCompletedEventArgs1 e)
        {
            if(e.Result)
            {
                _users.Add(_addingNewAnalyst);
            }
        }

        void ProxyGetUserAnalystsCompleted(object sender, GetUserAnalystsCompletedEventArgs1 e)
        {
            _users.Clear();
            foreach (var user in e.Result)
            {
                _users.Add(user);
            }
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

        public string SelectedRespondent
        {
            get { return _selectedRespondent; }
            set
            {
                if (_selectedRespondent != value)
                {
                    _selectedRespondent = value;
                    OnNotifyPropertyChanged("SelectedRespondent");
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

        public string SelectedAnalyst
        {
            get { return _selectedAnalyst; }
            set
            {
                if (_selectedAnalyst != value)
                {
                    _selectedAnalyst = value;
                    OnNotifyPropertyChanged("SelectedAnalyst");
                }
            }
        }
    }
}
