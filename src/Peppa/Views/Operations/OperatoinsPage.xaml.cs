using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Microsoft.Extensions.DependencyInjection;
using Peppa.ViewModels.Operations;
using Peppa.Dialogs;
using Peppa.Interface.ViewModels;
using Peppa.ViewModels.Pagination;

namespace Peppa.Views.Operations
{
    public sealed partial class OperationsPage : Page
    {
        private IOperationsViewModel _operationsViewModel;
        public OperationsPage()
        {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            _operationsViewModel = App.ServiceProvider.GetService<IOperationsViewModel>();
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
