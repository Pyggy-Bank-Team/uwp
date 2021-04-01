using Peppa.ViewModels.Users;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Peppa.ViewModels.Login;

namespace Peppa.Views.Login
{
    public sealed partial class LoginPage : Page
    {
        private UserViewModel _dataContext;
        private LoginViewModel _loginViewModel;

        public LoginPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _dataContext = (UserViewModel)App.ServiceProvider.GetService(typeof(UserViewModel));
        }

        private async void OnLoginClick(object sender, RoutedEventArgs e)
        {
            //UpdateProgressBar.Visibility = Visibility.Visible;

            // _dataContext.SaveUserName(LoginText.Text);
            // await _dataContext.OnLogin(LoginText.Text, PasswordText.Password);

            //UpdateProgressBar.Visibility = Visibility.Collapsed;

            Frame.Navigate(typeof(MainPage));
        }

        private void OnRegistrationLinkClick(object sender, RoutedEventArgs e)
        {
            // LoginPanel.Visibility = Visibility.Collapsed;
            // RegistrationPanel.Visibility = Visibility.Visible;
            // UpdateCurrincies();
        }

        private void OnLoginLinkClick(object sender, RoutedEventArgs e)
        {
            // RegistrationPanel.Visibility = Visibility.Collapsed;
            // LoginPanel.Visibility = Visibility.Visible;
        }

        private async void UpdateCurrincies()
        {
            // var currencies = await _dataContext.GetCurrencies();
            //
            // if (currencies != null)
            // {
            //     Currencies.ItemsSource = currencies;
            //     Currencies.SelectedIndex = 0;
            // }
            // else
            // {
            //     //TODO: Add a log that server send we back error
            //     var currentCurrency = new CurrencyResponse
            //     {
            //         Symbol = NumberFormatInfo.CurrentInfo.CurrencySymbol,
            //         Code = RegionInfo.CurrentRegion.ISOCurrencySymbol
            //     };
            //
            //     Currencies.ItemsSource = new[] { currentCurrency };
            //     Currencies.SelectedIndex = 0;
            // }
        }

        private void OnUserNameLostFocus(UIElement sender, Windows.UI.Xaml.Input.LosingFocusEventArgs args)
        {
           // ErrorText.Visibility = Visibility.Collapsed;
        }

        private async void OnRegistrationClick(object sender, RoutedEventArgs e)
        {
            // if (Password.Password != ConfirmPassword.Password)
            // {
            //     ErrorText.Text = "The passwords are not identical";
            //     ErrorText.Visibility = Visibility.Visible;
            //     return;
            // }
            //
            // var selectedCurrency = Currencies.SelectedItem as CurrencyResponse;
            //
            // var request = new CreateUserRequest
            // {
            //     UserName = UserName.Text,
            //     Password = Password.Password,
            //     CurrencyBase = selectedCurrency?.Code ?? RegionInfo.CurrentRegion.ISOCurrencySymbol
            // };
            //
            // var result = await _dataContext.OnRegistration(UserName.Text, Password.Password, selectedCurrency?.Code ?? RegionInfo.CurrentRegion.ISOCurrencySymbol);
            //
            // //TODO Handler all cases
            // switch (result.IdentityResult)
            // {
            //     case Enums.IdentityResultEnum.Successful:
            //         _dataContext.SaveAccessToken(result.Token);
            //         Frame.Navigate(typeof(MainPage));
            //         break;
            //     default:
            //         ErrorText.Text = string.Join(Environment.NewLine, result.Error.Errors);
            //         ErrorText.Visibility = Visibility.Visible;
            //         break;
            // }
        }

        private void OnPasswordLostFocus(object sender, RoutedEventArgs e)
        {
            //ErrorText.Visibility = Visibility.Collapsed;
        }
    }
}
