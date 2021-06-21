using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Peppa.Interface.InternalServices;
using Peppa.Interface.Models.Settings;
using Peppa.Interface.ViewModels;
using Peppa.Interface.WindowsService;

namespace Peppa.ViewModels.Settings
{
    public class SettingsViewModel : BaseViewModel, IInitialization
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

            Currencies = new List<string> {_model.Currency};
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

            IsProgressShow = false;
            RaisePropertyChanged(nameof(IsProgressShow));
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

        public List<string> Currencies { get; private set; }

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
    }
}