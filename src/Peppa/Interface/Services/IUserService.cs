using System.Threading;
using System.Threading.Tasks;
using Peppa.Contracts.Requests;
using Peppa.Contracts.Responses;
using Peppa.Contracts.Results;

namespace Peppa.Interface.Services
{
    public interface IUserService : IAuthorization
    {
        Task<ServiceResult<AccessTokenResponse>> RegistrationUser(CreateUserRequest request, CancellationToken token);

        Task<AccessTokenResponse> GetAccessToken(GetTokenRequest request, CancellationToken token);

        Task<CurrencyResponse[]> GetAvailableCurrencies(CancellationToken token);

        Task<UserInfoResponse> GetUserInfo(CancellationToken token);

        Task<bool> UpdateUserInfo(UpdateUserInfoRequest request, CancellationToken token);
    }
}
