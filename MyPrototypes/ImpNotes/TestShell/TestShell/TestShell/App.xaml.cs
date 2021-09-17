using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml; 
using TestShell.Views;
using Xamarin.Essentials;

namespace TestShell
{
    public partial class App : Application
    {
        public static bool IsWindows10 { get; set; }
        public App()
        {
            InitializeComponent();
            //Barrel.ApplicationId = AppInfo.PackageName;

            // The root page of your application
            if (DeviceInfo.Platform == DevicePlatform.UWP)
                MainPage = new AboutPage();
            else
                MainPage = new AppShell();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
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
