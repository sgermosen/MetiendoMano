using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ImpNotes.Notes.Views;

namespace ImpNotes.Services
{
    public class NavigationService
    {
        public async  Task Navigate(string PageName)
        {
            // App.Master.IsPresented = false;

            switch (PageName)
            {
                case "NotesListPage":
                    await App.Navigator.PushAsync(new NotesListPage());
                    break;
                case "AddNotePage":
                   await App.Navigator.PushAsync(new AddNotePage());
                    break;
                case "MainPage":
                    await App.Navigator.PopToRootAsync();
                    break;
                default:
                    break;
            }
        }
    }

}
