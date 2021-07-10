using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Peppa.Context.Entities;
using Peppa.Contracts.Requests;
using Peppa.Dto;
using Peppa.Enums;
using Peppa.Helpers;
using Peppa.Interface;
using Peppa.Interface.InternalServices;
using Peppa.Interface.Models;
using Peppa.Interface.Services;

namespace Peppa.Models
{
    public class LoginModel : ILoginModel
    {
        private readonly IPiggyRepository _repository;
        private readonly IUserService _service;
        private readonly ISettingsService _settingsService;

        public LoginModel(IPiggyRepository repository, IUserService service, ISettingsService settingsService)
        {
            _repository = repository;
            _service = service;
            _settingsService = settingsService;
            Currencies = new List<Currency>();
        }

        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }
        public List<Currency> Currencies { get; set; }
        public Currency Currency { get; set; }

        public async Task<SigninResultEnum> Signin(CancellationToken token)
        {
            var request = new GetTokenRequest
            {
                UserName = UserName,
                Password = Password
            };

            var response = await _service.GetAccessToken(request, token);
            if (response == null)
            {
                Error = _localizationService.GetTranslateByKey(Localization.NotValidUserNameOrPassword);
                return;
            }

            _settingsService.AddOrUpdateValue(Constants.AccessToken, response.AccessToken);
            await UpdateUserInfo(token);
        }

        public async Task Signup(CancellationToken token)
        {
            if (Password != ConfirmPassword)
            {
                Error = _localizationService.GetTranslateByKey(Localization.PasswordAndConfirmPasswordNotEquals);
                return;
            }

            if (Currency?.Code == null)
            {
                Error = _localizationService.GetTranslateByKey(Localization.CurrencyNotSelected);
                return;
            }

            var request = new CreateUserRequest
            {
                Email = Email,
                Password = Password,
                CurrencyBase = Currency.Code,
                UserName = UserName
            };

            var response = await _service.RegistrationUser(request, token);

            switch (response.Result)
            {
                case CreateUserResultEnum.Successful:
                    _settingsService.AddOrUpdateValue(Constants.AccessToken, response.Token.AccessToken);
                    await UpdateUserInfo(token);
                    break;
                case CreateUserResultEnum.UserNotCreated:
                    Error = _localizationService.GetTranslateByKey(Localization.UserNotCreated);
                    break;
                case CreateUserResultEnum.PasswordInvalid:
                    Error = _localizationService.GetTranslateByKey(Localization.PasswordInvalid);
                    break;
                default:
                    Error = _localizationService.GetTranslateByKey(Localization.OopsError);
                    break;
            }
        }

        public async Task UpdateCurrencies(CancellationToken token)
        {
            var response = await _service.GetAvailableCurrencies(token);
            if (response != null)
            {
                foreach (var currency in response)
                    Currencies.Add(new Currency {Code = currency.Code, Symbol = currency.Symbol});
            }
            //If we can't get available currencies from services then we set user's currency
            else
                Currencies.Add(new Currency {Symbol = NumberFormatInfo.CurrentInfo.CurrencySymbol, Code = RegionInfo.CurrentRegion.ISOCurrencySymbol});

            Currency = Currencies.First();
        }

        private async Task UpdateUserInfo(CancellationToken token)
        {
            var response = await _service.GetUserInfo(token);
            if (response != null)
            {
                var user = new User
                {
                    Email = response.Email,
                    CurrencyBase = CurrencyHelper.GetSymbol(response.CurrencyBase),
                    UserName = response.UserName
                };

                await _repository.CreateUser(user, token);
            }
        }
    }
}