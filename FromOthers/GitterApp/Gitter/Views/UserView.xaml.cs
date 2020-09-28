using Gitter.ViewModels;
using ReactiveUI;
using Xamarin.Forms;
using System;
using System.IO;
using System.Reactive.Linq;

namespace Gitter.Views
{
    public partial class UserView : ContentView, IViewFor<UserViewModel>
    {
        public static readonly BindableProperty ViewModelProperty =
            BindableProperty.Create<UserView, UserViewModel>(x => x.ViewModel, default(UserViewModel));

        public UserView()
        {
            InitializeComponent();

            this.WhenAnyValue(x => x.ViewModel)
                .Where(x => x != null)
                .Subscribe(x => this.BindingContext = x);

            this.WhenAnyValue(x => x.ViewModel.AvatarSource)
                .Where(x => x != null)
                .SelectMany(AvatarHelper.LoadAvatar)
                .Select(x => ImageSource.FromStream(() => new MemoryStream(x)))
                .ObserveOn(RxApp.MainThreadScheduler)
                .BindTo(this, x => x.Avatar.Source);
        }

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (UserViewModel)value; }
        }

        public UserViewModel ViewModel
        {
            get { return (UserViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }
    }
}

