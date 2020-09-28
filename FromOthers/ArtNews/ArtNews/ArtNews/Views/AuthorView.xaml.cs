using ArtNews.ViewModels;
using Plugin.SharedTransitions;
using Xamarin.Forms;

namespace ArtNews.Views
{
    public partial class AuthorView : ContentPage
    {
        public AuthorView()
        {
            InitializeComponent();
            BindingContext = new AuthorViewModel();

            SharedTransitionNavigationPage.SetBackgroundAnimation(this, BackgroundAnimation.Fade);
            SharedTransitionNavigationPage.SetTransitionDuration(this, 500);
        }

        private async void OnHighlightTapped(object sender, System.EventArgs e)
        {
            SharedTransitionNavigationPage.SetTransitionSelectedGroup(this, "Banner");     
            var context = (sender as View).BindingContext;
            await Navigation.PushAsync(new ArtDetailView(context));
        }
    }
}