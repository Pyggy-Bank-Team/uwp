using Windows.UI.Xaml.Controls;
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
        {
        }

        private void OnCancelButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}
