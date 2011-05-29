using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Statis.StatisServices;

namespace Statis
{
    public partial class MainPage : UserControl
    {
        private QuestionnaireAdministrativeServiceClient _service;
        
        public MainPage()
        {
            InitializeComponent();
        }

        // After the Frame navigates, ensure the HyperlinkButton representing the current page is selected
        private void ContentFrame_Navigated(object sender, NavigationEventArgs e)
        {
            foreach (UIElement child in LinksStackPanel.Children)
            {
                HyperlinkButton hb = child as HyperlinkButton;
                if (hb != null && hb.NavigateUri != null)
                {
                    if (hb.NavigateUri.ToString().Equals(e.Uri.ToString()))
                    {
                        VisualStateManager.GoToState(hb, "ActiveLink", true);
                    }
                    else
                    {
                        VisualStateManager.GoToState(hb, "InactiveLink", true);
                    }
                }
            }
            // handle login/logout panel visibility depending on user
            if (Application.Current.Resources.Contains("user"))
            {
                LoginStackPanel.Visibility = System.Windows.Visibility.Collapsed;
                LogoutStackPanel.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                LoginStackPanel.Visibility = System.Windows.Visibility.Visible;
                LogoutStackPanel.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        // If an error occurs during navigation, show an error window
        private void ContentFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            e.Handled = true;
            ChildWindow errorWin = new ErrorWindow(e.Uri);
            errorWin.Show();
        }
    }
}