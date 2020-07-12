using System.Threading.Tasks;
using piggy_bank_uwp.Interface;
using piggy_bank_uwp.Models.Requests;
using piggy_bank_uwp.Models.Responses;

namespace piggy_bank_uwp.Services.PiggyService
{
    public partial class PiggyService : IUserService
    {
        public Task<bool> RegistrationUser(UserRequest request)
        {
            throw new System.NotImplementedException();
        }

        public Task<AccessTokenResponse> GetAccessToken(UserRequest userRequest)
        {
            throw new System.NotImplementedException();
        }
    }
}