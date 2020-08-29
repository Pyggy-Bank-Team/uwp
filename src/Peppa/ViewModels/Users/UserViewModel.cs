using System.Threading.Tasks;
using piggy_bank_uwp.Interface;
using piggy_bank_uwp.Models.Requests;
using piggy_bank_uwp.Workers;

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

            if (accessToken != null)
            {
                SettingsWorker.Current.SaveValue(Constants.AccessToken, accessToken.AccessToken);
                SettingsWorker.Current.SaveValue(Constants.RefreshToken, accessToken.RefreshToken);
            }
        }

        public async Task OnRegistration(string userName, string password, string currency)
        {
            //TODO: Add main currency
            var request = new UserRequest
            {
                UserName = userName,
                Password = password,
                CurrencyBase = currency
            };

            var result = await _userService.RegistrationUser(request);
        }
    }
}