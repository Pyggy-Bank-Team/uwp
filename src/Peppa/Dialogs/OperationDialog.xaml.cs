using Windows.UI.Xaml;
using Peppa.Enums;
using Peppa.ViewModels.Operations;
using Windows.UI.Xaml.Controls;

namespace Peppa.Dialogs
{
    public sealed partial class OperationDialog : ContentDialog
    {
        private readonly OperationDialogViewModel _viewModel;

        public OperationDialog(OperationDialogViewModel viewModel)
        {
            this.InitializeComponent();
            _viewModel = viewModel;
        }

        private void OnCancelButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
            => _viewModel.Result = DialogResult.Cancel;

        private void OnDeleteButtonClick(object sender, RoutedEventArgs e)
            => _viewModel.Result = DialogResult.Delete;

        private void OnSaveButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
            => _viewModel.Result = DialogResult.Save;

        public DialogResult Result => _viewModel.Result;
    }
}
