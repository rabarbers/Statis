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

namespace Statis.ViewModels
{
    public class AnalystViewModel: ViewModelBase
    {
        private string _userName;
        private string _firstName;
        private string _lastName;
        private string _email;

        //public AnalystViewModel(Analyst model)
        //    : base(model)
        //{
        //    //_model = model;
        //}

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
