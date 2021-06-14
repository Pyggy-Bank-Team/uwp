using Peppa.ViewModels.Reports;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Peppa.Views.Reports
{
    public sealed partial class ReportView : Page
    {
        private IReportsViewModel _viewModel;
        public ReportView()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

        }
    }
}
