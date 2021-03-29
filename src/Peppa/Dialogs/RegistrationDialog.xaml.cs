using System.Globalization;
using System.Linq;
using Windows.UI.Xaml.Controls;
using Peppa.Contracts.Requests;
using Peppa.Contracts.Responses;
using Peppa.Interface;
using Peppa.Interface.Services;
using System;
using System.Threading;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Peppa.Dialogs
{
    public sealed partial class RegistrationDialog : ContentDialog
    {
        private readonly IUserService _userService;
        public RegistrationDialog(IUserService userService)
        {
            this.InitializeComponent();
            _userService = userService;
            SetAvailableCurrincies();
        }

        private async void SetAvailableCurrincies()
        {
            var currencies = await _userService.GetAvailableCurrencies(CancellationToken.None);

            if (currencies != null)
            {
                Currencies.ItemsSource = currencies;
                Currencies.SelectedIndex = 0;
            }
            else
            {
                //TODO: Add a log that server send we back error
                var currentCurrency = new CurrencyResponse
                {
                    Symbol = NumberFormatInfo.CurrentInfo.CurrencySymbol,
                    Code = RegionInfo.CurrentRegion.ISOCurrencySymbol
                };

                Currencies.ItemsSource = new[] { currentCurrency };
                Currencies.SelectedIndex = 0;
            }
        }

        private async void OnRegistration(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            args.Cancel = true;

            if (Password.Password != ConfirmPassword.Password)
            {
                ErrorText.Text = "The passwords are not identical";
                ErrorText.Visibility = Windows.UI.Xaml.Visibility.Visible;
                return;
            }

            var selectedCurrency = Currencies.SelectedItem as CurrencyResponse;

            var request = new UserRequest
            {
                UserName = UserName.Text,
                Password = Password.Password,
                CurrencyBase = selectedCurrency?.Code ?? RegionInfo.CurrentRegion.ISOCurrencySymbol
            };

            var result = await _userService.RegistrationUser(request, CancellationToken.None);
            
            //TODO Handler all cases
            switch (result.IdentityResult)
            {
                case Enums.IdentityResultEnum.Successful:
                    Token = result.Token;
                    InputedUserName = UserName.Text;
                    Hide();
                    break;
                default:
                    ErrorText.Text = string.Join(Environment.NewLine, result.Error.Errors);
                    ErrorText.Visibility = Windows.UI.Xaml.Visibility.Visible;
                    break;
            }
        }

        private void OnPasswordLostFocus(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ErrorText.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }

        private void OnUserNameLostFocus(Windows.UI.Xaml.UIElement sender, Windows.UI.Xaml.Input.LosingFocusEventArgs args)
        {
            ErrorText.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }

        public AccessTokenResponse Token { get; set; }

        public string InputedUserName { get; set; }
    }
}
