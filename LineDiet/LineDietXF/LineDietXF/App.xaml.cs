using LineDietXF.Interfaces;
using LineDietXF.MockServices;
using LineDietXF.Services;
using LineDietXF.Views;
using Microsoft.Practices.Unity;
using Prism.Unity;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LineDietXF
{
    public partial class App : PrismApplication
    {
        string DBPath { get; set; }

        public App(IPlatformInitializer platformInitializer, string dbPath) : base(platformInitializer)
        {
            DBPath = dbPath;
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            Container.Resolve<IAnalyticsService>().Initialize(Constants.Analytics.GA_TrackingID, Constants.Analytics.GA_AppName, Constants.Analytics.GA_DispatchPeriod);
            Container.Resolve<ISettingsService>().Initialize();

            try
            {
                await NavigationService.NavigateAsync($"{nameof(NavigationPage)}/{nameof(MainPage)}");
            }
            catch (Exception ex)
            {
                if (Debugger.IsAttached)
                    Debugger.Break();

                var analyticsService = Container.Resolve<IAnalyticsService>();
                if (analyticsService != null)
                    analyticsService.TrackFatalError($"{nameof(OnInitialized)} - an exception occurred trying to navigate to the MainPage.", ex);
                throw ex;
            }
        }

        protected override void RegisterTypes()
        {
            bool useMockServices = false;
#if DEBUG
            useMockServices = Constants.App.DEBUG_UseMocks;
#endif

            // Register Services
            // NOTE:: we register with ContainerControlledLifetimeManager to ensure only one instance exists
            if (useMockServices)
            {
                Container.RegisterType<IAnalyticsService, MockAnalyticsService>(new ContainerControlledLifetimeManager());
                Container.RegisterType<ISettingsService, MockSettingsService>(new ContainerControlledLifetimeManager());
                Container.RegisterType<IDataService, MockDataService>(new ContainerControlledLifetimeManager());
                Container.RegisterType<IReviewService, MockReviewService>(new ContainerControlledLifetimeManager());
                Container.RegisterType<IWindowColorService, MockWindowColorService>(new ContainerControlledLifetimeManager());
            }
            else
            {
                // NOTE:: IAnalyticsService must be registered by platform via IPlatformInitializer.RegisterTypes()
                Container.RegisterType<ISettingsService, LocalSettingsService>(new ContainerControlledLifetimeManager());
                Container.RegisterType<IDataService, SQLiteDataService>(new ContainerControlledLifetimeManager());
                // NOTE:: IReviewService must be registered by platform via IPlatformInitializer.RegisterTypes()
                Container.RegisterType<IWindowColorService, WindowColorService>(new ContainerControlledLifetimeManager());
            }

            // Register Pages
            Container.RegisterTypeForNavigation<NavigationPage>();
            Container.RegisterTypeForNavigation<MainPage>();
            Container.RegisterTypeForNavigation<DailyInfoPage>();
            Container.RegisterTypeForNavigation<GraphPage>();
            Container.RegisterTypeForNavigation<MenuPage>();
            Container.RegisterTypeForNavigation<SetGoalPage>();
            Container.RegisterTypeForNavigation<WeightEntryPage>();
            Container.RegisterTypeForNavigation<GettingStartedPage>();
            Container.RegisterTypeForNavigation<AboutPage>();
            Container.RegisterTypeForNavigation<SettingsPage>();

#if DEBUG
            Container.RegisterTypeForNavigation<DebugPage>();
#endif
        }

        async Task InitializeAsyncServices()
        {
            await Container.Resolve<IDataService>().Initialize(DBPath);
        }

        protected override async void OnStart()
        {
            // Handle when your app starts
            await InitializeAsyncServices();
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}