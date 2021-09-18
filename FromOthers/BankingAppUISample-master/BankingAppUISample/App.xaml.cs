using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BankingAppUISample
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new Views.BankingProfilePage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
