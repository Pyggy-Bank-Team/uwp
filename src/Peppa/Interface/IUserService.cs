using System.Threading.Tasks;
using Peppa.Contracts.Requests;
using Peppa.Contracts.Responses;
using Peppa.Models;

namespace Peppa.Interface
{
    public interface IUserService
    {
        Task<RegitrationResult> RegistrationUser(UserRequest request);

        Task<AccessTokenResponse> GetAccessToken(UserRequest userRequest);

        Task<AvailableCurrency[]> GetAvailableCurrencies();
    }
}
