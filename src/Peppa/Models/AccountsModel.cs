using piggy_bank_uwp.Entities;
using piggy_bank_uwp.Interface;
using piggy_bank_uwp.Workers;
using System.Threading.Tasks;

namespace piggy_bank_uwp.Models
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

        }
    }
}
