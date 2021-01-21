using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Peppa.Services;
using Peppa.Services.Windows;
using Peppa.ViewModels.Donate;

namespace Peppa.Views.Donate
{
    public sealed partial class DonatePage : Page
    {
        private DonateViewModel _donate;

        public DonatePage()
        {
            this.InitializeComponent();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            _donate = e.Parameter as DonateViewModel;
            InitProgressBar.Visibility = Windows.UI.Xaml.Visibility.Visible;
            await _donate.Initialization();
            InitProgressBar.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            ListViewDonate.ItemsSource = _donate.Items;
        }

        private async void OnItemClick(object sender, ItemClickEventArgs e)
        {
            DonateItemViewModel selectedItem = e.ClickedItem as DonateItemViewModel;

            if (selectedItem == null)
                return;

            string status = await _donate.BuyItem(selectedItem);

            if(!String.IsNullOrEmpty(status))
                await DialogService.GetPurchaseStatusDialog(status).ShowAsync();
        }
    }
}
