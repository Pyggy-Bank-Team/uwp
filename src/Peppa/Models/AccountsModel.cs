using System.Threading;
using System.Threading.Tasks;
using Peppa.Interface;
using Peppa.Interface.Models;
using Peppa.Interface.Services;
using Peppa.Contracts;
using Peppa.Contracts.Responses;
using Account = Peppa.Context.Entities.Account;

namespace Peppa.Models
{
    public class AccountsModel : IAccountsModel
    {
        private readonly IAccountService _service;
        private readonly IPiggyRepository _repository;

        public AccountsModel(IAccountService service, IPiggyRepository repository)
            => (_service, _repository) = (service, repository);


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
                var accounts = await _service.GetAccounts(token);
                if (accounts != null)
                {
                    foreach (var account in accounts)
                    {
                        var accountEntity = new Account
                        {
                            Id = account.Id,
                            Balance = account.Balance,
                            Currency = account.Currency,
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
    }
}