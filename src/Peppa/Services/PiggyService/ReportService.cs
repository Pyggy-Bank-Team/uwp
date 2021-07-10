using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Peppa.Contracts.Requests;
using Peppa.Contracts.Responses;
using Peppa.Dto;
using Peppa.Interface.InternalServices;
using Peppa.Interface.Services;

namespace Peppa.Services.PiggyService
{
    public class ReportService : PiggyServiceBase, IReportService
    {
        public ReportService(IHttpClientFactory httpClientFactory,ISettingsService settingsService) 
            : base(httpClientFactory, settingsService)
        {
        }

        public async Task<ItemReport[]> GetChartByCategories(GetReportRequest request, CancellationToken token)
        {
            var result = await Post<ItemReport[], GetReportRequest>("reports/chart/byCategories", request, token);
            return result.IsSuccess ? result.Ok : null;
        }

        public async Task<ChartByExpenseDaysResponse[]> GetChartByExpenseDays(GetChartByExpensePerDaysRequest request, CancellationToken token)
        {
            var result = await  Post<ChartByExpenseDaysResponse[], GetChartByExpensePerDaysRequest>("reports/chart/byExpensePerDays", request, token);
            return result.IsSuccess ? result.Ok : null;
        }
    }
}