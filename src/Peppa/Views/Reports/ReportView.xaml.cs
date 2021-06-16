using Peppa.ViewModels.Reports;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Microsoft.Extensions.DependencyInjection;

namespace Peppa.Views.Reports
{
    public sealed partial class ReportView : Page
    {
        private IReportsViewModel _viewModel;
        public ReportView()
        {
            this.InitializeComponent();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            _viewModel = App.ServiceProvider.GetService<IReportsViewModel>();
            await _viewModel.Initialization();
        }
    }
}
