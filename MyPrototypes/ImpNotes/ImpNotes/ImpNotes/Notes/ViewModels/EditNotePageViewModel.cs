using System;
using System.Collections.Generic;
using GalaSoft.MvvmLight.Command;
using ImpNotes.Models;
using ImpNotes.Services;

namespace ImpNotes.Notes.ViewModels
{
    public class EditNotePageViewModel : NotesBaseViewModel
    {
        public List<string> ColorNames = new List<string>();

        public RelayCommand SaveCommand => new RelayCommand(SaveClick);


        NotesModel Note { get; set; }

        public async void SaveClick()
        {
            if (String.IsNullOrEmpty(Title))
            {
                await App.Current.MainPage.DisplayAlert("Error", "Title field cannot be empty !", "ok");
                return;
            }
            if (String.IsNullOrEmpty(Text))
            {
                await App.Current.MainPage.DisplayAlert("Error", "Notes field cannot be empty !", "ok");
                return;
            }

            DataService noteService = new DataService();
            Note.Text = this.Text;
            Note.TextColor = SelectedTextColor;
            Note.Title = this.Title;
            Note.UpdatedDate = DateTime.UtcNow;
            noteService.Update(Note);

            //Text = String.Empty;
            // (App.Current.MainPage as Shell).GoToAsync("app:///newapp/greeting?msg=Hello");

            //    await Shell.Current.GoToAsync("//animals/monkeys");
            //(App.Current.MainPage as Shell).("app:///newapp/greeting?msg=Hello");
            //await App.Current.MainPage.Navigation.PopAsync();
            // await App.Navigator.PopAsync(true);

        }

        public RelayCommand SetTextColorCommmand => new RelayCommand(SetTextColorClick);

        private async void SetTextColorClick()
        {
            var result = await App.Current.MainPage.DisplayActionSheet("Select Color", "cancel", null, ColorNames.ToArray());
            if (result == "cancel")
            {
                return;
            }
            SelectedTextColor = result;
        }

        public void Initilize(NotesModel model)
        {
            //ColorNames.Clear();

            //foreach (var field in typeof(Xamarin.Forms.Color).GetFields(BindingFlags.Static | BindingFlags.Public))
            //{
            //    if (field != null && !String.IsNullOrEmpty(field.Name))
            //        ColorNames.Add(field.Name);
            //}
            //SelectedTextColor = "Black";

            // Text = @"Replase this text with your note";

            Text = model.Text;
            Title = model.Title;
            SelectedTextColor = model.TextColor;

            this.IsEnabled = true;
        }

        public EditNotePageViewModel(NotesModel model)
        {
            Text = model.Text;
            Title = model.Title;
            SelectedTextColor = model.TextColor;
            Note = model;
        }



    }
}
