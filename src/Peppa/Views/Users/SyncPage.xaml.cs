using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using piggy_bank_uwp.ViewModel;
using piggy_bank_uwp.ViewModels.Users;
using System.Threading.Tasks;

namespace piggy_bank_uwp.Views.Users
{
    public sealed partial class SyncPage : Page
    {
        private OneDriveViewModel _oneDrive;
        private readonly UserViewModel _dataContext;
        private bool _isLoaded;

        public SyncPage()
        {
            this.InitializeComponent();
            _dataContext = (UserViewModel) App.ServiceProvider.GetService(typeof(UserViewModel));
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
        }

        private void EditVisualMode()
        {
            if (_oneDrive.IsAuthenticated)
            {
                ShowLogoutButton();
            }
            else
            {
                ShowLoginButton();
            }

            EditEnableNotificationBlock();
        }

        private void ShowLoginButton()
        {
            //LoginButton.Visibility = Visibility.Visible;
            //LogoutButton.Visibility = Visibility.Collapsed;
        }

        private void ShowLogoutButton()
        {
            //LoginButton.Visibility = Visibility.Collapsed;
            //LogoutButton.Visibility = Visibility.Visible;
        }

        private void EditEnableNotificationBlock()
        {
            //NotificationSwitch.IsOn = _oneDrive.IsNotificationOn;
        }

        private void DisableNotifactionBlock()
        {
            //NotificationSwitch.IsOn = false;
        }

        private async void OnLogoutClick(object sender, RoutedEventArgs e)
        {
            AuthorizationRing.IsActive = true;

            await _oneDrive.Logout();

            if (!_oneDrive.IsAuthenticated)
            {
                _oneDrive.ClrearCacheBlod();
                _oneDrive.SaveNotificationSetting(isOn: false);
            }

            EditVisualMode();

            AuthorizationRing.IsActive = false;
        }

        private async Task OnLoginClick(object sender, RoutedEventArgs e)
        {
            _dataContext.OnLogin(LoginText.Text, PasswordText.Password);
        }

        private void OnToggled(object sender, RoutedEventArgs e)
        {
            if (!_isLoaded)
                return;

            //_oneDrive.SaveNotificationSetting(NotificationSwitch.IsOn);

            //if (NotificationSwitch.IsOn)
            //{
            //    _oneDrive.SaveLastTimeShow();
            //}
            //else
            //{
            //    _oneDrive.ClearLastTimeShow();
            //}
        }

        private async void OnUploadClick(object sender, RoutedEventArgs e)
        {
            //UpdateProgressBar.Visibility = Visibility.Visible;
            //bool haveDate = await _oneDrive.UpdateData();

            //if (!haveDate)
            //{
            //    await _oneDrive.CreateData();
            //    await _oneDrive.UpdateData();
            //}

            //UpdateProgressBar.Visibility = Visibility.Collapsed;
        }

        private async void OnDownloadClick(object sender, RoutedEventArgs e)
        {
            //UpdateProgressBar.Visibility = Visibility.Visible;
            //bool haveData = await _oneDrive.DonwloadData();

            //if (haveData)
            //{
            //    MainViewModel.Current.UpdateData();
            //}
            //else
            //{
            //    await _oneDrive.CreateData();
            //}

            //UpdateProgressBar.Visibility = Visibility.Collapsed;
        }
    }
}