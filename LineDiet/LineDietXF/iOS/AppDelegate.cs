using Foundation;
using LineDietXF.Interfaces;
using LineDietXF.iOS.Renderers;
using LineDietXF.iOS.Services;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Unity;
using System;
using System.IO;
using UIKit;

namespace LineDietXF.iOS
{
    /// <summary>
    /// Responsible for initializing iOS application. Responsible for registering app specific services (ex: IAnalyticsService).
    /// </summary>
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate, IPlatformInitializer
    {
        App _app;

        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            // NOTE:: status bar text being set to white is done in CustomNavigationPageRenderer

            // Set nav bar title to white
            UINavigationBar.Appearance.SetTitleTextAttributes(new UITextAttributes() { TextColor = UIColor.White });
            UITextField.Appearance.TintColor = UIColor.White;

            global::Xamarin.Forms.Forms.Init();
            OxyPlot.Xamarin.Forms.Platform.iOS.PlotViewRenderer.Init();

            string documentPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libraryPath = Path.Combine(documentPath, "../Library/");
            var path = Path.Combine(libraryPath, LineDietXF.Constants.App.SQLite_DB_Filename);

            _app = new App(this, path);
            LoadApplication(_app);

            CustomTabbedPageRenderer.EventAggregator = _app.Container.Resolve<IEventAggregator>();
            CustomNavigationPageRenderer.EventAggregator = _app.Container.Resolve<IEventAggregator>();

            return base.FinishedLaunching(app, options);
        }

        public void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<IReviewService, ReviewService>(new ContainerControlledLifetimeManager());

            bool useMockServices = false;
#if DEBUG
            useMockServices = LineDietXF.Constants.App.DEBUG_UseMocks;
#endif

            if (!useMockServices) // main XF App.xaml.cs will register a mock IAnalyticsService if this is true
                container.RegisterType<IAnalyticsService, AnalyticsService>(new ContainerControlledLifetimeManager());
        }
    }
}