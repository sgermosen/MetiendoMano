using FavFighters.ViewModels;
using Xamarin.Forms;

namespace FavFighters.Views
{
    public partial class FavFightersView : ContentPage
    {
        public FavFightersView()
        {
            InitializeComponent();
            BindingContext = new FavFightersViewModel();
        }
    }
}