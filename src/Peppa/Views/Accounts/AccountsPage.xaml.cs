using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using piggy_bank_uwp.ViewModels.Accounts;

namespace piggy_bank_uwp.Views.Accounts
{
    public sealed partial class BalancesPage : Page
    {
        private AccountsViewModel _dataContext;
        public BalancesPage()
        {
            this.InitializeComponent();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            UpdateProgressRing.Visibility = Visibility.Visible;
            _dataContext = (AccountsViewModel)App.ServiceProvider.GetService(typeof(AccountsViewModel));
            DataContext = _dataContext;

            await _dataContext.Initialization();

            UpdateProgressRing.Visibility = Visibility.Collapsed;
        }

        private void OnBalanceItemClick(object sender, ItemClickEventArgs e)
        {
            Frame.Navigate(typeof(EditBalancePage), e.ClickedItem);
        }

        private void OnAddedBalanceClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(EditBalancePage), new AccountViewModel());
        }
    }
}
