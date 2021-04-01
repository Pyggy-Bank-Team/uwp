using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Peppa.ViewModels.Login;

namespace Peppa.Views.Login
{
    public sealed partial class LoginPage : Page
    {
        private LoginViewModel _loginViewModel;

        public LoginPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            
        }
    }
}
