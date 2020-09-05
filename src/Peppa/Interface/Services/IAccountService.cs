using System.Threading.Tasks;
using Peppa.Contracts.Requests;
using Peppa.Contracts;
using Peppa.Context.Entities;

namespace Peppa.Interface.Services
{
    public interface IAccountService : IAuthorization
    {
        Task<AccountContract[]> GetAccounts();

        Task<bool> CreateAccount(AccountContract contract);

        Task UpdateAccount(AccountContract contract);
    }
}