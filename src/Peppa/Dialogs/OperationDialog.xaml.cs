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
        
        private async void OnLoaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            //Types.ItemsSource = new[] { OperationViewType.Expense, OperationViewType.Income, OperationViewType.Transfer };
           

            // if (_item.IsNew)
            // {
                //Types.SelectedItem = OperationViewType.Expense;
                //var accountTask = _viewModel.GetAccounts(false);
                //var categoryTask = _viewModel.GetCategories(false, CategoryType.Expense);

                //await Task.WhenAll(accountTask, categoryTask);

                //AccountComboBox.ItemsSource = accountTask.Result;
                //CategoryComboBox.ItemsSource = categoryTask.Result;
            // }
            // else
            // {
                //var accountTask = _viewModel.GetAccounts(true);
                //if (_item.Type == OperationType.Budget)
                //{
                //    BudgetOperationPanel.Visibility = Windows.UI.Xaml.Visibility.Visible;
                //    TransferOperationBudget.Visibility = Windows.UI.Xaml.Visibility.Collapsed;

                //    var categoryTask = _viewModel.GetCategories(true, _item.CategoryType);
                //    var operationTask = _viewModel.GetBudgetOperation(_item.Id);

                //    await Task.WhenAll(accountTask, categoryTask, operationTask);

                //    var operation = operationTask.Result;
                //    Types.SelectedItem = GetViewType(_item.Type, _item.CategoryType);
                //    Types.IsEnabled = false;

                //    AccountComboBox.ItemsSource = accountTask.Result;
                //    AccountComboBox.SelectedItem = accountTask.Result.FirstOrDefault(a => a.Id == operation.AccountId);
                //    CategoryComboBox.ItemsSource = categoryTask.Result;
                //    CategoryComboBox.SelectedItem = categoryTask.Result.FirstOrDefault(c => c.Id == operation.CategoryId);
                //}
                //else
                //{
                //    BudgetOperationPanel.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                //    TransferOperationBudget.Visibility = Windows.UI.Xaml.Visibility.Visible;

                //    var operationTask = _viewModel.GetTransferOperation(_item.Id);

                //    await Task.WhenAll(accountTask, operationTask);

                //    Types.SelectedItem = OperationViewType.Transfer;
                //    Types.IsEnabled = false;

                //    var accounts = accountTask.Result;
                //    var operation = operationTask.Result;

                //    FromComboBox.ItemsSource = accounts;
                //    FromComboBox.SelectedItem = accounts.FirstOrDefault(a => a.Id == operation.FromId);

                //    ToComboBox.ItemsSource = accounts;
                //    ToComboBox.SelectedItem = accounts.FirstOrDefault(a => a.Id == operation.ToId);
                //}               
            // }
        }

        private async void OnTypeItemClick(object sender, ItemClickEventArgs e)
        {
            //var accountTask = _viewModel.GetAccounts(false);

            //if ((OperationViewType)(e.ClickedItem) == OperationViewType.Transfer)
            //{
            //    BudgetOperationPanel.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            //    TransferOperationBudget.Visibility = Windows.UI.Xaml.Visibility.Visible;

            //    await Task.WhenAll(accountTask);

            //    var accounts = accountTask.Result;

            //    FromComboBox.ItemsSource = accounts;
            //    ToComboBox.ItemsSource = accounts;
            //}
            //else
            //{
            //    BudgetOperationPanel.Visibility = Windows.UI.Xaml.Visibility.Visible;
            //    TransferOperationBudget.Visibility = Windows.UI.Xaml.Visibility.Collapsed;

            //    var operationViewType = (OperationViewType)e.ClickedItem; 

            //    var categoryTask = _viewModel.GetCategories(false, operationViewType == OperationViewType.Expense ? CategoryType.Expense : CategoryType.Income);

            //    await Task.WhenAll(accountTask, categoryTask);

            //    var accounts = accountTask.Result;
            //    var categories = categoryTask.Result;

            //    AccountComboBox.ItemsSource = accounts;
            //    CategoryComboBox.ItemsSource = categories;
            //}
        }

        private void OnCancelButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            //_item.Action = ActionType.Cancel;
        }
        
        private void OnSaveButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            // _item.Action = ActionType.Save;
            //
            // if (Types.SelectedItem is OperationViewType selectedType)
            // {
            //     if (selectedType == OperationViewType.Transfer)
            //     {
            //         var from = FromComboBox.SelectedItem as AccountItemViewModel;
            //         var to = ToComboBox.SelectedItem as AccountItemViewModel;
            //         if (from != null && to != null)
            //         {
            //             _item.Type = OperationType.Transfer;
            //             _item.Transfer = new TransferOperationViewModel {FromId = from.Id, ToId = to.Id};
            //         }
            //     }
            //     else
            //     {
            //         var account = AccountComboBox.SelectedItem as AccountItemViewModel;
            //         var category = CategoryComboBox.SelectedItem as CategoryItemViewModel;
            //         if (account != null && category != null)
            //         {
            //             _item.Type = OperationType.Budget;
            //             _item.Budget = new BudgetOperationViewModel {AccountId = account.Id, CategoryId = category.Id};
            //         }
            //     }
            // }
        }
        
        private static OperationViewType GetViewType(OperationType operationType, CategoryType categoryType)
        {
            if (operationType == OperationType.Transfer)
                return OperationViewType.Transfer;

            return categoryType == CategoryType.Expense ? OperationViewType.Expense : OperationViewType.Income;
        }

        private void AmountTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // if (decimal.TryParse(AmountTextBox.Text, out var result))
            // {
            //     _item.EntityAmount = result;
            // }
        }
    }
}
