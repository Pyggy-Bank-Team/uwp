using Microsoft.Extensions.DependencyInjection;
using Peppa.Interface;
using Peppa.Services.PiggyService;
using Peppa.ViewModels.Accounts;
using Peppa.ViewModels.Users;

namespace Peppa.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void DependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<IAccountService, PiggyService>();
            services.AddScoped<IUserService, PiggyService>();
            services.AddSingleton<AccountsViewModel>();
            services.AddSingleton<UserViewModel>();
            services.AddHttpClient();
        }
    }
}
