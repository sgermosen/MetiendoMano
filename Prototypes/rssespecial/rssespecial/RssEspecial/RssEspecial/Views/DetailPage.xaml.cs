using RssEspecial.Model;
using RssEspecial.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RssEspecial.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailPage : ContentPage
    {
        DetailPageViewModel viewModel => App.Locator.DetailPageViewModel;
        public DetailPage(ItemModel model)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            BindingContext = viewModel;
            viewModel.Initilize(model);
        }
    }
}