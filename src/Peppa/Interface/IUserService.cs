using piggy_bank_uwp.Models.Requests;
using piggy_bank_uwp.Models.Responses;
using System.Threading.Tasks;

namespace piggy_bank_uwp.Interface
{
    public interface IUserService
    {
        Task<bool> RegistrationUser(UserRequest request);

        Task<AccessTokenResponse> GetAccessToken(UserRequest userRequest);
    }
}
