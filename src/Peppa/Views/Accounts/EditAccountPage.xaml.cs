using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using System;
using Peppa.Enums;
using Peppa.ViewModels.Accounts;
using piggy_bank_uwp.Enums;

namespace Peppa.Views.Accounts
{
    public sealed partial class EditBalancePage : Page
    {
        private AccountViewModel _account;

        public EditBalancePage()
            => InitializeComponent();

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _account = e.Parameter as AccountViewModel;
            Types.ItemsSource = Enum.GetValues(typeof(AccountType));
            Currencies.ItemsSource = new[] { _account.Currency };
        }

        private void OnSaveClick(object sender, RoutedEventArgs e)
        {
            _account.ButtonWasClicked = ButtonType.Save;
            GoBack();
        }

        private void OnDeleteClick(object sender, RoutedEventArgs e)
        {
            _account.ButtonWasClicked = ButtonType.Delete;
            GoBack();
        }

        private void OnCloseClick(object sender, RoutedEventArgs e)
        {
            _account.ButtonWasClicked = ButtonType.Cancel;
            GoBack();
        }

        private void GoBack()
        {
            if (Frame.CanGoBack)
                Frame.GoBack();
        }
    }
}
