using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Peppa.Contracts.Responses;
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

        #region Old

        public async Task CreatedAccount(Account account, CancellationToken token)
        {
            if (_service.IsAuthorized)
            {
                var request = new AccountResponse
                {
                    Balance = account.Balance,
                    Currency = account.Currency,
                    Title = account.Title,
                    Type = account.Type,
                    IsArchived = account.IsArchived
                };

                var createdAccount = await _service.CreateAccount(request, token);
                //TODO Add a log about service result
                if (createdAccount != null)
                {
                    if (await _repository.HaveAccount(account.Id, token))
                        await _repository.DeleteAccount(account.Id, token);

                    account.Id = createdAccount.Id;
                    account.Balance = createdAccount.Balance;
                    account.Currency = createdAccount.Currency;
                    account.Title = createdAccount.Title;
                    account.Type = createdAccount.Type;
                    account.IsArchived = createdAccount.IsArchived;
                    account.IsDeleted = createdAccount.IsDeleted;
                    account.IsSynchronized = true;
                }
            }

            await _repository.CreateAccount(account, token);
        }

        public async Task DeleteAccount(int id, CancellationToken token)
        {
            if (_service.IsAuthorized)
            {
                var isSuccessful = await _service.DeleteAccount(id, token);
                //TODO Add a log about service result
                if (isSuccessful)
                {
                    //TODO Added in database
                    return;
                }
            }

            //TODO Add case for non-login user
        }

        public async Task UpdateAccount(Account account, CancellationToken token)
        {
            if (_service.IsAuthorized)
            {
                var request = new AccountResponse
                {
                    Id = account.Id,
                    Balance = account.Balance,
                    Currency = account.Currency,
                    Title = account.Title,
                    Type = account.Type,
                    IsArchived = account.IsArchived
                };

                var isSuccessful = await _service.UpdateAccount(request, token);
                //TODO Add a log about service result
                if (isSuccessful)
                {
                    //TODO Added in database
                }
            }

            //TODO Add case for non-login user
        }

        public async Task<Account[]> GetAccounts(CancellationToken token)
        {
            //If user is logged in then sync accounts
            if (_service.IsAuthorized)
            {
                var accounts = await _service.GetAccounts(true, token);
                if (accounts != null)
                {
                    foreach (var account in accounts)
                    {
                        var accountEntity = new Account
                        {
                            Id = account.Id,
                            Balance = account.Balance,
                            Currency = GetSymbol(account.Currency),
                            Title = account.Title,
                            Type = account.Type,
                            IsArchived = account.IsArchived,
                            IsDeleted = account.IsDeleted,
                            IsSynchronized = true
                        };

                        if (await _repository.HaveAccount(account.Id, token))
                            await _repository.UpdateAccount(accountEntity, token);
                        else
                            await _repository.CreateAccount(accountEntity, token);
                    }
                }
            }

            return await _repository.GetAccounts(token);
        }

        public void Dispose()
            => _repository?.Dispose();

        private string GetSymbol(string currency)
        {
            if (string.IsNullOrWhiteSpace(currency))
                return null;

            try
            {
                return _availableCurrencies[currency];
            }
            catch
            {
                //TODO log
            }

            return null;
        }

        #endregion

        public async Task<IAccountModel> CreateNewAccount(CancellationToken token)
        {
            var user = await _repository.GetUser(token);
            var entity = new Account
            {
                Currency = CurrencyHelper.GetSymbol(user.CurrencyBase),
                Type = AccountType.Card
            };
            return new AccountModel(entity);
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
                Accounts.Add(new AccountModel(account));

            TotalAmount = Accounts.Sum(a => a.Balance);
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

        public List<IAccountModel> Accounts { get; }

        public double TotalAmount { get; private set; }
    }
}