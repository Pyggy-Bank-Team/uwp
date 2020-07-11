using piggy_bank_uwp.Entities;
using piggy_bank_uwp.Interface;
using System;
using System.Threading.Tasks;

namespace piggy_bank_uwp.Services.PiggyService
{
    public partial class PiggyService : IAccountService
    {
        public Task<Account[]> GetAccounts()
        {
            throw new NotImplementedException();
        }
    }
}
