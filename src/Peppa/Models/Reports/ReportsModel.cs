using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Peppa.Contracts.Requests;
using Peppa.Dto;
using Peppa.Enums;
using Peppa.Interface.Models;
using Peppa.Interface.Services;

namespace Peppa.Models.Reports
{
    public class ReportsModel : IReportsModel
    {
        private readonly IReportService _service;

        public ReportsModel(IReportService service)
        {
            _service = service;
        }

        public async Task<ReportResult> UpdateReports(CancellationToken token)
        {
            if (!_service.IsAuthorized)
                return ReportResult.Error;

            //If dates of report is not init, then we get report for week
            var from = From;
            if (from == default)
                from = DateTime.UtcNow.AddDays(-7);

            var to = To;
            if (to == default)
                to = DateTime.UtcNow;

            if (to < from)
                return ReportResult.ToLessFrom;

            var getExpenseReportRequest = new GetReportRequest
            {
                From = from,
                To = to,
                Type = CategoryType.Expense
            };

            var expenseReport = await _service.GetChartByCategories(getExpenseReportRequest, token);

            var getIncomeReportRequest = new GetReportRequest
            {
                From = from,
                To = to,
                Type = CategoryType.Income
            };

            var incomeReport = await _service.GetChartByCategories(getIncomeReportRequest, token);

            ExpenseReport = new ReportModel(expenseReport);
            IncomeReport = new ReportModel(incomeReport);

            return ReportResult.Ok;
        }
        
        public async Task<IEnumerable<ItemReport>> GetChartByCategories(CategoryType type, DateTime from, DateTime to, CancellationToken token)
        {
            if (_service.IsAuthorized)
            {
                var request = new GetReportRequest
                {
                    Type = type,
                    From = from,
                    To = to
                };

                var response = await _service.GetChartByCategories(request, token);
                return response.Select(c => new ItemReport
                {
                    Amount = c.Amount,
                    CategoryId = c.CategoryId,
                    CategoryTitle = c.CategoryTitle,
                    CategoryHexColor = c.CategoryHexColor
                });
            }

            return null;
        }
        
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public ReportModel ExpenseReport { get; private set; }
        public ReportModel IncomeReport { get; private set; }
    }
}