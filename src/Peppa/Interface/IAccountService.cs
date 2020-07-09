using System.Threading.Tasks;
using piggy_bank_uwp.Entities;

namespace piggy_bank_uwp.Interface
{
    public interface IAccountService
    {
        Task<Account[]> GetAccounts();
    }
}