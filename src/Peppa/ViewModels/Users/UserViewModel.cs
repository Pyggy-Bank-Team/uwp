using piggy_bank_uwp.Interface;

namespace piggy_bank_uwp.ViewModels.Users
{
    public class UserViewModel
    {
        private readonly IUserService _userService;

        public UserViewModel(IUserService userService)
            => _userService = userService;

    }
}
