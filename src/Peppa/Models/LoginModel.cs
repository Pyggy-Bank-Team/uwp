using System.Threading;
using System.Threading.Tasks;
using Peppa.Dto;
using Peppa.Interface;
using Peppa.Interface.Models;
using Peppa.Interface.Services;

namespace Peppa.Models
{
    public class LoginModel : BaseModel, ILoginModel
    {
        private readonly IPiggyRepository _repository;
        private readonly IUserService _service;

        private string _error;
        private Currency[] _currencies;

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

        public Currency[] Currencies
        {
            get => _currencies;
            set
            {
                _currencies = value;
                OnPropertyChanged(nameof(Currencies));
            }
        }

        public Currency SelectedCurrency { get; set; }

        public Task Signin()
        {
            throw new System.NotImplementedException();
        }

        public Task Signup()
        {
            throw new System.NotImplementedException();
        }

        public async Task GetCurrencies(CancellationToken token)
        {
            var response = await _service.GetAvailableCurrencies(token);
            
        }
    }
}