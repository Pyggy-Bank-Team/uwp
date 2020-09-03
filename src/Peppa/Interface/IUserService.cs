using piggy_bank_uwp.Models;
using System.Threading.Tasks;
using piggy_bank_uwp.Contracts.Requests;
using piggy_bank_uwp.Contracts.Responses;

namespace piggy_bank_uwp.Interface
{
    public interface IUserService
    {
        Task<RegitrationResult> RegistrationUser(UserRequest request);

        Task<AccessTokenResponse> GetAccessToken(UserRequest userRequest);

        Task<AvailableCurrency[]> GetAvailableCurrencies();
    }
}
