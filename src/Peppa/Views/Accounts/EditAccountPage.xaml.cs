using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using piggy_bank_uwp.ViewModels.Accounts;

namespace piggy_bank_uwp.Views.Accounts
{
    public sealed partial class EditBalancePage : Page
    {
        private AccountViewModel _account;

        public EditBalancePage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _account = e.Parameter as AccountViewModel;

        }

        private void OnSaveClick(object sender, RoutedEventArgs e)
        {
            //if (!String.IsNullOrEmpty(ChangeBalanceTextBox.Text))
            //{
            //    int value;
            //    bool canChange = Int32.TryParse(ChangeBalanceTextBox.Text, out value);

            //    if (canChange)
            //    {
            //        //_account.ChangeBalance(value);
            //    }
            //}

            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }

        private void OnDeleteClick(object sender, RoutedEventArgs e)
        {

        }

        private void OnCloseClick(object sender, RoutedEventArgs e)
        {
            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }
    }
}
