using Peppa.Enums;
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
            Types.ItemsSource = new[] { OperationViewType.Income, OperationViewType.Expense, OperationViewType.Transfer };

            var accountTask = _viewModel.GetAccounts();
            var categoryTask = _viewModel.GetCategories(_item.CategoryType);
            var operationTask = _viewModel.GetBudgetOperation(_item.Id);

            await Task.WhenAll(accountTask, categoryTask, operationTask);


            var operation = operationTask.Result;
            Types.SelectedItem = operation.ViewType;

            AccountComboBox.ItemsSource = accountTask.Result;
            AccountComboBox.SelectedItem = accountTask.Result.First(a => a.Id == operation.AccountId);
            CategoryComboBox.ItemsSource = categoryTask.Result;
            CategoryComboBox.SelectedItem = categoryTask.Result.First(c => c.Id == operation.CategoryId);
        }
    }
}
