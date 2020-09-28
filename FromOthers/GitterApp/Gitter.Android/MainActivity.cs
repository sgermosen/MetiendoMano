using Android.App;
using Android.Content.PM;
using Android.OS;
using ReactiveUI;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace Gitter.Android
{
    [Activity(Label = "Gitter", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : AndroidActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            Forms.Init(this, bundle);

            var bootstrapper = RxApp.SuspensionHost.GetAppState<AppBootstrapper>();
            this.SetPage(bootstrapper.CreateMainPage());
        }
    }
}