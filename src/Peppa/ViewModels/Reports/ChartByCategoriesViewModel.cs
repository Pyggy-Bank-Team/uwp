using System.Collections.Generic;

namespace Peppa.ViewModels.Reports
{
    public class ChartByCategoriesViewModel
    {
        public ChartByCategoriesViewModel(string title)
        {
            Title = title;
            Data = new List<TelerikItemReportViewModel>();
        }
        
        public string  Title { get; set; }
        public List<TelerikItemReportViewModel> Data { get; set; }
    }
}