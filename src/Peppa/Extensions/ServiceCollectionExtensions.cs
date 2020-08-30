using Microsoft.Extensions.DependencyInjection;
using piggy_bank_uwp.Interface;
using piggy_bank_uwp.Services.PiggyService;
using piggy_bank_uwp.ViewModels.Accounts;
using piggy_bank_uwp.ViewModels.Users;

namespace piggy_bank_uwp.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void DependencyInjection(this IServiceCollection services)
        {
            services.AddTransient<IAccountService, PiggyService>();
            services.AddTransient<IUserService, PiggyService>();
            services.AddSingleton<AccountsViewModel>();
            services.AddSingleton<UserViewModel>();
            services.AddHttpClient();
        }
    }
}
