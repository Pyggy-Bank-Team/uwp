using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using piggy_bank_uwp.ViewModels.Users;
using piggy_bank_uwp.Dialogs;
using System;

namespace piggy_bank_uwp.Views.Users
{
    public sealed partial class SyncPage : Page
    {
        private readonly UserViewModel _dataContext;

        public SyncPage()
        {
            this.InitializeComponent();
            _dataContext = (UserViewModel) App.ServiceProvider.GetService(typeof(UserViewModel));
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
        }

        private async void OnLoginClick(object sender, RoutedEventArgs e)
        {
            UpdateProgressBar.Visibility = Visibility.Visible;

            await _dataContext.OnLogin(LoginText.Text, PasswordText.Password);

            UpdateProgressBar.Visibility = Visibility.Collapsed;
        }

        private async void OnOpenDialogForRegistration(object sender, RoutedEventArgs e)
        {
            var registrationDialog = new RegistrationDialog();
            var result = await registrationDialog.ShowAsync();
        }
    }
}