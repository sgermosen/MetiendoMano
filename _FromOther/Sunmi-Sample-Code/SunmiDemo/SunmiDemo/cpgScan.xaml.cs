using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

namespace SunmiDemo
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class cpgScan : ContentPage
	{
		public cpgScan ()
		{
			InitializeComponent ();
		}

        private async void BtnCameraScan_Clicked(object sender, EventArgs e)
        {
            ntrLaserBarcode.Text = "";

            var scan = new ZXingScannerPage();
            await Navigation.PushAsync(new NavigationPage(scan));
            scan.OnScanResult += (result) =>
            {

                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Navigation.PopAsync();
                    ntrLaserBarcode.Text = result.Text;
                });
            };
        }
    }
}