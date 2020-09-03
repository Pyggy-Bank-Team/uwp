using System;
using System.Threading.Tasks;
using Peppa.Entities;
using Peppa.Interface;
using Peppa.Workers;

namespace Peppa.Models
{
    public class AccountsModel
    {
        private readonly IAccountService _service;
        private readonly DbWorker _dbWorker;

        public void AddAccount()
        {

        }

        public void DeleteAccount()
        {

        }

        public void UpdateAccount()
        {

        }

        public Task<Account[]> GetAccounts()
        {
            throw new NotImplementedException();
        }
    }
}
