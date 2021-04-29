using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Peppa.Enums;
using Peppa.ViewModels.Accounts;

namespace Peppa.Views.Accounts
{
    public sealed partial class EditBalancePage : Page
    {
        private AccountDialogViewModel _account;

        public EditBalancePage()
            => InitializeComponent();

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _account = e.Parameter as AccountDialogViewModel;
            Types.ItemsSource = new[] { AccountType.Card, AccountType.Cash };
            Currencies.ItemsSource = new[] { _account.Currency };
        }

        private void OnSaveClick(object sender, RoutedEventArgs e)
        {
            _account.Action = DialogResult.Save;
            GoBack();
        }

        private void OnCloseClick(object sender, RoutedEventArgs e)
        {
            _account.Action = DialogResult.Cancel;
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
            _account.Action = DialogResult.Delete;
            GoBack();
        }
    }
}
