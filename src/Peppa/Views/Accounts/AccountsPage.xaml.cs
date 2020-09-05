using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Peppa.ViewModels.Accounts;

namespace Peppa.Views.Accounts
{
    public sealed partial class BalancesPage
    {
        private AccountsViewModel _dataContext;

        public BalancesPage()
        {
            InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            UpdateProgressRing.Visibility = Visibility.Visible;
            _dataContext = (AccountsViewModel) App.ServiceProvider.GetService(typeof(AccountsViewModel));
            DataContext = _dataContext;

            var selectedItem = _dataContext.SelectedItem;
            if (selectedItem != null)
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