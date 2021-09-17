using System;
using ImpNotes.Models;
using ImpNotes.Notes.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ImpNotes.Notes.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddNotePage : ContentPage
    {
        AddNoteViewModel viewModel;
        public NotesModel Note { get; set; }

        public AddNotePage()
        {
            InitializeComponent();

            Note = new NotesModel
            {
                Title = "",
                Text = ""
            };

          //  BindingContext = this;
           BindingContext = viewModel = new AddNoteViewModel();
           viewModel.Initilize();
        }



        //private void Button_Clicked(object sender, EventArgs e)
        //{
        //    DependencyService.Get<IAdInterstitial>().ShowAd();

        //}

        //async void Save_Clicked(object sender, EventArgs e)
        //{
        //    //MessagingCenter.Send(this, "AddItem", Item);
        //    viewModel.SaveClick();
        //    await Navigation.PopAsync();
        //}

        //async void Cancel_Clicked(object sender, EventArgs e)
        //{
        //    await Navigation.PopAsync();
        //}
    }
}