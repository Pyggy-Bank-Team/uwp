using System.Threading.Tasks;
using piggy_bank_uwp.Interface;
using piggy_bank_uwp.Models.Requests;

namespace piggy_bank_uwp.ViewModels.Users
{
    public class UserViewModel
    {
        private readonly IUserService _userService;

        public UserViewModel(IUserService userService)
            => _userService = userService;

        public async Task OnLogin(string userName, string password)
        {
            var request = new UserRequest
            {
                UserName = userName,
                Password = password
            };

            var accessToken = await _userService.GetAccessToken(request);
        }

        public async Task OnRegistration(string userName, string password)
        {
            //TODO: Add main currency
            var request = new UserRequest
            {
                UserName = userName,
                Password = password
            };

            var result = await _userService.RegistrationUser(request);
        }
    }
}