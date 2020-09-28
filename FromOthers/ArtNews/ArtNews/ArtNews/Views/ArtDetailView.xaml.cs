using ArtNews.ViewModels;
using Plugin.SharedTransitions;
using Xamarin.Forms;

namespace ArtNews.Views
{
    public partial class ArtDetailView : ContentPage
    {
        public ArtDetailView(object parameter)
        {
            InitializeComponent();
            BindingContext = new ArtDetailViewModel(parameter);

            if (Device.RuntimePlatform == "Android")
                NavigationPage.SetHasNavigationBar(this, false);

            SharedTransitionNavigationPage.SetTransitionDuration(this, 500);
        }
    }
}