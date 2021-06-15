using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Peppa.Utilities;
using Peppa.ViewModels.Reports;
using Telerik.UI.Xaml.Controls.Chart;

namespace Peppa.Views.Diagram
{
    public sealed partial class DiagramPage : Page
    {
        private ReportsViewModel _reports;

        public DiagramPage()
        {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            _reports = (ReportsViewModel) App.ServiceProvider.GetService(typeof(ReportsViewModel));

            await  _reports.Initialization();
            //UpdateChartByCategories(Diagram, LabelsListView, _reports.ExpenseChart.Data);
            //UpdateChartByCategories(Income, IncomeLabels, _reports.IncomeChart.Data);
        }

        private void UpdateChartByCategories(RadPieChart chart, ListView labels, List<TelerikItemReportViewModel> data)
        {
            //chart.Series[0].ItemsSource = _reports.ExpenseChart.Data;

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
            //await _reports.ApplyFilter(StartDatePicker.Date.Value.UtcDateTime, EndDatePicker.Date.Value.UtcDateTime);
            //UpdateChartByCategories(Diagram, LabelsListView, _reports.ExpenseChart.Data);
            //UpdateChartByCategories(Income, IncomeLabels, _reports.IncomeChart.Data);
        }
    }
}
