using Microsoft.Extensions.DependencyInjection;
using Peppa.Services.PiggyService;
using Peppa.ViewModels.Accounts;
using Peppa.ViewModels.Users;
using Peppa.Interface.Services;
using Peppa.Interface.Models;
using Peppa.Models;
using Peppa.Interface;
using Peppa.Repositories;
using Peppa.ViewModels.Categories;
using Peppa.ViewModels.Diagram;
using Peppa.ViewModels.Operations;

namespace Peppa.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void DependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IUserService, PiggyService>();
            services.AddScoped<IAccountsModel, AccountsModel>();
            services.AddScoped<IPiggyRepository, PiggyRepository>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICategoriesModel, CategoriesModel>();
            services.AddScoped<IOperationService, OperationService>();
            services.AddScoped<IOperationsModel, OperationsModel>();
            services.AddScoped<IReportService, ReportService>();
            services.AddScoped<IReportModel, ReportModel>();
            services.AddSingleton<CategoriesViewModel>();
            services.AddSingleton<AccountsViewModel>();
            services.AddSingleton<UserViewModel>();
            services.AddSingleton<OperationsViewModel>();
            services.AddSingleton<DiagramViewModel>();
            services.AddHttpClient();
        }
    }
}
