
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.OS;
using Plugin.CurrentActivity;

namespace ImpNotes.Droid
{
    [Activity(Label = "ImpNotes", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public static int width, height;
        public static MainActivity Instance;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            global::Xamarin.Forms.Forms.SetFlags("Shell_Experimental", "Visual_Experimental", "CollectionView_Experimental", "FastRenderers_Experimental");
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            Instance = this;

            CrossCurrentActivity.Current.Init(this, savedInstanceState);

            Display display = WindowManager.DefaultDisplay;
            Android.Graphics.Point size = new Android.Graphics.Point();
            display.GetSize(size);
            width = size.X;
            height = size.Y;

            Android.Gms.Ads.MobileAds.Initialize(ApplicationContext, Ads.AdConstant.AppId);
        
            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}