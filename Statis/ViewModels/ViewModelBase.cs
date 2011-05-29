using System.ComponentModel;
using System.Windows;

namespace Statis.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        protected void OnNotifyPropertyChanged(string p)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(p));
            }
        }

        public bool IsDesignTime
        {
            get
            {
                return (Application.Current == null) || (Application.Current.GetType() == typeof(Application));
            }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
