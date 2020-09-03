using System.Threading.Tasks;
using Peppa.Contracts.Requests;
using Peppa.Entities;

namespace Peppa.Interface
{
    public interface IAccountService
    {
        Task<Account[]> GetAccounts();

        Task<bool> CreateAccount(AccountRequest request);

        Task UpdateAccount(AccountRequest request);
    }
}