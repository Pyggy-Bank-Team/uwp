using System;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Peppa.ViewModels;
using Peppa.Views.Accounts;
using Peppa.Views.Donate;
using Peppa.Views.Users;
using Peppa.Views.Categories;

namespace Peppa.Views
{
    public sealed partial class MainPage : Page
    {
        private MainViewModel _mainViewModel;

        public MainPage()
        {
            this.InitializeComponent();

            _mainViewModel = MainViewModel.Current;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _mainViewModel.Init();
            DataContext = _mainViewModel;
        }

        private void OnBackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
            if (ContentFrame.CanGoBack)
            {
                ContentFrame.GoBack();
            }
        }

        private void OnItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked)
            {
                ContentFrame.Navigate(typeof(SettingsPage.SettingsPage));
                NavView.Header = Localize.GetTranslateByKey(Localize.Settings);
            }
            else
            {
                var item = sender.MenuItems.OfType<NavigationViewItem>().First(x => (string)x.Content == (string)args.InvokedItem);
                NavViewNavigate(item as NavigationViewItem);
            }
        }

        private void NavViewNavigate(NavigationViewItem navigationViewItem)
        {
            switch (navigationViewItem.Tag)
            {
                case Constants.accounts:
                    ContentFrame.Navigate(typeof(BalancesPage));
                    NavView.Header = Localize.GetTranslateByKey(Localize.Accounts);
                    break;
                case Constants.costs:
                    //ContentFrame.Navigate(typeof(CostsPage));
                    //NavView.Header = Localize.GetTranslateByKey(Localize.Costs);
                    break;
                case Constants.categories:
                    ContentFrame.Navigate(typeof(CategoriesPage));
                    NavView.Header = Localize.GetTranslateByKey(Localize.Categories);
                    break;
                case Constants.diagrams:
                    //ContentFrame.Navigate(typeof(DiagramPage), _mainViewModel.Diagram);
                    //NavView.Header = Localize.GetTranslateByKey(Localize.Diagrama);
                    break;
                case Constants.synchronization:
                    ContentFrame.Navigate(typeof(SyncPage));
                    NavView.Header = Localize.GetTranslateByKey(Localize.Synchronization);
                    break;
                case Constants.donate:
                    ContentFrame.Navigate(typeof(DonatePage), _mainViewModel.Donate);
                    NavView.Header = Localize.Donate;
                    break;
            }
        }

        private void OnNavViewLoaded(object sender, RoutedEventArgs e)
        {
            var item = NavView.MenuItems.OfType<NavigationViewItem>().First(n => (string)n.Tag == Constants.synchronization);
            NavViewNavigate(item);
        }

        private async void OnFeedbackTapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            if (Microsoft.Services.Store.Engagement.StoreServicesFeedbackLauncher.IsSupported())
            {
                var launcher = Microsoft.Services.Store.Engagement.StoreServicesFeedbackLauncher.GetDefault();
                await launcher.LaunchAsync();
            }
        }
    }
}
