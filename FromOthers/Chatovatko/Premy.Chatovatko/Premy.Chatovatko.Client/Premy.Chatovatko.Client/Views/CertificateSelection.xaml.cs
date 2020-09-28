using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Premy.Chatovatko.Client.Cryptography;
using Premy.Chatovatko.Libs.Cryptography;
using Premy.Chatovatko.Libs.Logging;
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
    public partial class CertificateSelection : ContentPage, ILoggable
    {
        App app;
        Logger logger;

        public CertificateSelection(App app, Logger logger)
        {
            InitializeComponent();
            this.app = app;
            this.logger = logger;
            IsGenerating = false;
            IsLoadingFile = false;
        }

        private async void Generate()
        {
            X509Certificate2 clientCert = null;
            IsGenerating = true;
            await Task.Run(() =>
            {
                clientCert = X509Certificate2Generator.GenerateCACertificate(logger);
            });
            
            app.AfterCertificateSelected(clientCert);
        }

        private async void LoadFromFile()
        {
            IsLoadingFile = true;
            try
            {

                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);
                if (status != PermissionStatus.Granted)
                {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Storage))
                    {
                        await DisplayAlert("Need access storage", "It is nessecary for certificate loading.", "OK");
                    }

                    var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Storage);
                    status = results[Permission.Storage];
                }

                if (status == PermissionStatus.Denied)
                {
                    await DisplayAlert("Error", "Storage permission is not granted.", "OK");
                    IsLoadingFile = false;
                    return;
                }


                FileData fileData = await CrossFilePicker.Current.PickFile();

                if (fileData == null)
                {
                    IsLoadingFile = false;
                    return;
                }

                X509Certificate2 cert = X509Certificate2Utils.ImportFromPkcs12(fileData.DataArray, true);

                app.AfterCertificateSelected(cert);
            }
            catch(Exception ex)
            {
                logger.LogException(this, ex);
                await DisplayAlert("Error", "Certificate loading failed.", "OK");
                IsLoadingFile = false;
            }
        }

        public string GetLogSource()
        {
            return "Certificate selector";
        }

        private bool _IsGenerating;
        public bool IsGenerating
        {
            get { return _IsGenerating; }
            set
            {
                _IsGenerating = value;
                generatingLabel.IsVisible = value;
                activityIndicator.IsVisible = value;
                activityIndicator.IsRunning = value;

                introLabel.IsVisible = !value;
                buttonLayout.IsVisible = !value;
            }
        }

        private bool _IsLoadingFile;
        public bool IsLoadingFile
        {
            get => _IsLoadingFile;
            set
            {
                _IsLoadingFile = value;
                loadingFileLabel.IsVisible = value;
                activityIndicator.IsVisible = value;
                activityIndicator.IsRunning = value;

                introLabel.IsVisible = !value;
                buttonLayout.IsVisible = !value;
            }
        }

    }
}
