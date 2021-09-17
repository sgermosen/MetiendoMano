using System;
using ImpNotes.Models;
using ImpNotes.Notes.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ImpNotes.Notes.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NotesDetailPage : ContentPage
	{
        NotesDetailPageViewModel viewModel;
        private NotesDetailPageViewModel notesDetailPageViewModel;

        public NotesModel Note { get; set; }

        public NotesDetailPage(NotesModel notesModel)
        {
            InitializeComponent();

            BindingContext = viewModel = new NotesDetailPageViewModel(notesModel);

            viewModel.Initilize(notesModel);
            Note = notesModel;

        }

        ////I dont want to use
        //async void Edit_Clicked(object sender, EventArgs e)
        //{
        //    await Navigation.PushAsync((new EditNotePage(Note)));
        //   // await Navigation.PushAsync(new ItemDetailPage(new EditNotePage(Note)));

        //}
    }
}