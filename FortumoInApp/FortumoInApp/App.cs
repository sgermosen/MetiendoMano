using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

using InApp.Pages;
using InApp.ViewModels;

namespace InApp
{
     
    public class App : Application
    {
        public static App Instance;
        public static InAppViewModel ViewModel;

        public App()
        {
            Instance = this;

            ViewModel = new InAppViewModel();
            ViewModel.RestoreState(Current.Properties);

            MainPage = new RootPage();
        }

        
        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
            ViewModel.SaveState(Current.Properties);
        }

        protected override void OnResume()
        {
        }
    }
}
