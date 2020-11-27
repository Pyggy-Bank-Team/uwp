using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Peppa.ViewModels.Accounts;
using Peppa.ViewModels.Operations;
using piggy_bank_uwp.Enums;

namespace Peppa.Views.Operations
{
    public sealed partial class EditOperationPage : Page
    {
        private OperationViewModel _dataContext;
        private bool _isInit;

        public EditOperationPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _dataContext = e.Parameter as OperationViewModel;
            DataContext = _dataContext;

            AccountComboBox.ItemsSource = _dataContext.GetAccounts().Result;
            CategoryComboBox.ItemsSource = _dataContext.GetCategories().Result;

            Types.ItemsSource = new[] { OperationType.Budget, OperationType.Plan, OperationType.Transfer };

            //_cost = e.Parameter as CostViewModel;
            //DatePicker.Date = _cost.DateOffset;
            ////CategoriesComboBox.ItemsSource = MainViewModel.Current.Categories;
            //BalancesComboBox.ItemsSource = MainViewModel.Current.Accounts.List;

            //if (!_cost.IsNew)
            //{
            //    //CategoriesComboBox.SelectedItem = MainViewModel.Current.Categories.FirstOrDefault(t => t.Id == _cost.CategoryId);
            //   // BalancesComboBox.SelectedItem = MainViewModel.Current.Accounts.List.FirstOrDefault(b=>b.Id == _cost.BalanceId);
            //    //CostTextBox.Text = _cost.Cost.ToString();
            //}

            //_isInit = true;
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);

            _isInit = false;
        }

        private async void OnSaveClick(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            //if(CategoriesComboBox.SelectedItem == null)
            //{
            //    await DialogService
            //        .GetInformationDialog(Localize.GetTranslateByKey(Localize.WarringCostContent))
            //        .ShowAsync();

            //    return;
            //}

            //if(BalancesComboBox.SelectedItem == null)
            //{
            //    await DialogService
            //        .GetInformationDialog(Localize.GetTranslateByKey(Localize.WarringBalanceCostContent))
            //        .ShowAsync();
            //    return;
            //}

            //if (_cost.IsNew)
            //{
            //    _cost.IsNew = false;
            //    await MainViewModel.Current.AddCost(_cost);
            //}
            //else
            //{
            //    await MainViewModel.Current.UpdateCost(_cost);
            //}

            //if (Frame.CanGoBack)
            //{
            //    Frame.GoBack();
            //}
        }

        private void OnCloseClick(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            //Need ask about save cost
            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }

        private void OnTagSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!_isInit)
                return;

            //var selectedCategory = e.AddedItems[0] as CategoryViewModel;

            //_cost.ChangedCategory(selectedCategory?.Id);
        }

        private async void OnDeleteClick(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }

        private void OnDateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {

        }

        private void OnBalanceSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!_isInit)
                return;

            var selectedBalance = e.AddedItems[0] as AccountViewModel;

            //_cost.ChangedBalance(selectedBalance?.Id);
        }

        private void OnAmountChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            //var regex = new Regex(@"^\d$");
            //if (regex.IsMatch(AmountTextBox.Text))
            //    _dataContext.Amount = decimal.Parse(AmountTextBox.Text);
            //else
            //    AmountTextBox.Text = _dataContext.Amount.ToString();
        }
    }
}
