using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Peppa.Contracts.Responses;
using Peppa.Interface.Services;

namespace Peppa.Services.PiggyService
{
    public class AccountService : PiggyServiceBase, IAccountService
    {
        public AccountService(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
        }

        public Task<AccountResponse[]> GetAccounts(bool showArchivedAccounts, CancellationToken token)
            => Get<AccountResponse[]>($"accounts?all={showArchivedAccounts}", token);

        public Task<AccountResponse> CreateAccount(AccountResponse response, CancellationToken token)
            => Post<AccountResponse, AccountResponse>("accounts", response, token);

        public Task<bool> UpdateAccount(AccountResponse response, CancellationToken token)
            => Put($"accounts/{response.Id}",response, token);

        public Task<bool> DeleteAccount(int id, CancellationToken token)
            => Delete($"accounts/{id}", token);
    }
}