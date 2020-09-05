using System.Threading;
using System.Threading.Tasks;
using Peppa.Interface;
using Peppa.Interface.Models;
using Peppa.Interface.Services;
using Peppa.Context.Entities;
using Peppa.Contracts;

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
                var request = new AccountContract
                {
                    Balance = account.Balance,
                    Currency = account.Currency,
                    Title = account.Title,
                    Type = account.Type,
                    IsArchived = account.IsArchived
                };

                var isSuccessful = await _service.CreateAccount(request);
                //TODO Add a log about service result
                if (isSuccessful)
                {
                    //TODO Added in database
                    return;
                }
            }
            
            //TODO Add case for non-login user
        }

        public async Task DeleteAccount(int id, CancellationToken token)
        {
            if (_service.IsAuthorized)
            {
                var isSuccessful = await _service.DeleteAccount(id);
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
                var request = new AccountContract
                {
                    Id = account.Id,
                    Balance = account.Balance,
                    Currency = account.Currency,
                    Title = account.Title,
                    Type = account.Type,
                    IsArchived = account.IsArchived
                };

                var isSuccessful = await _service.UpdateAccount(request);
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
                var accounts = await _service.GetAccounts();
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
                            IsDeleted = account.IsDeleted
                        };
                        
                        if (await _repository.HaveAccount(account.Id, token))
                            await _repository.UpdateAccount(accountEntity, token);
                        else
                            await _repository.CreateAccount(accountEntity, token);
                    }
                }
            }

            return await _repository.GetAccounts();
        }

        public void Dispose()
            => _repository?.Dispose();
    }
}