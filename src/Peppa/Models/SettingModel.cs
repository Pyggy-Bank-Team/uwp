using System.Threading;
using System.Threading.Tasks;
using Windows.Globalization;
using Windows.UI.Xaml;
using Peppa.Context.Entities;
using Peppa.Contracts.Requests;
using Peppa.Interface;
using Peppa.Interface.InternalServices;
using Peppa.Interface.Services;

namespace Peppa.Models
{
    public class SettingModel
    {
        private readonly IPiggyRepository _repository;
        private readonly IUserService _service;
        private readonly ISettingsService _settingsService;

        private bool _isDarkMode;

        public SettingModel(IPiggyRepository repository, IUserService service, ISettingsService settingsService)
        {
            _repository = repository;
            _service = service;
            _settingsService = settingsService;

            _isDarkMode = true;
            if (_settingsService.TryGetValue(Constants.RequestedTheme, out var theme))
                _isDarkMode = theme != ApplicationTheme.Light.ToString();

            Language = ApplicationLanguages.PrimaryLanguageOverride;
        }

        public async Task UpdateUser(CancellationToken token)
        {
            if (!_service.IsAuthorized)
                return;

            var userInfoResponse = await _service.GetUserInfo(token);

            await _repository.UpdateUser(new User
            {
                Email = userInfoResponse.Email,
                CurrencyBase = userInfoResponse.CurrencyBase,
                UserName = userInfoResponse.UserName
            }, token);
            
            var user = await _repository.GetUser(token);
            Login = user.UserName;
            Email = user.Email;
            Currency = user.CurrencyBase;
        }

        public async Task ChangeEmail(CancellationToken token)
        {
            if (!_service.IsAuthorized)
                return;

            var request = new UpdateUserInfoRequest
            {
                Email = Email
            };

            await _service.UpdateUserInfo(request, token);
        }

        public async Task ChangeCurrency(CancellationToken token)
        {
            if (!_service.IsAuthorized)
                return;

            var request = new UpdateUserInfoRequest
            {
                NewCurrency = Currency
            };

            await _service.UpdateUserInfo(request, token);
        }

        public void ChangeLanguage()
        {
            ApplicationLanguages.PrimaryLanguageOverride = Language;

        }

        public void LogOut()
        {
            if (_settingsService.HaveValue(Constants.AccessToken))
                _settingsService.RemoveValue(Constants.AccessToken);
        }

        public bool DarkModeIsEnabled
        {
            get => _isDarkMode;
            set
            {
                if (_isDarkMode == value)
                    return;

                _isDarkMode = value;
                _settingsService.AddOrUpdateValue(Constants.RequestedTheme, _isDarkMode 
                    ? ApplicationTheme.Dark.ToString() 
                    : ApplicationTheme.Light.ToString());
            }
        }
        
        public string Language { get; set; }
        public string Email { get; set; }
        public string Currency { get; set; }
        public string Login { get; set; }
    }
}