using System.Threading;
using System.Threading.Tasks;
using Peppa.Context.Entities;
using Peppa.Contracts.Requests.Accounts;
using Peppa.Enums;
using Peppa.Interface.Models.Accounts;
using Peppa.Interface.Services;

namespace Peppa.Models.Accounts
{
    public class AccountModel : IAccountModel
    {
        private readonly IAccountService _service;

        public AccountModel(Account model, IAccountService service)
        {
            _service = service;
            Balance = model.Balance;
            Currency = model.Currency;
            Id = model.Id;
            Type = model.Type;
            IsArchived = model.IsArchived;
            IsSynchronized = model.IsSynchronized;
        }

        public long Id { get; }

        public string Title { get; set; }

        public string Currency { get; set; }

        public double Balance { get; set; }

        public AccountType Type { get; set; }

        public bool IsArchived { get; set; }

        public bool IsSynchronized { get; set; }

        public async Task Save(CancellationToken token)
        {
            if (!_service.IsAuthorized)
                return;
            
            var request = new CreateAccountRequest
            {
                Balance = Balance,
                Currency = Currency,
                Title = Title,
                Type = Type,
                IsArchived = IsArchived
            };

            await _service.CreateAccount(request, token);
        }

        public async Task Update(CancellationToken token)
        {
            if (!_service.IsAuthorized)
                return;
            
            var request = new UpdateAccountRequest
            {
                Balance = Balance,
                Currency = Currency,
                Id = Id,
                Title = Title,
                Type = Type,
                IsArchived = IsArchived
            };

            await _service.UpdateAccount(request, token);
        }

        public async Task Delete(CancellationToken token)
        {
            if (!_service.IsAuthorized)
                return;
            
            await _service.DeleteAccount(Id, token);
        }
    }
}