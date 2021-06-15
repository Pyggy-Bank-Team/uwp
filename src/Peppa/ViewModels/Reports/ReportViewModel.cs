using System.Collections.Generic;
using System.Linq;
using Peppa.Interface.Models.Reports;
using Peppa.Utilities;
using Telerik.UI.Xaml.Controls.Chart;
using Windows.UI.Xaml.Media;

namespace Peppa.ViewModels.Reports
{
    public class ReportViewModel
    {
        private readonly IReportModel _model;

        public ReportViewModel(IReportModel model, string title)
        {
            _model = model;
            Title = title;
            List = _model.List.Select(item => new TelerikItemReportViewModel
            {
                Color = item.CategoryHexColor,
                Title = item.CategoryTitle,
                Value = item.Amount
            }).ToList();

            Palette = new ChartPalette { Name = "CustomsDark" };

            foreach (var color in _model.List)
                Palette.FillEntries.Brushes.Add(new SolidColorBrush(ColorUtility.GetColorFromHexString(color.CategoryHexColor)));
        }

        public string Title { get; }
        public double TotalAmount => _model.TotalAmount;
        public List<TelerikItemReportViewModel> List { get; }
        public ChartPalette Palette { get;  }
    }
}