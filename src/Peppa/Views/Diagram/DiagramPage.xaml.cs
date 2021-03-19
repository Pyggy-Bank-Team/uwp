using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Peppa.Utilities;
using Peppa.ViewModels.Diagram;
using Telerik.UI.Xaml.Controls.Chart;

namespace Peppa.Views.Diagram
{
    public sealed partial class DiagramPage : Page
    {
        private DiagramViewModel _diagram;

        public DiagramPage()
        {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            _diagram = (DiagramViewModel) App.ServiceProvider.GetService(typeof(DiagramViewModel));

            await  _diagram.Initialization();

            Diagram.Series[0].ItemsSource = _diagram.Datas;

            ChartPalette palette = new ChartPalette { Name = "CustomsDark" };

            foreach (var color in _diagram.Datas)
            {
                palette.FillEntries.Brushes.Add(new SolidColorBrush(ColorUtility.GetColorFromHexString(color.Color)));
            }

            Diagram.Palette = palette;
            LabelsListView.ItemsSource = _diagram.Datas;

            if (!_diagram.IsEmpty)
                await _diagram.UpdateTile();
        }

        private void OnFilterClick(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            FilterFlyout.ShowAt((AppBarButton)sender);
        }

        private void OnCheckMarkClick(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            FilterFlyout.Hide();

            _diagram.ApplyFilter(StartDatePicker.Date.Date, EndDatePicker.Date.Date);

            Diagram.Series[0].ItemsSource = null;
            Diagram.Series[0].ItemsSource = _diagram.Datas;

            ChartPalette palette = new ChartPalette { Name = "CustomsDark" };

            foreach (var color in _diagram.Datas)
            {
                palette.FillEntries.Brushes.Add(new SolidColorBrush(ColorUtility.GetColorFromHexString(color.Color)));
            }

            Diagram.Palette = palette;
            LabelsListView.ItemsSource = null;
            LabelsListView.ItemsSource = _diagram.Datas;
        }

        private void OnCancelClick(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            FilterFlyout.Hide();
        }
    }
}
