using League.ViewModels;
using Xamarin.Forms;

namespace League.Views
{
    public partial class StandingsPage : ContentPage
    {
        public StandingsPage(StandingsViewModel vm)
        {
            InitializeComponent();

            BindingContext = vm;
        }
    }
}
