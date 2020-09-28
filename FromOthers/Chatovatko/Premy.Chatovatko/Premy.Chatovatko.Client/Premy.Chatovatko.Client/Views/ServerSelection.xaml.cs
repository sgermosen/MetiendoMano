using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Premy.Chatovatko.Client.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ServerSelection : ContentPage
	{
        private X509Certificate2 cert;
        private App app;
        private String error;

		public ServerSelection (App app, X509Certificate2 cert)
		{
			InitializeComponent ();
            this.cert = cert;
            this.app = app;
		}

        public ServerSelection(App app, X509Certificate2 cert, String serverAddress, String password, String userName, String error) : this(app, cert)
        {
            serverAddressEntry.Text = serverAddress;
            passwordEntry.Text = password;
            userNameEntry.Text = userName;
            this.error = error;
        }

        public async void DisplayError(String message)
        {
            if(message == null)
            {
                return;
            }
            await DisplayAlert("Error", message, "OK");
        }

        protected override void OnAppearing()
        {
            this.DisplayError(error);
        }

        public void Confirm()
        {
            app.AfterServerSelected(cert, serverAddressEntry.Text, passwordEntry.Text, userNameEntry.Text);
        }

	}
}
