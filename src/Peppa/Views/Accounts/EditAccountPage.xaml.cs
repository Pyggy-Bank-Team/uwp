using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
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
            Types.ItemsSource = new[] { AccountType.Card, AccountType.Cash };
            Currencies.ItemsSource = new[] { _account.Currency };
        }

        private void OnSaveClick(object sender, RoutedEventArgs e)
        {
            _account.Action = ActionType.Save;
            GoBack();
        }

        private void OnCloseClick(object sender, RoutedEventArgs e)
        {
            _account.Action = ActionType.Cancel;
            GoBack();
        }

        private void GoBack()
        {
            if (Frame.CanGoBack)
                Frame.GoBack();
        }

        private void OnTitleSizeChanged(object sender, SizeChangedEventArgs e)
        {
            Types.Width = e.NewSize.Width;
            Currencies.Width = e.NewSize.Width;
        }

        private void OnDeleteClick(object sender, RoutedEventArgs e)
        {
            _account.Action = ActionType.Delete;
            GoBack();
        }
    }
}
