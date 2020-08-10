using System;
using ImpNotes.Models;
using ImpNotes.Notes.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ImpNotes.Notes.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditNotePage : ContentPage
    {
        EditNotePageViewModel viewModel;
        //  public NotesModel Note { get; set; }

        public EditNotePage(NotesModel notesModel)
        {
            InitializeComponent();

            BindingContext = viewModel = new EditNotePageViewModel(notesModel);

            viewModel.Initilize(notesModel);
        }



        //private void Button_Clicked(object sender, EventArgs e)
        //{
        //    DependencyService.Get<IAdInterstitial>().ShowAd();

        //}

        async void Save_Clicked(object sender, EventArgs e)
        {
            //MessagingCenter.Send(this, "AddItem", Item);
            viewModel.SaveClick();
            //  await Navigation.PopAsync();
            //await (App.Current.MainPage as Shell)
            //    .GoToAsync( new NotesListPage());
            await Navigation.PopToRootAsync();
         // await Navigation.PushAsync((new NotesListPage()));
            //  await Navigation.RemovePage(all)
          //  await Application.Current.MainPage = new AppShell();
           //  await App.Current.MainPage.Navigation.PushAsync(new NotesListPage());
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}