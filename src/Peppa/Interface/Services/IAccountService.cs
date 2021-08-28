using System.Threading;
using System.Threading.Tasks;
using Peppa.Contracts.Requests.Accounts;
using Peppa.Contracts.Responses;

namespace Peppa.Interface.Services
{
    public interface IAccountService : IAuthorization
    {
        Task<AccountResponse[]> GetAccounts(bool showDeletedAccounts, CancellationToken token);

        Task<AccountResponse> CreateAccount(CreateAccountRequest response, CancellationToken token);

        Task<bool> UpdateAccount(UpdateAccountRequest response, CancellationToken token);

        Task<bool> DeleteAccount(long id, CancellationToken token);
    }
}