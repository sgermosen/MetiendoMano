using Premy.Chatovatko.Client.Libs.Database.Models;
using Premy.Chatovatko.Libs.Cryptography;
using Premy.Chatovatko.Libs.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Premy.Chatovatko.Client.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Settings : ContentPage, ILoggable
	{
        private App app;

        public Settings (App app)
		{
			InitializeComponent ();
            this.app = app;
		}

        private void close_Clicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        private async void exportButton_Pressed(object sender, EventArgs e)
        {
            var answer = await DisplayAlert("Export", "Do you really want to export your certificate to your documents?", "Yes", "No");
            if (answer)
            {
                try
                {
                    app.saveFile(X509Certificate2Utils.ExportToPkcs12(app.settings.ClientCertificate), "chatovatkoCert.p12") ;
                }
                catch(Exception ex)
                {
                    ShowError(ex);

                }
            }
        }

        private async void deleteDatabaseButton_Pressed(object sender, EventArgs e)
        {
            var answer = await DisplayAlert("Delete", "Do you really want to delete your database?", "Yes", "No");
            if (answer)
            {
                for(int i = 0; i != 30; i++)
                {
                    try
                    {
                        app.initializator.DBDelete();
                        Environment.Exit(0);
                    }
                    catch
                    {

                    }
                }
                try
                {
                    app.initializator.DBDelete();
                    Environment.Exit(0);
                }
                catch(Exception ex)
                {
                    ShowError(ex);
                }
            }
        }

        private async void ShowError(Exception ex)
        {
            app.logger.LogException(this, ex);
            await DisplayAlert(ex.Source, ex.Message, "OK");
        }

        public string GetLogSource()
        {
            return "Settings";
        }
    }
}
