using System.Globalization;
using Windows.UI.Xaml.Controls;
using Peppa.Contracts.Requests;
using Peppa.Contracts.Responses;
using Peppa.Interface;

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
            var currencies = await _userService.GetAvailableCurrencies();

            if (currencies != null)
            {
                Currencies.ItemsSource = currencies;
                Currencies.SelectedIndex = 0;
            }
            else
            {
                //TODO: Add a log that server send we back error
                var currentCurrency = new AvailableCurrency
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
                PasswrodNote.Text = "The passwords are not identical";
                PasswrodNote.Visibility = Windows.UI.Xaml.Visibility.Visible;
                return;
            }

            var selectedCurrency = Currencies.SelectedItem as AvailableCurrency;

            var request = new UserRequest
            {
                UserName = UserName.Text,
                Password = Password.Password,
                CurrencyBase = selectedCurrency?.Code ?? RegionInfo.CurrentRegion.ISOCurrencySymbol
            };

            var result = await _userService.RegistrationUser(request);
            
            //TODO Handler all cases
            switch (result.IdentityResult)
            {
                case Enums.IdentityResultEnum.DuplicateUserName:
                    UserNameNote.Visibility = Windows.UI.Xaml.Visibility.Visible;
                    break;
                case Enums.IdentityResultEnum.TokenIsNullOrEmpty:
                    break;
                case Enums.IdentityResultEnum.TokenGenerateError:
                    break;
                case Enums.IdentityResultEnum.Successful:
                    Token = result.Token;
                    InputedUserName = UserName.Text;
                    Hide();
                    break;
                case Enums.IdentityResultEnum.InternalServerError:
                    break;
                case Enums.IdentityResultEnum.PasswordTooShort:
                    PasswrodNote.Text = result.Error.Description;
                    PasswrodNote.Visibility = Windows.UI.Xaml.Visibility.Visible;
                    break;
                default:
                    break;
            }
        }

        private void OnPasswordLostFocus(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            PasswrodNote.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            UserNameNote.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }

        private void OnUserNameLostFocuse(Windows.UI.Xaml.UIElement sender, Windows.UI.Xaml.Input.LosingFocusEventArgs args)
        {
            PasswrodNote.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            UserNameNote.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }

        public AccessTokenResponse Token { get; set; }

        public string InputedUserName { get; set; }
    }
}
