using Microsoft.Extensions.DependencyInjection;
using piggy_bank_uwp.Interface;
using piggy_bank_uwp.Services.PiggyService;

namespace piggy_bank_uwp.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void DependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<IAccountService, PiggyService>();
            services.AddScoped<IUserService, PiggyService>();
            services.AddHttpClient();
        }
    }
}
