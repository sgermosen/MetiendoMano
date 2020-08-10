using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Notif.App.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MasterPage :  MasterDetailPage
    {
        private readonly object Navigator;

        public MasterPage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        App.Navigator = this.Navigator;
    }
    }

}