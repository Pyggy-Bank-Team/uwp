using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Peppa.Contracts.Requests;
using Peppa.Contracts.Responses;
using Peppa.Contracts.Results;
using Peppa.Interface.InternalServices;
using Peppa.Interface.Services;

namespace Peppa.Services.PiggyService
{
    public class UserService : PiggyServiceBase, IUserService
    {
        public UserService(IHttpClientFactory httpClientFactory, ISettingsService settingsService) 
            : base(httpClientFactory, settingsService)
        {
        }

        public Task<ServiceResult<AccessTokenResponse>> RegistrationUser(CreateUserRequest request, CancellationToken token)
            => Post<AccessTokenResponse, CreateUserRequest>("users", request, token);

        public Task<ServiceResult<AccessTokenResponse>> GetAccessToken(GetTokenRequest request, CancellationToken token)
            => Post<AccessTokenResponse, GetTokenRequest>("tokens/connect", request, token);

        public Task<CurrencyResponse[]> GetAvailableCurrencies(CancellationToken token)
            => Get<CurrencyResponse[]>("currencies", token);

        public Task<UserInfoResponse> GetUserInfo(CancellationToken token)
            => Get<UserInfoResponse>("users/userInfo", token);

        public Task<bool> UpdateUserInfo(UpdateUserInfoRequest request, CancellationToken token)
            => Patch("users", request, token);
    }
}