﻿using System;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Peppa.Views.Accounts;
using Peppa.Views.Categories;
using Peppa.Views.Operations;
using Peppa.Views.Reports;

namespace Peppa.Views
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
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
                ContentFrame.Navigate(typeof(Settings.SettingsPage));
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
                    break;
                case Constants.operations:
                    ContentFrame.Navigate(typeof(OperationsPage));
                    break;
                case Constants.categories:
                    ContentFrame.Navigate(typeof(CategoriesPage));
                    break;
                case Constants.diagrams:
                    ContentFrame.Navigate(typeof(ReportView));
                    break;
            }
        }

        private void OnNavViewLoaded(object sender, RoutedEventArgs e)
        {
            var item = NavView.MenuItems.OfType<NavigationViewItem>().First(n => (string)n.Tag == Constants.operations);
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
