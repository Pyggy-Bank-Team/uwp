using Windows.UI.Xaml;
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
            
            _dataContext = (AccountsViewModel) App.ServiceProvider.GetService(typeof(AccountsViewModel));
            await _dataContext.Initialization();
        }

        private void OnAddedBalanceClick(object sender, RoutedEventArgs e)
        {
            var newAccount = new AccountViewModel();
            _dataContext.SelectedItem = newAccount;
            Frame.Navigate(typeof(EditBalancePage), newAccount);
        }
    }
}