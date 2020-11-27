using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Peppa.Services;
using Peppa.ViewModels;
using Peppa.ViewModels.Operations;

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

            StupCollapsed();
            Progress.Visibility = Visibility.Collapsed;
        }

        private void OnCostItemClick(object sender, ItemClickEventArgs e)
        {
            Frame.Navigate(typeof(EditOperationPage), e.ClickedItem);
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
            RefreshContainer.RequestRefresh();
        }

        private async void OnRefreshRequested(RefreshContainer sender, RefreshRequestedEventArgs args)
        {
           
        }

        private void StupVisible()
        {
            RefreshContainer.Visibility = Visibility.Collapsed;
            StubTextBlock.Visibility = Visibility.Visible;
        }

        private void StupCollapsed()
        {
            RefreshContainer.Visibility = Visibility.Visible;
            StubTextBlock.Visibility = Visibility.Collapsed;
        }
    }
}
