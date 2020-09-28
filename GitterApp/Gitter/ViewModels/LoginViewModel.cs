using System;
using ReactiveUI;
using Splat;

namespace Gitter.ViewModels
{
    public class LoginViewModel : ReactiveObject, IRoutableViewModel
    {
        public LoginViewModel(IScreen hostScreen = null)
        {
            this.HostScreen = hostScreen ?? Locator.Current.GetService<IScreen>();
        }

        public IScreen HostScreen { get; private set; }

        public string UrlPathSegment
        {
            get { return "Login"; }
        }
    }
}