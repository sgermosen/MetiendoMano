using Gitter.ViewModels;
using ReactiveUI;
using Xamarin.Forms;

namespace Gitter
{
    public class LoginPage : ContentPage, IViewFor<LoginViewModel>
    {
        public static readonly BindableProperty ViewModelProperty = BindableProperty.Create<LoginPage, LoginViewModel>(x => x.ViewModel, default(LoginViewModel));

        public LoginPage()
        {
        }

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (LoginViewModel)value; }
        }

        public LoginViewModel ViewModel
        {
            get { return (LoginViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }
    }
}