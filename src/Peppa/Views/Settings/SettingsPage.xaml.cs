using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Peppa.Interface.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace Peppa.Views.Settings
{
    public sealed partial class SettingsPage : Page
    {
        private ISettingsViewModel _viewModel;
        public SettingsPage()
        {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            _viewModel = App.ServiceProvider.GetService<ISettingsViewModel>();
            await _viewModel.Initialization();
        }
    }
}
