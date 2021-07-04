using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Bluetooth;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Util;
using XF.Bluetooth.Printer.Plugin.Abstractions;

namespace SunmiDemo.Droid
{
    public class Print : IPrint
    {
        public async Task PrintText(string input, string printerName)
        {
            using (BluetoothAdapter bluetoothAdapter = BluetoothAdapter.DefaultAdapter)
            {
                if (bluetoothAdapter == null)
                {
                    throw new Exception("No default adapter");
                    //return;
                }

                if (!bluetoothAdapter.IsEnabled)
                {
                    throw new Exception("Bluetooth not enabled");
                }

                BluetoothDevice device = (from bd in bluetoothAdapter.BondedDevices
                                          where bd.Name == printerName
                                          select bd).FirstOrDefault();
                if (device == null)
                    throw new Exception(printerName + " device not found.");

                try
                {
                    using (BluetoothSocket _socket = device.CreateRfcommSocketToServiceRecord(UUID.FromString("00001101-0000-1000-8000-00805f9b34fb")))
                    {

                        await _socket.ConnectAsync();
                        

                        switch (input)
                        {
                            case "LF":
                                _socket.OutputStream.WriteByte(0x0A);
                                break;

                            default:
                                byte[] message = System.Text.Encoding.ASCII.GetBytes(input);
                                await _socket.OutputStream.WriteAsync(message, 0, message.Length);
                                _socket.OutputStream.WriteByte(0x0A);
                                break;
                        }
                        
                        _socket.Close();
                    }
                }
                catch (Exception exp)
                {

                    throw exp;
                }


            }
        }
    }
}