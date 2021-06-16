using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Peppa.Contracts.Requests;
using Peppa.Contracts.Responses;
using Peppa.Dto;
using Peppa.Interface.Services;

namespace Peppa.Services.PiggyService
{
    public class ReportService : PiggyServiceBase, IReportService
    {
        public ReportService(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
        }

        public Task<ItemReport[]> GetChartByCategories(GetReportRequest request, CancellationToken token)
            => Post<ItemReport[], GetReportRequest>("reports/chart/byCategories", request, token);

        public Task<ChartByExpenseDaysResponse[]> GetChartByExpenseDays(GetChartByExpensePerDaysRequest request, CancellationToken token)
            => Post<ChartByExpenseDaysResponse[], GetChartByExpensePerDaysRequest>("reports/chart/byExpensePerDays", request, token);
    }
}