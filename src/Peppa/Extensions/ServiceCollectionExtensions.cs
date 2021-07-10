using Microsoft.Extensions.DependencyInjection;
using Peppa.Services.PiggyService;
using Peppa.ViewModels.Accounts;
using Peppa.Interface.Services;
using Peppa.Interface.Models;
using Peppa.Models;
using Peppa.Interface;
using Peppa.Interface.InternalServices;
using Peppa.Interface.Models.Accounts;
using Peppa.Interface.Models.Categories;
using Peppa.Interface.Models.Operations;
using Peppa.Interface.Models.Settings;
using Peppa.Interface.ViewModels;
using Peppa.Interface.WindowsService;
using Peppa.Models.Accounts;
using Peppa.Models.Categories;
using Peppa.Models.Operations;
using Peppa.Models.Reports;
using Peppa.Repositories;
using Peppa.Services.Internal;
using Peppa.Services.Windows;
using Peppa.ViewModels.Categories;
using Peppa.ViewModels.Login;
using Peppa.ViewModels.Operations;
using Peppa.ViewModels.Reports;
using Peppa.ViewModels.Settings;

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
            services.AddScoped<IReportsModel, ReportsModel>();
            services.AddScoped<ILoginModel, LoginModel>();
            services.AddScoped<ISettingsModel, SettingsModel>();
            //Services
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IUserService, UserService>();
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
            services.AddScoped<IOperationsViewModel, OperationsViewModel>();
            services.AddScoped<ICategoriesViewModel, CategoriesViewModel>();
            services.AddSingleton<AccountsViewModel>();
            services.AddScoped<IReportsViewModel, ReportsViewModel>();
            services.AddScoped<ISettingsViewModel, SettingsViewModel>();
            services.AddHttpClient();
        }
    }
}
