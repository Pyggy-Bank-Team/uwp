using System.Threading;
using System.Threading.Tasks;
using Peppa.Contracts;
using Peppa.Contracts.Responses;

namespace Peppa.Interface.Services
{
    public interface IAccountService : IAuthorization
    {
        Task<AccountResponse[]> GetAccounts(bool showArchivedAccounts, CancellationToken token);

        Task<AccountResponse> CreateAccount(AccountResponse response, CancellationToken token);

        Task<bool> UpdateAccount(AccountResponse response, CancellationToken token);

        Task<bool> DeleteAccount(int id, CancellationToken token);
    }
}