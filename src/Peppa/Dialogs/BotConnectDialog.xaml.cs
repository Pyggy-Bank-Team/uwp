using Peppa.ViewModels.Settings;
using Windows.UI.Xaml.Controls;


namespace Peppa.Dialogs
{
    public sealed partial class BotConnectDialog : ContentDialog
    {
        private readonly BotDialogViewModel _viewModel;

        public BotConnectDialog(BotDialogViewModel viewModel)
        {
            _viewModel = viewModel;

            this.InitializeComponent();
        }

        private void OnCloseButtonClick(object sender, Windows.UI.Xaml.RoutedEventArgs e)
            => Hide();

        private async void OnDialogLoaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            await _viewModel.Initialization();
        }
    }
}
