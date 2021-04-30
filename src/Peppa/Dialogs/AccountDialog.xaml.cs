using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Peppa.Enums;
using Peppa.ViewModels.Accounts;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Peppa.Dialogs
{
    public sealed partial class AccountDialog : ContentDialog
    {
        private readonly AccountDialogViewModel _viewModel;

        public AccountDialog(AccountDialogViewModel viewModel)
        {
            _viewModel = viewModel;
            this.InitializeComponent();
        }

        private void OnSaveButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
            => _viewModel.Result = DialogResult.Save;

        private void OnCancelButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
            => _viewModel.Result = DialogResult.Cancel;

        private void OnDeleteButtonClick(object sender, RoutedEventArgs e)
        {
            _viewModel.Result = DialogResult.Delete;
            Hide();
        }

        public DialogResult Result => _viewModel.Result;
    }
}
