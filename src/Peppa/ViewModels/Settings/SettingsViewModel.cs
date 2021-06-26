using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Peppa.Interface.InternalServices;
using Peppa.Interface.Models.Settings;
using Peppa.Interface.ViewModels;
using Peppa.Interface.WindowsService;
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
            Version = string.Format("{0}.{1}.{2}.{3}",
                    Package.Current.Id.Version.Major,
                    Package.Current.Id.Version.Minor,
                    Package.Current.Id.Version.Build,
                    Package.Current.Id.Version.Revision);
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
            IsDarkModeEnabled = _model.DarkModeIsEnabled;
            
            RaisePropertyChanged(nameof(Email));
            RaisePropertyChanged(nameof(Currency));
            RaisePropertyChanged(nameof(UserName));
            RaisePropertyChanged(nameof(IsDarkModeEnabled));

            IsProgressShow = false;
            RaisePropertyChanged(nameof(IsProgressShow));
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
            }
        }
        
        public bool IsProgressShow { get; set; }

        public bool IsDarkModeEnabled { get; set; }

        public string Version { get; set; }
    }
}