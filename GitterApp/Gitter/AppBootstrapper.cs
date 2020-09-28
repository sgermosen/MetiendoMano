using System.Collections.Generic;
using System.Net.Http;
using System.Reactive.Linq;
using Akavache;
using Gitter.ViewModels;
using Gitter.Views;
using ModernHttpClient;
using ReactiveUI;
using ReactiveUI.XamForms;
using Splat;
using Xamarin.Forms;

namespace Gitter
{
    public class AppBootstrapper : ReactiveObject, IScreen
    {
        public AppBootstrapper()
        {
            Router = new RoutingState();
            Locator.CurrentMutable.RegisterConstant(this, typeof(IScreen));

            Locator.CurrentMutable.RegisterLazySingleton(() => new NativeMessageHandler(), typeof(HttpMessageHandler));

            Locator.CurrentMutable.Register(() => new LoginPage(), typeof(IViewFor<LoginViewModel>));
            Locator.CurrentMutable.Register(() => new RoomsPage(), typeof(IViewFor<RoomsViewModel>));
            //Locator.CurrentMutable.Register(() => new MessagesPage(), typeof(IViewFor<MessagesViewModel>));

            LoginInfo loginInfo = BlobCache.Secure.GetLoginAsync("Gitter")
                .Catch<LoginInfo, KeyNotFoundException>(ex => Observable.Return((LoginInfo)null))
                .Wait();

            if (loginInfo == null)
            {
                this.Router.Navigate.Execute(new LoginViewModel());
            }

            else
            {
                this.Router.Navigate.Execute(new RoomsViewModel());
            }
        }

        public RoutingState Router { get; protected set; }

        public Page CreateMainPage()
        {
            return new RoutedViewHost();
        }
    }
}