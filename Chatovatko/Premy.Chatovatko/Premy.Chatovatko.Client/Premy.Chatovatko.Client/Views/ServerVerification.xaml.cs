using Premy.Chatovatko.Libs.Cryptography;
using Premy.Chatovatko.Libs.DataTransmission.JsonModels;
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
	public partial class ServerVerification : ContentPage
	{
        App app;
        ServerInfo info;
        X509Certificate2 clientCert;
        String serverAddress;
        String password;
        String userName;

		public ServerVerification (App app, ServerInfo info, X509Certificate2 clientCert, String serverAddress, String password, String userName)
		{
			InitializeComponent ();
            this.app = app;
            this.clientCert = clientCert;
            this.info = info;
            this.serverAddress = serverAddress;
            this.password = password;
            this.userName = userName;

            StringBuilder builder = new StringBuilder("Do you trust this server?\n\n");
            builder.AppendLine("Server name:");
            builder.AppendLine(info.Name);
            builder.AppendLine();
            builder.AppendLine("Public key SHA-256 sum:");
            builder.AppendLine(SHA256Utils.ComputeCertHash(info.PublicCertificate));
            builder.AppendLine();
            builder.AppendLine("Password required:");
            builder.Append(info.PasswordRequired);

            textLabel.Text = builder.ToString();

        }

        public void Trusted()
        {
            app.AfterServerConfirmed(clientCert, info, serverAddress, password, userName);
        }

        public void NotTrusted()
        {
            app.MainPage = new ServerSelection(app, clientCert, serverAddress, password, userName, null);
        }

    }
}
