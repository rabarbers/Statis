using System.Windows;
using Microsoft.Practices.Prism.Commands;
using Statis.StatisServices;

namespace Statis.ViewModels
{
    public class HomeViewModel: ViewModelBase
    {
        private readonly QuestionnaireAdministrativeServiceClient _service;
        private string _userName;
        private string _password1;
        private string _password2;
        private string _firstName;
        private string _lastName;
        private string _email;

        public DelegateCommand RegisterMe { get; private set; }

        public HomeViewModel()
        {
            _service = new QuestionnaireAdministrativeServiceClient();
            _service.RegisterUserCompleted += delegate(object sender, RegisterUserCompletedEventArgs1 e)
                                                  {
                                                      if (e.Result)
                                                      {
                                                          UserName = null;
                                                          Password1 = null;
                                                          Password2 = null;
                                                          FirstName = null;
                                                          LastName = null;
                                                          Email = null;
                                                          MessageBox.Show("Reģistrācija veiksmīga!");
                                                      }
                                                  };
            _service.OpenAsync();
            
            
            RegisterMe = new DelegateCommand(() =>
            {
                if (UserName != null && Password1 != null && Password1 == Password2)
                {
                    _service.RegisterUserAsync(UserName, Password1, FirstName, LastName, Email);
                }
            });
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

        public string Password1
        {
            get { return _password1; }
            set
            {
                if (_password1 != value)
                {
                    _password1 = value;
                    OnNotifyPropertyChanged("Password1");
                }
            }
        }

        public string Password2
        {
            get { return _password2; }
            set
            {
                if (_password2 != value)
                {
                    _password2 = value;
                    OnNotifyPropertyChanged("Password2");
                }
            }
        }

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                if (_firstName != value)
                {
                    _firstName = value;
                    OnNotifyPropertyChanged("FirstName");
                }
            }
        }

        public string LastName
        {
            get { return _lastName; }
            set
            {
                if (_lastName != value)
                {
                    _lastName = value;
                    OnNotifyPropertyChanged("LastName");
                }
            }
        }

        public string Email
        {
            get { return _email; }
            set
            {
                if (_email != value)
                {
                    _email = value;
                    OnNotifyPropertyChanged("Email");
                }
            }
        }
    }
}
