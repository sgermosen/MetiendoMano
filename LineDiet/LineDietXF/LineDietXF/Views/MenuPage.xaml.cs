using LineDietXF.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LineDietXF.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPage : ContentPage
    {
        public MenuPage()
        {
            InitializeComponent();
        }

        void MenuListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            MenuListView.SelectedItem = null; // clear selection (so it doesn't stay highlighted)
        }
    }
}