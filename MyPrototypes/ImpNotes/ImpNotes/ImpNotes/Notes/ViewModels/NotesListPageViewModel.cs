using ImpNotes.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using ImpNotes.Notes.ViewModels;
using ImpNotes.Notes.Views;
using ImpNotes.Services;
using ImpNotes.ViewModels;
using Xamarin.Forms;

namespace ImpNotes.Notes.ViewModels
{
    public class NotesListPageViewModel : NotesBaseViewModel
    {
        private NavigationService navigationService;

        
        private ObservableCollection<NotesModel> _notesList;
        public ObservableCollection<NotesModel> NotesList
        {
            get => _notesList;
            set {
                _notesList = value;
                RaisePropertyChanged();
            }
        }

        public ICommand AddNoteCommand
        {
            get {
                return new RelayCommand(() => navigationService.Navigate("AddNotePage"));
            }
        }

        public ICommand AddNoteCommand2 => new RelayCommand(this.GoToAddNote);

        private async void GoToAddNote()
        {
            //this.AddNote = new AddNoteViewModel();

            //await Application.Current.MainPage.Navigation.PushAsync(new AddNotePage());
          //  await App.Navigator.PushAsync(new NavigationPage(new AddNotePage()));
            await navigationService.Navigate("AddNotePage");
            //  await Navigation.PushModalAsync(new NavigationPage(new Page1()));
            //  await App.Navigator.PushAsync(new AddReactionPage());
        }



        public AddNoteViewModel AddNote { get; set; }

        //internal void DeleteNotes(NotesModel notesModel)
        //{
        //    NotesList.Remove(notesModel);
        //    RaisePropertyChanged();
        //}

        public void Initilize()
        {
            try
            {
                var list = new DataService().GetList<NotesModel>(false);

                NotesList = new ObservableCollection<NotesModel>(list);
            }
            catch (Exception ex)
            {
                NotesList = new ObservableCollection<NotesModel>();
            }

        }

        public Command LoadItemsCommand { get; set; }

        public NotesListPageViewModel()
        {
            navigationService = new NavigationService();
            //Title = "Browse";
            //Items = new ObservableCollection<Item>();
            //LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            //MessagingCenter.Subscribe<AddNotePage, NotesModel>(this, "AddItem", async (obj, item) =>
            //{
            //    var newItem = item as NotesModel;
            //    NotesList.Add(newItem);
            //  //  await DataStore.AddItemAsync(newItem);
            //});
        }



        //async Task ExecuteLoadItemsCommand()
        //{
        //    if (IsBusy)
        //        return;

        //    IsBusy = true;

        //    try
        //    {
        //        Items.Clear();
        //        var items = await DataStore.GetItemsAsync(true);
        //        foreach (var item in items)
        //        {
        //            Items.Add(item);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine(ex);
        //    }
        //    finally
        //    {
        //        IsBusy = false;
        //    }
        //}

        internal void Navigate(NotesModel notesModel)
        {
            //   App.Current.MainPage.Navigation.PushAsync(new NotesDetailPage(notesModel));
        }
    }
}
