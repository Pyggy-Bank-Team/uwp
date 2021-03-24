using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Peppa.Utilities;
using Peppa.ViewModels.Report;
using Telerik.UI.Xaml.Controls.Chart;

namespace Peppa.Views.Diagram
{
    public sealed partial class DiagramPage : Page
    {
        private ReportViewModel _report;

        public DiagramPage()
        {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            _report = (ReportViewModel) App.ServiceProvider.GetService(typeof(ReportViewModel));

            await  _report.Initialization();
            UpdateChartByCategories(Diagram, LabelsListView, _report.ExpenseChart.Data);
            UpdateChartByCategories(Income, IncomeLabels, _report.IncomeChart.Data);
        }

        private void UpdateChartByCategories(RadPieChart chart, ListView labels, List<DataDiagramViewModel> data)
        {
            chart.Series[0].ItemsSource = _report.ExpenseChart.Data;

            ChartPalette palette = new ChartPalette { Name = "CustomsDark" };

            foreach (var color in data)
            {
                palette.FillEntries.Brushes.Add(new SolidColorBrush(ColorUtility.GetColorFromHexString(color.Color)));
            }

            chart.Palette = palette;
            labels.ItemsSource = data;
        }

        private async void ApplyFilter(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            await _report.ApplyFilter(StartDatePicker.Date.Value.UtcDateTime, EndDatePicker.Date.Value.UtcDateTime);
            UpdateChartByCategories(Diagram, LabelsListView, _report.ExpenseChart.Data);
            UpdateChartByCategories(Income, IncomeLabels, _report.IncomeChart.Data);
        }
    }
}
