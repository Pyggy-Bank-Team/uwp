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

            if (_dataContext.SelectedItem != null)
                await _dataContext.UpdateData();

            await _dataContext.Initialization();
            _dataContext.SelectedItem = null;

            UpdateProgressRing.Visibility = Visibility.Collapsed;
        }       

        private void OnBalanceItemClick(object sender, ItemClickEventArgs e)
        {
            _dataContext.SelectedItem = e.ClickedItem as AccountViewModel;
            Frame.Navigate(typeof(EditBalancePage), e.ClickedItem);
        }

        private void OnAddedBalanceClick(object sender, RoutedEventArgs e)
        {
            var newAccount = new AccountViewModel();
            _dataContext.SelectedItem = newAccount;
            Frame.Navigate(typeof(EditBalancePage), newAccount);
        }
    }
}
