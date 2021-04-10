using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Peppa.ViewModels.Operations;
using Peppa.Dialogs;
using Peppa.ViewModels.Pagination;

namespace Peppa.Views.Operations
{
    public sealed partial class OperationsPage : Page
    {
        private OperationsViewModel _operationsViewModel;
        public OperationsPage()
        {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            Progress.Visibility = Visibility.Visible;

            _operationsViewModel = (OperationsViewModel) App.ServiceProvider.GetService(typeof(OperationsViewModel));
           await _operationsViewModel.Initialization();

            TableView.ItemsSource = _operationsViewModel.List;
            PaganationView.ItemsSource = _operationsViewModel.Pagination;
            Progress.Visibility = Visibility.Collapsed;
        }

        private async void OnOperationClick(object sender, ItemClickEventArgs e)
        {
            var operation = (ListItemViewModel) e.ClickedItem;
            
            var operationModal = new OperationDialog(_operationsViewModel, operation);
            await operationModal.ShowAsync();
            
            await _operationsViewModel.DoAction(operation);
            if (operation.Action != Enums.ActionType.Cancel)
                await _operationsViewModel.Initialization();
        }

        private async void OnAddOperationClick(object sender, RoutedEventArgs e)
        {
            var newOperation = new ListItemViewModel();

            var operationModel = new OperationDialog(_operationsViewModel, newOperation);
            await operationModel.ShowAsync();

            await _operationsViewModel.DoAction(newOperation);
            if (newOperation.Action != Enums.ActionType.Cancel)
                await _operationsViewModel.Initialization();
        }

        private async void OnPaganationClick(object sender, ItemClickEventArgs e)
        {
            _operationsViewModel.CurrentPage = ((PaginationItemViewModel)e.ClickedItem).Number;
            Progress.Visibility = Visibility.Visible;

            await _operationsViewModel.Initialization();

            TableView.ItemsSource = _operationsViewModel.List;

            Progress.Visibility = Visibility.Collapsed;
        }
    }
}
