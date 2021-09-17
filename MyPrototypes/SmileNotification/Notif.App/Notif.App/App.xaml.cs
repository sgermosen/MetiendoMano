using System;
using Notif.App.ViewModels;
using Notif.App.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Notif.App
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainViewModel.GetInstance().Login = new LoginViewModel();
            this.MainPage = new NavigationPage(new LoginPage());
        }

       // public static object Navigator { get; set; }

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
