using System.Threading;
using System.Threading.Tasks;
using Peppa.Contracts.Requests;
using Peppa.Contracts.Responses;

namespace Peppa.Interface.Services
{
    public interface IReportService : IAuthorization
    {
        Task<ChartByCategoriesResponse[]> GetChartByCategories(GetChartByCategoriesRequest request, CancellationToken token);
        Task<ChartByExpenseDaysResponse[]> GetChartByExpenseDays(GetChartByExpensePerDaysRequest request, CancellationToken token);
    }
}