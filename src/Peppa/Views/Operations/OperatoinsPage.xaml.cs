using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Microsoft.Extensions.DependencyInjection;
using Peppa.Interface.ViewModels;

namespace Peppa.Views.Operations
{
    public sealed partial class OperationsPage : Page
    {
        private IOperationsViewModel _operationsViewModel;
        public OperationsPage()
        {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            _operationsViewModel = App.ServiceProvider.GetService<IOperationsViewModel>();
            await _operationsViewModel.Initialization();
        }
    }
}
