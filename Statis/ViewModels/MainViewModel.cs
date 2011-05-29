/// <summary>
/// handles the main user authentication
/// </summary>

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
        public string greeting { get; set; }
        //private string _loggedUserName;

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
                    _authenticatingUserName = UserName;
                    _service.AuthenticateUserAsync(UserName, Password);
                    greeting = "Sveiks, lietotāj!";
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
            }
            greeting = "Sveiks, " + _authenticatingUserName + "!";
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
