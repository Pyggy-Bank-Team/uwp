using Microsoft.Extensions.DependencyInjection;
using Peppa.Services.PiggyService;
using Peppa.ViewModels.Accounts;
using Peppa.ViewModels.Users;
using Peppa.Interface.Services;
using Peppa.Interface.Models;
using Peppa.Models;
using Peppa.Interface;
using Peppa.Interface.InternalServices;
using Peppa.Interface.ViewModels;
using Peppa.Interface.WindowsService;
using Peppa.Models.Operations;
using Peppa.Repositories;
using Peppa.Services.Internal;
using Peppa.Services.Windows;
using Peppa.ViewModels.Categories;
using Peppa.ViewModels.Login;
using Peppa.ViewModels.Operations;
using Peppa.ViewModels.Report;

namespace Peppa.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void DependencyInjectionSetup(this IServiceCollection services)
        {
            //Models
            services.AddScoped<IAccountsModel, AccountsModel>();
            services.AddScoped<ICategoriesModel, CategoriesModel>();
            services.AddScoped<IOperationsModel, OperationsModel>();
            services.AddScoped<IReportModel, ReportModel>();
            services.AddScoped<ILoginModel, LoginModel>();
            //Services
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IUserService, PiggyService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IReportService, ReportService>();
            services.AddScoped<ISettingsService, SettingsService>();
            services.AddScoped<IOperationService, OperationService>();
            services.AddScoped<ILocalizationService, LocalizationService>();
            services.AddScoped<IToastService, ToastService>();
            //Repositories
            services.AddScoped<IPiggyRepository, PiggyRepository>();
            //ViewModels
            services.AddScoped<ILoginViewModel, LoginViewModel>();
            services.AddSingleton<CategoriesViewModel>();
            services.AddSingleton<AccountsViewModel>();
            services.AddSingleton<UserViewModel>();
            services.AddSingleton<OperationsViewModel>();
            services.AddSingleton<ReportViewModel>();
            services.AddHttpClient();
        }
    }
}
