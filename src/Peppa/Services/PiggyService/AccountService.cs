using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Peppa.Contracts.Requests.Accounts;
using Peppa.Contracts.Responses;
using Peppa.Interface.InternalServices;
using Peppa.Interface.Services;

namespace Peppa.Services.PiggyService
{
    public class AccountService : PiggyServiceBase, IAccountService
    {
        public AccountService(IHttpClientFactory httpClientFactory,ISettingsService settingsService) 
            : base(httpClientFactory, settingsService)
        {
        }

        public Task<AccountResponse[]> GetAccounts(bool showDeletedAccounts, CancellationToken token)
            => Get<AccountResponse[]>($"accounts?all={showDeletedAccounts}", token);

        public async Task<AccountResponse> CreateAccount(CreateAccountRequest response, CancellationToken token)
        {
            var result = await Post<AccountResponse, CreateAccountRequest>("accounts", response, token);
            return result.IsSuccess ? result.Ok : null;
        }

        public Task<bool> UpdateAccount(UpdateAccountRequest response, CancellationToken token)
            => Put($"accounts/{response.Id}",response, token);

        public Task<bool> DeleteAccount(long id, CancellationToken token)
            => Delete($"accounts/{id}", token);
    }
}