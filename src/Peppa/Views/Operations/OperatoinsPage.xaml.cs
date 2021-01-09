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
            var operationModal = new OperationDialog(_dataContext, (ListItemViewModel)e.ClickedItem);

            await operationModal.ShowAsync();

            //Frame.Navigate(typeof(EditOperationPage), e.ClickedItem);
        }

        private async void OnAddedCostClick(object sender, RoutedEventArgs e)
        {
            //if (!MainViewModel.Current.HaveCategories)
            //{
            //    await DialogService
            //        .GetInformationDialog(Localize.GetTranslateByKey(Localize.WarringCategoriesContent))
            //        .ShowAsync();

            //    return;
            //}

            //Frame.Navigate(typeof(EditOperationPage), new OperationViewModel());
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
