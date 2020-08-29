﻿using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using piggy_bank_uwp.ViewModels.Users;
using piggy_bank_uwp.Dialogs;
using System;
using piggy_bank_uwp.Interface;

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

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (!string.IsNullOrEmpty(_dataContext.Token) && !string.IsNullOrEmpty(_dataContext.UserName))
            {
                LoginGrid.Visibility = Visibility.Collapsed;

                Avatar.DisplayName = _dataContext.UserName;
                UserName.Text = _dataContext.UserName;
                UserGrid.Visibility = Visibility.Visible;
            }
        }

        private async void OnLoginClick(object sender, RoutedEventArgs e)
        {
            UpdateProgressBar.Visibility = Visibility.Visible;

            _dataContext.SaveUserName(LoginText.Text);
            await _dataContext.OnLogin(LoginText.Text, PasswordText.Password);

            UpdateProgressBar.Visibility = Visibility.Collapsed;

            if (!string.IsNullOrEmpty(_dataContext.Token) && !string.IsNullOrEmpty(_dataContext.UserName))
            {
                LoginGrid.Visibility = Visibility.Collapsed;

                Avatar.DisplayName = _dataContext.UserName;
                UserName.Text = _dataContext.UserName;
                UserGrid.Visibility = Visibility.Visible;
            }
        }

        private async void OnOpenDialogForRegistration(object sender, RoutedEventArgs e)
        {
            var registrationDialog = new RegistrationDialog((IUserService)App.ServiceProvider.GetService(typeof(IUserService)));
            await registrationDialog.ShowAsync();

            if (registrationDialog.Token != null)
            {
                _dataContext.SaveAccessToken(registrationDialog.Token);
                LoginGrid.Visibility = Visibility.Collapsed;

                Avatar.DisplayName = registrationDialog.InputedUserName;
                UserName.Text = registrationDialog.InputedUserName;
                UserGrid.Visibility = Visibility.Visible;
            }
        }

        private void OnAccountSettings(object sender, RoutedEventArgs e)
        {
            _dataContext.RemovedSaveData();
            LoginGrid.Visibility = Visibility.Visible;
            UserGrid.Visibility = Visibility.Collapsed;
        }
    }
}