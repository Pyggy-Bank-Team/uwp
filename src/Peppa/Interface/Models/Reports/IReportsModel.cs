using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Peppa.Dto;
using Peppa.Enums;
using Peppa.Models.Reports;

namespace Peppa.Interface.Models
{
    public interface IReportsModel
    {
        Task<ReportResult> UpdateReports(CancellationToken token);
        Task<IEnumerable<ItemReport>> GetChartByCategories(CategoryType type, DateTime from, DateTime to, CancellationToken token);
        DateTime From { get; set; }
        DateTime To { get; set; }
        ReportModel ExpenseReport { get; }
        ReportModel IncomeReport { get; }
    }
}