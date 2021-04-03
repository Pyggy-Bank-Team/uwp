using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Microsoft.Extensions.DependencyInjection;
using Peppa.Interface.ViewModels;

namespace Peppa.Views.Login
{
    public sealed partial class LoginPage : Page
    {
        private ILoginViewModel _loginViewModel;

        public LoginPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _loginViewModel = App.ServiceProvider.GetService<ILoginViewModel>();
        }
    }
}
