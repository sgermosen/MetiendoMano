using Gitter.ViewModels;
using ReactiveUI;
using Xamarin.Forms;
using System;
using System.IO;
using System.Reactive.Linq;

namespace Gitter.Views
{
    public partial class RoomView : ContentView, IViewFor<RoomViewModel>
    {
        public static readonly BindableProperty ViewModelProperty =
            BindableProperty.Create<RoomView, RoomViewModel>(x => x.ViewModel, default(RoomViewModel));

        public RoomView()
        {
            InitializeComponent();

            this.WhenAnyValue(x => x.ViewModel)
                .Where(x => x != null)
                .Subscribe(x => this.BindingContext = x);

            this.WhenAnyValue(x => x.ViewModel.UserAvatarSource)
                .Where(x => x != null)
                .SelectMany(AvatarHelper.LoadAvatar)
                .Select(x => ImageSource.FromStream(() => new MemoryStream(x)))
                .ObserveOn(RxApp.MainThreadScheduler)
                .BindTo(this, x => x.Avatar.Source);
        }

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (RoomViewModel)value; }
        }

        public RoomViewModel ViewModel
        {
            get { return (RoomViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }
    }
}