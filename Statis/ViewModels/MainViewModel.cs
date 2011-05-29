/// <summary>handles the main user authentication</summary>

using System.Windows;
using Microsoft.Practices.Prism.Commands;
using Statis.StatisServices;

namespace Statis.ViewModels
{
    public class MainViewModel: ViewModelBase
    {
        private readonly QuestionnaireAdministrativeServiceClient _service;
        private string _userName;
        private string _password;
        private string _authenticatingUserName;
        private string _greeting;
        private string _loginLogoutText;
        private Visibility _loggedInVisibility;
        //private string _loggedUserName;
        
        public DelegateCommand LoginCommand { get; private set; }

        public MainViewModel()
        {
            GreetingText = "";
            LoginLogoutText = "Ieiet";
            LoggedInVisibility = Visibility.Visible;
            
            LoginCommand = new DelegateCommand(() =>
            {
                if (UserName != null && Password != null)
                {
                    if(Application.Current.Resources.Contains("user"))
                    {
                        Application.Current.Resources.Remove("user");
                    }
                    _authenticatingUserName = UserName;
                    _service.AuthenticateUserAsync(UserName, Password);
                }
            });

            _service = new QuestionnaireAdministrativeServiceClient();
            _service.AuthenticateUserCompleted += ProxyAuthenticateUserCompleted;
            _service.OpenAsync();
            
        }

        private void ProxyAuthenticateUserCompleted(object sender, AuthenticateUserCompletedEventArgs1 e)
        {
            if(e.Result)
            {
                Application.Current.Resources.Add("user", _authenticatingUserName);
                GreetingText = "Sveiks, " + _authenticatingUserName + "!";

                UserName = "";
                Password = "";
                LoggedInVisibility = Visibility.Collapsed;
                LoginLogoutText = "Iziet";

                LoginCommand = new DelegateCommand(() =>
                {
                    if (Application.Current.Resources.Contains("user"))
                    {
                        Application.Current.Resources.Remove("user");
                    }
                    GreetingText = "";
                    LoginLogoutText = "Ieiet";
                    LoggedInVisibility = Visibility.Visible;

                    LoginCommand = new DelegateCommand(() =>
                    {
                        if (UserName != null && Password != null)
                        {
                            if (Application.Current.Resources.Contains("user"))
                            {
                                Application.Current.Resources.Remove("user");
                            }
                            _authenticatingUserName = UserName;
                            _service.AuthenticateUserAsync(UserName, Password);
                        }
                    });
                    OnNotifyPropertyChanged("LoginCommand");
                });
                OnNotifyPropertyChanged("LoginCommand");
            }          
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

        public string GreetingText
        {
            get { return _greeting; }
            set
            {
                if (_greeting != value)
                {
                    _greeting = value;
                    OnNotifyPropertyChanged("GreetingText");
                }
            }
        }
        
        public string LoginLogoutText
        {
            get { return _loginLogoutText; }
            set
            {
                if (_loginLogoutText != value)
                {
                    _loginLogoutText = value;
                    OnNotifyPropertyChanged("LoginLogoutText");
                }
            }
        }

        public Visibility LoggedInVisibility
        {
            get { return _loggedInVisibility; }
            set
            {
                if (_loggedInVisibility != value)
                {
                    _loggedInVisibility = value;
                    OnNotifyPropertyChanged("LoggedInVisibility");
                }
            }
        }

    }
}
