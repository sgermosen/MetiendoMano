using Premy.Chatovatko.Libs.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Premy.Chatovatko.Client.Helpers
{
    public abstract class CustomContentPage : ContentPage, ILoggable
    {
        protected App app;

        public CustomContentPage(App app)
        {
            this.app = app;
        }

        public abstract string GetLogSource();

        private async void ShowError(Exception ex)
        {
            app.logger.LogException(this, ex);
            await DisplayAlert(ex.Source, ex.Message, "OK");
        }
    }
}
