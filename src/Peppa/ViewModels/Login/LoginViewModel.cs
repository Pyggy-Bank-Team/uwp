using System.ComponentModel;
using Windows.UI.Xaml;
using Peppa.Dto;
using Peppa.Enums;
using Peppa.Interface.InternalServices;
using Peppa.Interface.Models;
using Peppa.Interface.ViewModels;
using Peppa.Interface.WindowsService;
using Peppa.Views;
using System.Collections.ObjectModel;
using System.Linq;

namespace Peppa.ViewModels.Login
{
    public class LoginViewModel : BaseViewModel, ILoginViewModel
    {
        private readonly ILoginModel _model;
        private readonly IToastService _toastService;
        private readonly ILocalizationService _localizationService;

        private Currency _selectedCurrency;

        public LoginViewModel(ILoginModel model, IToastService toastService, ILocalizationService localizationService)
        {
            IsLoginPanelShow = true;
            IsRegistrationPanelShow = false;
            IsLoginProgressShow = false;
            _model = model;
            _toastService = toastService;
            _localizationService = localizationService;
            Currencies = new ObservableCollection<Currency>();
        }

        public string UserName
        {
            get => _model.UserName;
            set
            {
                if (_model.UserName == value)
                    return;

                _model.UserName = value;
                RaisePropertyChanged(nameof(UserName));
            }
        }

        public string Password
        {
            get => _model.Password;
            set
            {
                if (_model.Password == value)
                    return;

                _model.Password = value;
                RaisePropertyChanged(nameof(Password));
            }
        }

        public string ConfirmPassword
        {
            get => _model.ConfirmPassword;
            set
            {
                if (_model.ConfirmPassword == value)
                    return;

                _model.ConfirmPassword = value;
                RaisePropertyChanged(nameof(ConfirmPassword));
            }
        }

        public string Email
        {
            get => _model.Email;
            set
            {
                if (_model.Email == value)
                    return;

                _model.Email = value;
                RaisePropertyChanged(nameof(Email));
            }
        }

        public string Error { get; private set; }

        public ObservableCollection<Currency> Currencies { get; }

        public Currency SelectedCurrency
        {
            get => _selectedCurrency;
            set
            {
                if (_selectedCurrency == value)
                    return;

                _selectedCurrency = value;
                _model.Currency = value;
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

            var result = SigninResultEnum.UnknownError;
            try
            {
               result = await _model.Signin(GetCancellationToken());
            }
            catch
            {
                _toastService.ShowNotification("SoS", _localizationService.GetTranslateByKey(Localization.OopsError));
            }

            IsLoginProgressShow = false;
            RaisePropertyChanged(nameof(IsLoginProgressShow));

            switch (result)
            {
                case SigninResultEnum.UserNotFound:
                    Error = _localizationService.GetTranslateByKey(Localization.UserNotFound);
                    RaisePropertyChanged(nameof(Error));
                    break;
                case SigninResultEnum.InvalidPassword:
                    Error = _localizationService.GetTranslateByKey(Localization.PasswordDoesntFit);
                    RaisePropertyChanged(nameof(Error));
                    break;
                case SigninResultEnum.UnknownError:
                    Error = _localizationService.GetTranslateByKey(Localization.OopsError);
                    RaisePropertyChanged(nameof(Error));
                    break;
            }

            if (result == SigninResultEnum.Ok)
                Frame.Navigate(typeof(MainPage));
        }
        
        public async void OnRegistrationButtonClick(object sender, RoutedEventArgs e)
        {
            IsLoginProgressShow = true;
            RaisePropertyChanged(nameof(IsLoginProgressShow));

            var result = SignupResultEnum.UnknownError;
            try
            {
                result = await _model.Signup(GetCancellationToken());
            }
            catch
            {
                _toastService.ShowNotification("SoS", _localizationService.GetTranslateByKey(Localization.OopsError));
            }
            
            IsLoginProgressShow = false;
            RaisePropertyChanged(nameof(IsLoginProgressShow));

            switch (result)
            {
                case SignupResultEnum.PasswordAndConfirmPasswordNotEquals:
                    Error = _localizationService.GetTranslateByKey(Localization.PasswordAndConfirmPasswordNotEquals);
                    RaisePropertyChanged(nameof(Error));
                    break;
                case SignupResultEnum.CurrencyNotSelected:
                    Error =_localizationService.GetTranslateByKey(Localization.CurrencyNotSelected);
                    RaisePropertyChanged(nameof(Error));
                    break;
                case SignupResultEnum.UserNotCreated:
                    Error = _localizationService.GetTranslateByKey(Localization.UserNotCreated);
                    RaisePropertyChanged(nameof(Error));
                    break;
                case SignupResultEnum.PasswordInvalid:
                    Error = _localizationService.GetTranslateByKey(Localization.PasswordInvalid);
                    RaisePropertyChanged(nameof(Error));
                    break;
                case SignupResultEnum.DuplicateUserName:
                    Error = _localizationService.GetTranslateByKey(Localization.UserAlreadyExists);
                    RaisePropertyChanged(nameof(Error));
                    break;
                case SignupResultEnum.InvalidUserName:
                    Error = _localizationService.GetTranslateByKey(Localization.InvalidUserName);
                    RaisePropertyChanged(nameof(Error));
                    break;
                case SignupResultEnum.UnknownError:
                    Error = _localizationService.GetTranslateByKey(Localization.OopsError);
                    RaisePropertyChanged(nameof(Error));
                    break;
                case SignupResultEnum.EmailEmptyOrNull:
                    Error = _localizationService.GetTranslateByKey(Localization.EmailEmptyOrNull);
                    RaisePropertyChanged(nameof(Error));
                    break;
            }

            if (result == SignupResultEnum.Ok)
                Frame.Navigate(typeof(MainPage));
        }

        public async void OnRegistrationLinkButtonClick(object sender, RoutedEventArgs e)
        {
            IsLoginPanelShow = false;
            IsRegistrationPanelShow = true;
            Error = null;
            RaisePropertyChanged(nameof(IsLoginPanelShow));
            RaisePropertyChanged(nameof(IsRegistrationPanelShow));
            RaisePropertyChanged(nameof(Error));

            if (Currencies.Count > 0)
                return;
            
            try
            {
                await _model.UpdateCurrencies(GetCancellationToken());
            }
            catch
            {
                _toastService.ShowNotification("SoS", _localizationService.GetTranslateByKey(Localization.OopsError));
            }

            Currencies.Clear();

            foreach (var item in _model.Currencies)
                Currencies.Add(item);

            _selectedCurrency = Currencies.First();
            RaisePropertyChanged(nameof(SelectedCurrency));
        }

        public void OnLoginLinkClick(object sender, RoutedEventArgs e)
        {
            IsLoginPanelShow = true;
            IsRegistrationPanelShow = false;
            Error = null;
            RaisePropertyChanged(nameof(IsLoginPanelShow));
            RaisePropertyChanged(nameof(IsRegistrationPanelShow));
            RaisePropertyChanged(nameof(Error));
        }

        private void OnModelPropertyChanged(object sender, PropertyChangedEventArgs e)
            => RaisePropertyChanged(e.PropertyName);
    }
}