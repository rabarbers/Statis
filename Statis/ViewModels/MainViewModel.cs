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
using Microsoft.Practices.Prism.Commands;
using Statis.StatisServices;

namespace Statis.ViewModels
{
    public class MainViewModel: ViewModelBase
    {
        private readonly QuestionnaireAdministrativeServiceClient _service;
        private string _userName;
        private string _password;
        private string _loggedUserName;

        public DelegateCommand LoginCommand { get; private set; }

        public MainViewModel()
        {
            LoginCommand = new DelegateCommand(() =>
            {
                if (UserName != null && Password != null)
                {
                    if(Application.Current.Resources.Contains("user"))
                    {
                        Application.Current.Resources.Remove("user");
                    }
                    Application.Current.Resources.Add("user", UserName);

                    //_service.GetUserRespondentsAsync(UserName);
                }
            });

            //_service = new QuestionnaireAdministrativeServiceClient();
            //_service.OpenCompleted += delegate
            //                              {
            //                                  if (Application.Current.Resources.Contains("user"))
            //                                  {
            //                                      var user = Application.Current.Resources["user"] as string;
            //                                      _service.GetUserRespondentsAsync(user);
            //                                  }
            //                              };
            //_service.GetUserRespondentsCompleted += ProxyGetUserRespondentsCompleted;
            //_service.OpenAsync();
            
        }

        

        public string UserName
        {
            get { return _userName; }
            set
            {
                if (_userName != value)
                {
                    _userName = value;
                    OnNotifyPropertyChanged("UserName");
                }
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                if (_password != value)
                {
                    _password = value;
                    OnNotifyPropertyChanged("Password");
                }
            }
        }
    }
}
