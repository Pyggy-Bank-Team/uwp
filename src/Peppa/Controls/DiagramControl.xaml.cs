using Telerik.UI.Xaml.Controls.Chart;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Peppa.Utilities;
using Peppa.ViewModels.Reports;


namespace Peppa.Controls
{
    public sealed partial class DiagramControl : UserControl
    {
        private ReportsViewModel _reports;

        public DiagramControl()
        {
            this.InitializeComponent();
        }

        private void OnDataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            _reports = args.NewValue as ReportsViewModel;

            // Diagram.Series[0].ItemsSource = _report.Datas;
            //
            // ChartPalette palette = new ChartPalette { Name = "CustomsDark" };
            //
            // foreach (var color in _report.Datas)
            // {
            //     palette.FillEntries.Brushes.Add(new SolidColorBrush(ColorUtility.GetColorFromHexString(color.Color)));
            // }
            //
            // Diagram.Palette = palette;
        }
    }
}
