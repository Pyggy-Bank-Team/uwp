using System;
using System.Threading.Tasks;

namespace Peppa.ViewModels.Reports
{
    public interface IReportsViewModel
    {
        ReportViewModel ExpenseReport { get; }
        DateTime From { get; set; }
        ReportViewModel IncomeReport { get; }
        DateTime To { get; set; }

        Task Initialization();
    }
}