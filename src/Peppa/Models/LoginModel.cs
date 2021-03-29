using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Peppa.Context.Entities;
using Peppa.Contracts.Requests;
using Peppa.Dto;
using Peppa.Interface;
using Peppa.Interface.Models;
using Peppa.Interface.Services;
using Peppa.Workers;

namespace Peppa.Models
{
    public class LoginModel : BaseModel, ILoginModel
    {
        private readonly IPiggyRepository _repository;
        private readonly IUserService _service;

        private string _error;

        public LoginModel(IPiggyRepository repository, IUserService service)
        {
            _repository = repository;
            _service = service;
        }

        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public string Error
        {
            get => _error;
            set
            {
                if (_error != value)
                {
                    _error = value;
                    OnPropertyChanged(nameof(Error));
                }
            }
        }

        public List<Currency> Currencies { get; set; }

        public Currency SelectedCurrency { get; set; }

        public async Task Signin(CancellationToken token)
        {
            var request = new GetTokenRequest
            {
                UserName = UserName,
                Password = Password
            };

            var response = await _service.GetAccessToken(request, token);
            if (response == null)
            {
                Error = Localize.GetTranslateByKey(Localize.WarringCostContent);
                return;
            }
            
            SettingsWorker.Current.SaveValue(Constants.AccessToken, response.AccessToken);
            await UpdateUserInfo(token);
        }

        public Task Signup(CancellationToken token)
        {
            throw new System.NotImplementedException();
        }

        public async Task UpdateCurrencies(CancellationToken token)
        {
            var response = await _service.GetAvailableCurrencies(token);
            if (response != null)
            {
                foreach (var currency in response)
                    Currencies.Add(new Currency {Code = currency.Code, Symbol = currency.Symbol});
                
                OnPropertyChanged(nameof(Currencies));
            }
        }

        private async Task UpdateUserInfo(CancellationToken token)
        {
            var response = await _service.GetUserInfo(token);
            if (response != null)
            {
                var user = new User
                {
                    Email = response.Email,
                    CurrencyBase = response.CurrencyBase,
                    UserName = response.UserName
                };

                await _repository.CreateUser(user, token);
            }
        }
    }
}