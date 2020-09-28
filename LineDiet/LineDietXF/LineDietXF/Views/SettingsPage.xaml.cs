using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LineDietXF.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();            
        }

        void SettingsListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            SettingsListView.SelectedItem = null; // clear selection (so it doesn't stay highlighted)
        }
    }
}
