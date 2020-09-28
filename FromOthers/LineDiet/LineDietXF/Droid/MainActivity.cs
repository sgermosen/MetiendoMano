using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Support.Design.Widget;
using LineDietXF.Droid.Services;
using LineDietXF.Events;
using LineDietXF.Interfaces;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Unity;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace LineDietXF.Droid
{
    /// <summary>
    /// Main Activity for Android application. Responsible for registering app specific services (ex: IAnalyticsService).
    /// </summary>
    [Activity(Label = "Line Diet", Icon = "@drawable/ic_launcher", Theme = "@style/MyTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity, IPlatformInitializer
    {
        public static IAnalyticsService AnalyticsService { get; private set; }
        App _app;

        protected override void OnCreate(Bundle bundle)
        {
            // set style resources for tabs and toolbar
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            OxyPlot.Xamarin.Forms.Platform.Android.PlotViewRenderer.Init();

            // get the path for the database
            string path = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), Constants.App.SQLite_DB_Filename);

            // load the main app
            _app = new App(this, path);
            LoadApplication(_app);

            // subscribe to the ChangeColorEvent for updating tabs and toolbar colors
            var container = _app.Container;
            var eventAggregator = container.Resolve<IEventAggregator>();
            eventAggregator.GetEvent<ChangeColorEvent>().Subscribe(HandleChangeColorEvent);
        }

        public void RegisterTypes(IUnityContainer container)
        {
            var reviewService = new ReviewService(this, PackageName);
            container.RegisterInstance<IReviewService>(reviewService, new ContainerControlledLifetimeManager());

            bool useMockServices = false;
#if DEBUG
            useMockServices = LineDietXF.Constants.App.DEBUG_UseMocks;
#endif

            if (!useMockServices) // main XF App.xaml.cs will register a mock IAnalyticsService if this is true
            {
                AnalyticsService = new AnalyticsService(this);
                container.RegisterInstance<IAnalyticsService>(AnalyticsService, new ContainerControlledLifetimeManager());
            }
        }

        void HandleChangeColorEvent(Color color)
        {
            TabLayout tabLayout = FindViewById(Resource.Id.sliding_tabs) as TabLayout;
            if (tabLayout != null)
                tabLayout.SetBackgroundColor(color.ToAndroid());

            var navPage = _app.MainPage as NavigationPage;
            if (navPage != null)
                navPage.BarBackgroundColor = color;
        }
    }
}