﻿using System;
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
        private OperationsViewModel _dataContext;
        public OperationsPage()
        {
            this.InitializeComponent();
            StupVisible();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            Progress.Visibility = Visibility.Visible;

            _dataContext = (OperationsViewModel) App.ServiceProvider.GetService(typeof(OperationsViewModel));
           await _dataContext.Initialization();

            TableView.ItemsSource = _dataContext.List;
            PaganationView.ItemsSource = _dataContext.Pagination;
            StupCollapsed();
            Progress.Visibility = Visibility.Collapsed;
        }

        private async void OnOperationClick(object sender, ItemClickEventArgs e)
        {
            var operation = (ListItemViewModel) e.ClickedItem;
            
            var operationModal = new OperationDialog(_dataContext, operation);
            await operationModal.ShowAsync();
            
            await _dataContext.DoAction(operation);
            if (operation.Action != Enums.ActionType.Cancel)
                await _dataContext.Initialization();
        }

        private async void OnAddOperationClick(object sender, RoutedEventArgs e)
        {
            var newOperation = new ListItemViewModel();

            var operationModel = new OperationDialog(_dataContext, newOperation);
            await operationModel.ShowAsync();

            await _dataContext.DoAction(newOperation);
            if (newOperation.Action != Enums.ActionType.Cancel)
                await _dataContext.Initialization();
        }

        private void OnRefreshClick(object sender, RoutedEventArgs e)
        {
            //RefreshContainer.RequestRefresh();
        }

        private async void OnRefreshRequested(RefreshContainer sender, RefreshRequestedEventArgs args)
        {
           
        }

        private void StupVisible()
        {
            //RefreshContainer.Visibility = Visibility.Collapsed;
            //StubTextBlock.Visibility = Visibility.Visible;
        }

        private void StupCollapsed()
        {
            //RefreshContainer.Visibility = Visibility.Visible;
            //StubTextBlock.Visibility = Visibility.Collapsed;
        }

        private async void OnPaganationClick(object sender, ItemClickEventArgs e)
        {
            _dataContext.CurrentPage = ((PaginationItemViewModel)e.ClickedItem).Number;
            Progress.Visibility = Visibility.Visible;

            await _dataContext.Initialization();

            TableView.ItemsSource = _dataContext.List;

            Progress.Visibility = Visibility.Collapsed;
        }
    }
}
