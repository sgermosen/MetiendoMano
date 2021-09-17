using RssEspecial.Model;
using RssEspecial.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RssEspecial.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        HomePageViewModel viewModel => App.Locator.HomePageViewModel;
        public HomePage()
        {
            InitializeComponent();
           BindingContext= viewModel;
            viewModel.Initlize();
            NavigationPage.SetHasNavigationBar(this, false);
            //MyLabel.Text = " <b>&nbsp;No te olvides de comentar nuevamente y compartirlo con tus amigos desafiandolos con el HashTag #JsMentales</b>";
            //MyLabel.TextType = TextType.Html;
          
        }
        public ItemModel itemModel;
        private void TapGestureRecognizer_Tapped(object sender, System.EventArgs e)
        {
            if(sender is Grid label)
            {
                if (label.BindingContext is ItemModel itemModel)
                {
                    viewModel.ListItemTapped(itemModel);
                }
            }
        }
    }
}