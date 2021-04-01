using System;
using System.Collections.Generic;
using System.ComponentModel;
using Windows.UI.Xaml;
using Peppa.Dto;
using Peppa.Interface.InternalServices;
using Peppa.Interface.Models;
using Peppa.Interface.WindowsService;

namespace Peppa.ViewModels.Login
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly ILoginModel _model;
        private readonly IToastService _toastService;
        private readonly ILocalizationService _localizationService;

        public LoginViewModel(ILoginModel model, IToastService toastService, ILocalizationService localizationService)
        {
            IsLoginPanelShow = true;
            IsRegistrationPanelShow = false;
            IsLoginProgressShow = false;
            _model = model;
            _toastService = toastService;
            _localizationService = localizationService;
            _model.PropertyChanged += OnModelPropertyChanged;
        }

        public string UserName
        {
            get => _model.UserName;
            set
            {
                _model.UserName = value;
                RaisePropertyChanged(nameof(UserName));
            }
        }

        public string Password
        {
            get => _model.Password;
            set
            {
                _model.Password = value;
                RaisePropertyChanged(nameof(Password));
            }
        }

        public string ConfirmPassword
        {
            get => _model.ConfirmPassword;
            set
            {
                _model.ConfirmPassword = value;
                RaisePropertyChanged(nameof(ConfirmPassword));
            }
        }

        public string Email
        {
            get => _model.Email;
            set
            {
                _model.Email = value;
                RaisePropertyChanged(nameof(Email));
            }
        }

        public string Error => _model.Error;

        public List<Currency> Currencies => _model.Currencies;

        public Currency SelectedCurrency
        {
            get => _model.SelectedCurrency;
            set
            {
                _model.SelectedCurrency = value;
                RaisePropertyChanged(nameof(SelectedCurrency));
            }
        }
        
        public bool IsLoginProgressShow { get; set; }
        
        public bool IsLoginPanelShow { get; set; }
        
        public bool IsRegistrationPanelShow { get; set; }

        public async void OnLoginButtonClick(object sender, RoutedEventArgs e)
        {
            IsLoginProgressShow = true;
            RaisePropertyChanged(nameof(IsLoginProgressShow));

            try
            {
                await _model.Signin(GetCancellationToken());
            }
            catch
            {
                _toastService.ShowNotification("SoS", _localizationService.GetTranslateByKey(Localization.OopsError));
            }
        }
        
        public async void OnRegistrationButtonClick(object sender, RoutedEventArgs e)
        {
            
        }

        public void OnRegistrationLinkButtonClick(object sender, RoutedEventArgs e)
        {
            
        }

        public void OnLoginLinkClick(object sender, RoutedEventArgs e)
        {
            
        }

        private void OnModelPropertyChanged(object sender, PropertyChangedEventArgs e)
            => RaisePropertyChanged(e.PropertyName);
    }
}