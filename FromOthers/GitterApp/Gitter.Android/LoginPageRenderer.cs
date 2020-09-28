using System.Reactive.Linq;
using Akavache;
using Android.App;
using Gitter;
using Gitter.Android;
using Gitter.ViewModels;
using ReactiveUI;
using Splat;
using Xamarin.Auth;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(LoginPage), typeof(LoginPageRenderer))]

namespace Gitter.Android
{
    public class LoginPageRenderer : PageRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Page> e)
        {
            base.OnElementChanged(e);

            var activity = (Activity)this.Context;

            var auth = new GitterAuth();
            auth.Completed += (sender, eventArgs) =>
            {
                if (eventArgs.IsAuthenticated)
                {
                    BlobCache.Secure.SaveLogin("Gitter", eventArgs.Account.Properties["access_token"], "Gitter").Wait();

                    var screen = Locator.Current.GetService<IScreen>();
                    screen.Router.NavigateAndReset.Execute(new RoomsViewModel());
                }
                else
                {
                    // The user cancelled
                }
            };

            activity.StartActivity(auth.GetUI(activity));
        }
    }
}