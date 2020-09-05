using System;
using System.Threading.Tasks;
using Peppa.Interface;
using Peppa.Interface.Models;
using Peppa.Interface.Services;
using Peppa.Workers;
using Peppa.Context.Entities;

namespace Peppa.Models
{
    public class AccountsModel : IAccountsModel
    {
        private readonly IAccountService _service;
        private readonly IPiggyRepository _repository;

        public AccountsModel(IAccountService service, IPiggyRepository repository)
            => (_service, _repository) = (service, repository);


        public async Task CreatedAccount()
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAccount()
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAccount()
        {
            throw new NotImplementedException();
        }

        public async Task<Account[]> GetAccounts()
        {
            //If user is logged in then sync accounts
            if (SettingsWorker.Current.HaveValue(Constants.AccessToken))
            {
                var accounts = await _service.GetAccounts();
                if (accounts != null)
                {
                    
                }
            }

            return await _repository.GetAccounts();
        }

        public void Dispose()
            => _repository?.Dispose();
    }
}