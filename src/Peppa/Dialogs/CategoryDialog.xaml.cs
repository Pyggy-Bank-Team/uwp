using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Peppa.Enums;
using Peppa.ViewModels.Categories;

namespace Peppa.Dialogs
{
    public sealed partial class CategoryDialog : ContentDialog
    {
        private readonly CategoryDialogViewModel _viewModel;

        public CategoryDialog(CategoryDialogViewModel viewModel)
        {
            _viewModel = viewModel;
            InitializeComponent();
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

