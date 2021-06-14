using System.Threading;
using System.Threading.Tasks;
using Peppa.Contracts.Requests;
using Peppa.Contracts.Responses;
using Peppa.Dto;

namespace Peppa.Interface.Services
{
    public interface IReportService : IAuthorization
    {
        Task<ItemReport[]> GetChartByCategories(GetReportRequest request, CancellationToken token);
        Task<ChartByExpenseDaysResponse[]> GetChartByExpenseDays(GetChartByExpensePerDaysRequest request, CancellationToken token);
    }
}