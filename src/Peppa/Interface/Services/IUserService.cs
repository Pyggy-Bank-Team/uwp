using System.Threading.Tasks;
using Peppa.Contracts.Requests;
using Peppa.Contracts.Responses;
using Peppa.Models;
using piggy_bank_uwp.Contracts.Requests;

namespace Peppa.Interface.Services
{
    public interface IUserService : IAuthorization
    {
        Task<RegitrationResult> RegistrationUser(UserRequest request);

        Task<AccessTokenResponse> GetAccessToken(GetTokenRequest request);

        Task<AvailableCurrency[]> GetAvailableCurrencies();
    }
}
