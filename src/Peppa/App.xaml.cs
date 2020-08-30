﻿using System;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.ApplicationModel.Core;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.ViewManagement;
using piggy_bank_uwp.Workers;
using Windows.Storage;
using Microsoft.HockeyApp;
using Microsoft.Extensions.DependencyInjection;
using piggy_bank_uwp.Extensions;
using piggy_bank_uwp.Views;

namespace piggy_bank_uwp
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;

            App.Current.RequestedTheme = SettingsWorker.Current.GetRequestedTheme();
            var serviceCollection = new ServiceCollection();
            serviceCollection.DependencyInjection();

            ServiceProvider = serviceCollection.BuildServiceProvider();
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    try
                    {
                        if (ApplicationData.Current.LocalSettings.Values.ContainsKey("navigationState"))
                        {
                            rootFrame.SetNavigationState((string)ApplicationData.Current.LocalSettings.Values["navigationState"]);
                        }
                    }
                    catch { }
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    // When the navigation stack isn't restored navigate to the first page,
                    // configuring the new page by passing required information as a navigation
                    // parameter
                    rootFrame.Navigate(typeof(MainPage), e.Arguments);
                }
                // Ensure the current window is active
                Window.Current.Activate();

                ExtendAcrylicIntoTitleBar();
            }

#if RELEASE
            InitHockeyApp();
#endif
        }

        public static Task RunUIAsync(Action agileCallback)
        {
            return CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Low, () =>
            {
                agileCallback();
            }).AsTask();
        }

        protected override void OnActivated(IActivatedEventArgs args)
        {
            if(args.Kind == ActivationKind.ToastNotification)
            {
                Frame rootFrame = Window.Current.Content as Frame;
                if (rootFrame == null)
                {
                    rootFrame = new Frame();
                    Window.Current.Content = rootFrame;
                }
                rootFrame.Navigate(typeof(MainPage));
                Window.Current.Activate();
            }
        }

        private void ExtendAcrylicIntoTitleBar()
        {
            CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = true;
            ApplicationViewTitleBar titleBar = ApplicationView.GetForCurrentView().TitleBar;
            titleBar.ButtonBackgroundColor = Colors.Transparent;
            titleBar.ButtonInactiveBackgroundColor = Colors.Transparent;
        }

        private string GetDefaultNavigationState()
        {
            Frame frame = new Frame();
            frame.Navigate(typeof(MainPage));
            return frame.GetNavigationState();
        }

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();

            string navigationState = default(string);

            try
            {
                Frame rootFrame = Window.Current.Content as Frame;
                navigationState = rootFrame.GetNavigationState();
            }
            catch
            {
                navigationState = GetDefaultNavigationState();
            }
            finally
            {
                ApplicationData.Current.LocalSettings.Values["navigationState"] = navigationState;
            }

            deferral.Complete();
        }

        private void InitHockeyApp()
        {
            try
            {
                HockeyClient.Current.Configure("e4d002033ba2405683fe9b3e4b202604",
                new TelemetryConfiguration
                {
                    EnableDiagnostics = true,
                    Collectors = WindowsCollectors.Metadata |
                                 WindowsCollectors.Session |
                                 WindowsCollectors.UnhandledException
                });
            }
            catch { }
        }

        public static ServiceProvider  ServiceProvider { get; private set; }
    }
}
