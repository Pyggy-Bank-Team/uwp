using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Peppa.ViewModels.Reports
{
    public interface IReportsViewModel : INotifyPropertyChanged
    {
        ReportViewModel ExpenseReport { get; }
        DateTimeOffset? From { get; set; }
        ReportViewModel IncomeReport { get; }
        DateTimeOffset? To { get; set; }
        bool IsProgressShow { get; set; }

        Task Initialization();
    }
}