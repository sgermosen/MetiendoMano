using Gitter.ViewModels;
using ReactiveUI;
using Xamarin.Forms;
using System;
using System.IO;
using System.Reactive.Linq;

namespace Gitter.Views
{
    public partial class MessageView : ContentView, IViewFor<MessageViewModel>
    {
        public static readonly BindableProperty ViewModelProperty =
            BindableProperty.Create<MessageView, MessageViewModel>(x => x.ViewModel, default(MessageViewModel));

        public MessageView()
        {
            InitializeComponent();

            this.WhenAnyValue(x => x.ViewModel)
                .Where(x => x != null)
                .Subscribe(x => this.BindingContext = x);

            this.WhenAnyValue(x => x.ViewModel.SenderAvatarSource)
                .Where(x => x != null)
                .SelectMany(AvatarHelper.LoadAvatar)
                .Select(x => ImageSource.FromStream(() => new MemoryStream(x)))
                .ObserveOn(RxApp.MainThreadScheduler)
                .BindTo(this, x => x.Avatar.Source);
        }

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (MessageViewModel)value; }
        }

        public MessageViewModel ViewModel
        {
            get { return (MessageViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }
    }
}