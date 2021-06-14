using System;
using System.Linq;
using System.Threading.Tasks;
using Peppa.Enums;
using Peppa.Interface.Models;
using Peppa.Interface.ViewModels;

namespace Peppa.ViewModels.Reports
{
    public class ReportsViewModel : BaseViewModel, IInitialization
    {
        private readonly IReportsModel _model;

        public ReportsViewModel(IReportsModel model)
        {
            _model = model;
            ExpenseChart = new ChartByCategoriesViewModel("Expense");
            IncomeChart = new ChartByCategoriesViewModel("Income");
        }

        public async Task Initialization()
        {
            var items = await _model.GetChartByCategories(CategoryType.Expense, DateTime.Now.AddYears(-1), DateTime.Now, GetCancellationToken());
            if (items != null)
            {
                ExpenseChart.Data = items.Select(d => new DataDiagramViewModel
                {
                    Value = d.Amount,
                    Color = d.CategoryHexColor,
                    Title = d.CategoryTitle
                }).ToList();
            }
            
            items = await _model.GetChartByCategories(CategoryType.Income, DateTime.Now.AddYears(-1), DateTime.Now, GetCancellationToken());
            if (items != null)
            {
                IncomeChart.Data = items.Select(d => new DataDiagramViewModel
                {
                    Value = d.Amount,
                    Color = d.CategoryHexColor,
                    Title = d.CategoryTitle
                }).ToList();
            }
        }

        public void Finalization()
        {

        }

        public async Task ApplyFilter(DateTime startDate, DateTime endDate)
        {
            var items = await _model.GetChartByCategories(CategoryType.Expense, startDate, endDate, GetCancellationToken());
            if (items != null)
            {
                ExpenseChart.Data = items.Select(d => new DataDiagramViewModel
                {
                    Value = d.Amount,
                    Color = d.CategoryHexColor,
                    Title = d.CategoryTitle
                }).ToList();
            }

            items = await _model.GetChartByCategories(CategoryType.Income, startDate, endDate, GetCancellationToken());
            if (items != null)
            {
                IncomeChart.Data = items.Select(d => new DataDiagramViewModel
                {
                    Value = d.Amount,
                    Color = d.CategoryHexColor,
                    Title = d.CategoryTitle
                }).ToList();
            }
        }
        
        public ChartByCategoriesViewModel ExpenseChart { get; }
        public ChartByCategoriesViewModel IncomeChart { get; }
    }
}
