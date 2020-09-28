using System;
using Android.App;
using Android.Runtime;
using ReactiveUI;

namespace Gitter.Android
{
    [Application(Label = "Gitter", Theme = "@android:style/Theme.Holo.Light",
#if DEBUG
 Debuggable = true
#else
 Debuggable = false
#endif
)]
    public class AndroidApplication : Application
    {
        private AutoSuspendHelper autoSuspendHelper;

        public AndroidApplication(IntPtr handle, JniHandleOwnership transfer)
            : base(handle, transfer)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();

            autoSuspendHelper = new AutoSuspendHelper(this);
            RxApp.SuspensionHost.CreateNewAppState = () => new AppBootstrapper();

            RxApp.SuspensionHost.SetupDefaultSuspendResume();
        }
    }
}