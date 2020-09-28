using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;

namespace LineDietXF.Droid
{
    /// <summary>
    /// The first screen the user sees - the splash screen.
    /// Reference: https://developer.xamarin.com/guides/android/user_interface/creating_a_splash_screen/
    /// </summary>
    [Activity(Theme = "@style/MyTheme.Splash", MainLauncher = true, NoHistory = true)]
    public class SplashActivity : AppCompatActivity
    {
        static readonly string TAG = "X:" + typeof(SplashActivity).Name;

        public override void OnCreate(Bundle savedInstanceState, PersistableBundle persistentState)
        {
            base.OnCreate(savedInstanceState, persistentState);
        }

        protected override async void OnResume()
        {
            base.OnResume();

            await Task.Delay(1); // just letting the system catch up now before launching real main activity

            StartActivity(new Intent(Application.Context, typeof(MainActivity)));
        }
    }
}