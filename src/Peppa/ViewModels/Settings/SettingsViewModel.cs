using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Peppa.Interface.InternalServices;
using Peppa.Interface.Models.Settings;
using Peppa.Interface.ViewModels;
using Peppa.Interface.WindowsService;
using Peppa.Views.Login;
using Windows.ApplicationModel;

namespace Peppa.ViewModels.Settings
{
    public class SettingsViewModel : BaseViewModel, ISettingsViewModel
    {
        private readonly ISettingsModel _model;
        private readonly ILocalizationService _localizationService;
        private readonly IToastService _toastService;

        private string _language;

        public SettingsViewModel(ISettingsModel model, ILocalizationService localizationService, IToastService toastService)
        {
            _model = model;
            _localizationService = localizationService;
            _toastService = toastService;

            Languages = _localizationService.Languages.Select(p => p.Value).ToList();
            _language = _localizationService.Languages[_model.Language];
            Version = string.Format("{0}.{1}.{2}",
                    Package.Current.Id.Version.Major,
                    Package.Current.Id.Version.Minor,
                    Package.Current.Id.Version.Build);
        }

        public async Task Initialization()
        {
            IsProgressShow = true;
            RaisePropertyChanged(nameof(IsProgressShow));
            
            try
            {
                await _model.UpdateUser(GetCancellationToken());
            }
            catch
            {
                _toastService.ShowNotification("SoS", _localizationService.GetTranslateByKey(Localization.OopsError));
            }

            Email = _model.Email;
            Currency = _model.Currency;
            UserName = _model.Login;
            
            RaisePropertyChanged(nameof(Email));
            RaisePropertyChanged(nameof(Currency));
            RaisePropertyChanged(nameof(UserName));
            RaisePropertyChanged(nameof(IsDarkModeEnabled));

            IsProgressShow = false;
            RaisePropertyChanged(nameof(IsProgressShow));
        }

        public void OnLogoutClick(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            _model.LogOut();
            Frame.CacheSize = 0;
            Frame.Navigate(typeof(LoginPage));
        }

        public string UserName { get; set; }

        public string Email { get; set; }
        
        public string Currency { get; set; }

        public List<string> Languages { get; }

        public string Language
        {
            get => _language;
            set
            {
                if (_language == value)
                    return;

                _language = value;
                _model.Language = _localizationService.Languages.FirstOrDefault(l => l.Value == value).Key;               
                RaisePropertyChanged(nameof(Language));

                _model.ChangeLanguage();

                IsChangedSettings = true;
                RaisePropertyChanged(nameof(IsChangedSettings));
            }
        }
        
        public bool IsProgressShow { get; set; }

        public bool IsDarkModeEnabled
        {
            get => _model.DarkModeIsEnabled;
            set
            {
                if (_model.DarkModeIsEnabled == value)
                    return;

                _model.DarkModeIsEnabled = value;

                IsChangedSettings = true;
                RaisePropertyChanged(nameof(IsChangedSettings));
            }
        }

        public string Version { get; set; }

        public bool IsChangedSettings { get; set; }
    }
}