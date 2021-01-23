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
            Types.ItemsSource = new[] { OperationViewType.Expense, OperationViewType.Income, OperationViewType.Transfer };
           

            if (_item.IsNew)
            {
                Types.SelectedItem = OperationViewType.Expense;
                var accountTask = _viewModel.GetAccounts(false);
                var categoryTask = _viewModel.GetCategories(false, CategoryType.Expense);

                await Task.WhenAll(accountTask, categoryTask);

                AccountComboBox.ItemsSource = accountTask.Result;
                CategoryComboBox.ItemsSource = categoryTask.Result;
            }
            else
            {
                var accountTask = _viewModel.GetAccounts(true);
                if (_item.Type == OperationType.Budget)
                {
                    BudgetOperationPanel.Visibility = Windows.UI.Xaml.Visibility.Visible;
                    TransferOperationBudget.Visibility = Windows.UI.Xaml.Visibility.Collapsed;

                    var categoryTask = _viewModel.GetCategories(true, _item.CategoryType);
                    var operationTask = _viewModel.GetBudgetOperation(_item.Id);

                    await Task.WhenAll(accountTask, categoryTask, operationTask);

                    var operation = operationTask.Result;
                    Types.SelectedItem = operation.ViewType;
                    Types.IsEnabled = false;

                    AccountComboBox.ItemsSource = accountTask.Result;
                    AccountComboBox.SelectedItem = accountTask.Result.FirstOrDefault(a => a.Id == operation.AccountId);
                    CategoryComboBox.ItemsSource = categoryTask.Result;
                    CategoryComboBox.SelectedItem = categoryTask.Result.FirstOrDefault(c => c.Id == operation.CategoryId);
                }
                else
                {
                    BudgetOperationPanel.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                    TransferOperationBudget.Visibility = Windows.UI.Xaml.Visibility.Visible;

                    var operationTask = _viewModel.GetTransferOperation(_item.Id);

                    await Task.WhenAll(accountTask, operationTask);

                    Types.SelectedItem = OperationViewType.Transfer;
                    Types.IsEnabled = false;

                    var accounts = accountTask.Result;
                    var operation = operationTask.Result;

                    FromComboBox.ItemsSource = accounts;
                    FromComboBox.SelectedItem = accounts.FirstOrDefault(a => a.Id == operation.AccountId);

                    ToComboBox.ItemsSource = accounts;
                    ToComboBox.SelectedItem = accounts.FirstOrDefault(a => a.Id == operation.ToAccountId);
                }               
            }
        }

        private void OnTypeItemClick(object sender, ItemClickEventArgs e)
        {
            if ((OperationViewType)(e.ClickedItem) == OperationViewType.Transfer)
            {
                BudgetOperationPanel.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                TransferOperationBudget.Visibility = Windows.UI.Xaml.Visibility.Visible;
            }
            else
            {
                BudgetOperationPanel.Visibility = Windows.UI.Xaml.Visibility.Visible;
                TransferOperationBudget.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }
        }
    }
}
