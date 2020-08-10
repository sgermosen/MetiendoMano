using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using DontBeDoryApp.Datos;
using DontBeDoryApp.Paginas;

namespace DontBeDoryApp
{
	public class App : Application
	{
        public static AzureDataService AzureService = new AzureDataService();

        public App ()
		{
            SQLitePCL.Batteries.Init();
            MainPage = new NavigationPage(new PaginaListaNotas());
        }

        protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
