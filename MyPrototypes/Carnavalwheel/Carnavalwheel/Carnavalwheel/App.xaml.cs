using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Carnavalwheel
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            var firstPage = new NavigationPage(new RoulettePage());
            MainPage = firstPage;
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
