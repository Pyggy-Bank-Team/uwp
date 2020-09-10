using System.Threading.Tasks;
using Peppa.Contracts;

namespace Peppa.Interface.Services
{
    public interface IAccountService : IAuthorization
    {
        Task<AccountContract[]> GetAccounts();

        Task<AccountContract> CreateAccount(AccountContract contract);

        Task<bool> UpdateAccount(AccountContract contract);

        Task<bool> DeleteAccount(int id);
    }
}