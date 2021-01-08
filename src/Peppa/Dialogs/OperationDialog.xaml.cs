using Peppa.ViewModels.Operations;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace Peppa.Dialogs
{
    public sealed partial class OperationDialog : ContentDialog
    {
        private readonly OperationsViewModel _viewModel;
        private readonly ListItemViewModel _item;

        public OperationDialog(OperationsViewModel viewModel, ListItemViewModel item)
        {
            this.InitializeComponent();
            _viewModel = viewModel;
            _item = item;
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        private async void OnLoaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var accountTask = _viewModel.GetAccounts();
            var categoryTask = _viewModel.GetCategories(_item.CategoryType);
            var operationTask = _viewModel.GetBudgetOperation(_item.Id);

            await Task.WhenAll(accountTask, categoryTask, operationTask);

            AccountComboBox.ItemsSource = accountTask.Result;
            AccountComboBox.SelectedItem = accountTask.Result.First(a => a.Id == operationTask.Result.AccountId);
            CategoryComboBox.ItemsSource = categoryTask.Result;
            CategoryComboBox.SelectedItem = categoryTask.Result.First(c => c.Id == operationTask.Result.CategoryId);
        }
    }
}
