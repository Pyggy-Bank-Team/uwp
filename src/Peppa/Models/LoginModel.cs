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

            var result = await _service.GetAccessToken(request, token);
            if (!result.IsSuccess)
            {
                var error = result.Error;
                switch (error.Type)
                {
                    case "InvalidPassword":
                        return SigninResultEnum.InvalidPassword;
                    case "UserNotFound":
                        return SigninResultEnum.UserNotFound;
                    default:
                        return SigninResultEnum.UnknownError;
                }
            }

            var response = result.Ok;
            _settingsService.AddOrUpdateValue(Constants.AccessToken, response.AccessToken);
            await UpdateUserInfo(token);

            return SigninResultEnum.Ok;
        }

        public async Task<SignupResultEnum> Signup(CancellationToken token)
        {
            if (Password != ConfirmPassword)
                return SignupResultEnum.PasswordAndConfirmPasswordNotEquals;

            if (Currency?.Code == null)
                return SignupResultEnum.CurrencyNotSelected;

            if (string.IsNullOrWhiteSpace(Email))
                return SignupResultEnum.EmailEmptyOrNull;

            var request = new CreateUserRequest
            {
                Email = Email,
                Password = Password,
                CurrencyBase = Currency.Code,
                UserName = UserName
            };

            var result = await _service.RegistrationUser(request, token);

            if (!result.IsSuccess)
            {
                var error = result.Error;
                switch (error.Type)
                {
                    case "PasswordInvalid":
                        return SignupResultEnum.PasswordInvalid;
                    case "DuplicateUserName":
                        return SignupResultEnum.DuplicateUserName;
                    case "InvalidUserName":
                        return SignupResultEnum.InvalidUserName;
                    case "UserNotCreated":
                        return SignupResultEnum.UserNotCreated;
                    default:
                        return SignupResultEnum.UnknownError;
                }
            }

            var response = result.Ok;
            _settingsService.AddOrUpdateValue(Constants.AccessToken, response.AccessToken);
            await UpdateUserInfo(token);

            return SignupResultEnum.Ok;
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