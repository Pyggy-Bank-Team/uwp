using System.Threading;
using System.Threading.Tasks;
using Peppa.Contracts.Requests;
using Peppa.Contracts.Responses;
using Peppa.Models;

namespace Peppa.Interface.Services
{
    public interface IUserService : IAuthorization
    {
        Task<RegitrationResult> RegistrationUser(UserRequest request, CancellationToken token);

        Task<AccessTokenResponse> GetAccessToken(GetTokenRequest request, CancellationToken token);

        Task<CurrencyResponse[]> GetAvailableCurrencies(CancellationToken token);

        Task<UserInfoResponse> GetUserInfo(CancellationToken token);
    }
}
