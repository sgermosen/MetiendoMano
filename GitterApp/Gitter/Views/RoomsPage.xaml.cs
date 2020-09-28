using System;
using System.Reactive.Linq;
using Gitter.ViewModels;
using ReactiveUI;
using Xamarin.Forms;

namespace Gitter.Views
{
    public partial class RoomsPage : MasterDetailPage, IViewFor<RoomsViewModel>
    {
        public static readonly BindableProperty ViewModelProperty =
            BindableProperty.Create<RoomsPage, RoomsViewModel>(x => x.ViewModel, default(RoomsViewModel));

        public RoomsPage()
        {
            InitializeComponent();

            this.WhenAnyValue(x => x.ViewModel)
                .Subscribe(x => this.BindingContext = x);

            this.WhenAnyValue(x => x.ViewModel)
               .Where(x => x != null)
               .InvokeCommand(this, x => x.ViewModel.LoadRooms);

            this.WhenAnyValue(x => x.ViewModel.SelectedRoom)
                .Where(r => r != null)
                .Subscribe(r =>
                    {
                        //show the room name, because it happens not automatically
                        this.Title = r.Name;
                        this.Detail.BindingContext = new MessagesViewModel(r.Room);
                        this.ViewModel.SelectedRoom = null;
                        this.IsPresented = false;
                    });

            // show the master page first; setting IsPresented to true outright in the constructor does not work
            this.Events().Appearing
                .Where(x => x != null)
                .Subscribe(_ => this.IsPresented = true);
        }

        object IViewFor.ViewModel
        {
            get { return this.ViewModel; }
            set { this.ViewModel = (RoomsViewModel)value; }
        }

        public RoomsViewModel ViewModel
        {
            get { return (RoomsViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }
    }
}