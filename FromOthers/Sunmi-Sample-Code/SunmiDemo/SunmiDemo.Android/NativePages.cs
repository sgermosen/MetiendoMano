using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Bluetooth;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SunmiDemo.Droid;
using SunmiDemo.ViewModel;

[assembly: Xamarin.Forms.Dependency(typeof(NativePages))]
namespace SunmiDemo.Droid
{
    public class NativePages : INativePages
    {
        public NativePages()
        {

        }

        public async void StartActivityInAndroid(string message2)
        {
            Print print = new Print();
            string printerName = "";

            if (BluetoothAdapter.DefaultAdapter != null && BluetoothAdapter.DefaultAdapter.IsEnabled)
            {
                foreach (var pairedDevice in BluetoothAdapter.DefaultAdapter.BondedDevices)
                {
                    // add validation to select InnerPrinter
                    if (pairedDevice.Address == "66:22:61:E1:4D:F1")
                        printerName = pairedDevice.Name;
                }

                if (printerName != null)
                {
                    await print.PrintText(message2, printerName);

                }

            }
        }
    }
}