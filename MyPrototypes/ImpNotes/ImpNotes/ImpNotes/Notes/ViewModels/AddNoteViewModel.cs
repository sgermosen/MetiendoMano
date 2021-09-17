using System;
using System.Collections.Generic;
using GalaSoft.MvvmLight.Command;
using ImpNotes.Services;

namespace ImpNotes.Notes.ViewModels
{
    public class AddNoteViewModel : NotesBaseViewModel
    {
        public List<string> ColorNames = new List<string>();

        public RelayCommand SaveCommand => new RelayCommand(SaveClick);

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
            noteService.Add(new Models.NotesModel()
            {
                Title = this.Title,
                Text = this.Text,
                TextColor = this.SelectedTextColor
            });
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

        public void Initilize()
        {
            //ColorNames.Clear();

            //foreach (var field in typeof(Xamarin.Forms.Color).GetFields(BindingFlags.Static | BindingFlags.Public))
            //{
            //    if (field != null && !String.IsNullOrEmpty(field.Name))
            //        ColorNames.Add(field.Name);
            //}
            //SelectedTextColor = "Black";

            // Text = @"Replase this text with your note";
            this.IsEnabled = true;
        }



    }
}
