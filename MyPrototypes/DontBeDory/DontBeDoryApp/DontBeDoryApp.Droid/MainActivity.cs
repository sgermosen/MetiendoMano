using System;

using Android.App;
using Android.Content.PM;
using Android.Gms.Ads;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using Gcm.Client;

namespace DontBeDoryApp.Droid
{
	[Activity (Label = "DontBeDoryApp", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
            // GCM
            instance = this;
            /////

            base.OnCreate (bundle);

			global::Xamarin.Forms.Forms.Init (this, bundle);
            Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init();
            LoadApplication (new DontBeDoryApp.App ());
		    MobileAds.Initialize(ApplicationContext, "ca-app-pub-8521044456540023~7459937605");

            // GCM
            try
            {
                GcmClient.CheckDevice(this);
                GcmClient.CheckManifest(this);
                System.Diagnostics.Debug.WriteLine("Registering...");
                GcmClient.Register(this, PushHandlerBroadcastReceiver.SENDER_IDS);
            }
            catch (Java.Net.MalformedURLException)
            {
                CreateAndShowDialog("There was an error creating the client. Verify the URL.", "Error");
            }
            catch (Exception e)
            {
                CreateAndShowDialog(e.Message, "Error");
            }
        }

        private void CreateAndShowDialog(String message, String title)
        {
            AlertDialog.Builder builder = new AlertDialog.Builder(this);

            builder.SetMessage(message);
            builder.SetTitle(title);
            builder.Create().Show();
        }

        static MainActivity instance = null;

        public static MainActivity CurrentActivity
        {
            get { return instance; }
        }

    }
}

