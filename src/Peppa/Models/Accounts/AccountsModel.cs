using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Peppa.Contracts.Requests.Accounts;
using Peppa.Enums;
using Peppa.Helpers;
using Peppa.Interface;
using Peppa.Interface.Models.Accounts;
using Peppa.Interface.Services;
using Account = Peppa.Context.Entities.Account;

namespace Peppa.Models.Accounts
{
    public class AccountsModel : IAccountsModel
    {
        private readonly IAccountService _service;
        private readonly IPiggyRepository _repository;

        public AccountsModel(IAccountService service, IPiggyRepository repository)
        {
            _service = service;
            _repository = repository;
            Accounts = new List<IAccountModel>();
        }

        public IAccountModel CreateNewAccount()
        {
            var entity = new Account
            {
                Currency = CurrencyBase,
                Type = AccountType.Card
            };

            return new AccountModel(entity, _service, isNew: true);
        }

        public async Task UpdateAccounts(CancellationToken token)
        {
            if (_service.IsAuthorized)
            {
                var accounts = await _service.GetAccounts(true, token);
                if (accounts != null)
                {
                    foreach (var receivedAccount in accounts)
                    {
                        var entity = new Account
                        {
                            Id = receivedAccount.Id,
                            Balance = receivedAccount.Balance,
                            Currency = CurrencyHelper.GetSymbol(receivedAccount.Currency),
                            Title = receivedAccount.Title,
                            Type = receivedAccount.Type,
                            IsArchived = receivedAccount.IsArchived,
                            IsDeleted = receivedAccount.IsDeleted,
                            IsSynchronized = true
                        };

                        if (await _repository.HaveAccount(receivedAccount.Id, token))
                            await _repository.UpdateAccount(entity, token);
                        else
                            await _repository.CreateAccount(entity, token);
                    }
                }
            }

            Accounts.Clear();

            foreach (var account in await _repository.GetAccounts(token))
                Accounts.Add(new AccountModel(account, _service));

            TotalAmount = Accounts.Where(a => !a.IsArchived).Sum(a => a.Balance);

            var user = await _repository.GetUser(token);
            CurrencyBase = user.CurrencyBase;
        }

        public async Task SaveAccount(IAccountModel newAccount, CancellationToken token)
        {
            if (newAccount == null)
                return;

            await newAccount.Save(token);
        }

        public async Task UpdateAccount(IAccountModel account, CancellationToken token)
        {
            if (account == null)
                return;

            await account.Update(token);
        }

        public async Task DeleteAccount(IAccountModel account, CancellationToken token)
        {
            if (account == null)
                return;

            await account.Delete(token);
        }

        public void Dispose()
            => _repository?.Dispose();

        public List<IAccountModel> Accounts { get; }

        public double TotalAmount { get; private set; }

        public string CurrencyBase { get; set; }
    }
}