using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using ImpNotes.Notes.Views;
using ImpNotes.Services;

namespace ImpNotes.Notes.ViewModels
{
    public class NotesMainPageViewModel : NotesBaseViewModel
    {

        private NavigationService navigationService;
        
        public NotesMainPageViewModel()
        {
            navigationService = new NavigationService();
        }

        public string PageName { get; set; }



        public ICommand AddNoteCommand
        {
            get {
                return new RelayCommand(() => navigationService.Navigate("AddNotePage"));
            }
        }


        public RelayCommand GoToNotesCommand => new RelayCommand(GoToNotesClick);

        private void GoToNotesClick()
        {
            App.Current.MainPage.Navigation.PushAsync(new NotesListPage());
        }

        public ICommand GoToCommand { get { return new RelayCommand<string>(GoTo); } }

        private void GoTo(string pageName)
        {
            navigationService.Navigate(pageName);
        }



    }
}
