using ImpNotes.Models;
using ImpNotes.Notes.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ImpNotes.Notes.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TextToImagePage : ContentPage
	{
        TextToImagePageViewModel viewModel;
        public TextToImagePage(NotesModel notes)
        {
            InitializeComponent();
            BindingContext = viewModel = new TextToImagePageViewModel();
            viewModel.Initilize(notes);
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }
    }
}