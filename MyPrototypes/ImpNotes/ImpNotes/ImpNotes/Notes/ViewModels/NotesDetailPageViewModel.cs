using GalaSoft.MvvmLight.Command;
using ImpNotes.Models;
using ImpNotes.Notes.Views;

namespace ImpNotes.Notes.ViewModels
{
    public class NotesDetailPageViewModel : NotesBaseViewModel
    {
        public RelayCommand NavigateCommand => new RelayCommand(NavigateClick);

        private void NavigateClick()
        {
            App.Current.MainPage.Navigation.PushAsync(new TextToImagePage(
                new NotesModel()
                { Text = Text, TextColor = SelectedTextColor }));
        }

        public void Initilize(NotesModel model)
        {
            Text = model.Text;
            Title = model.Title;
            SelectedTextColor = model.TextColor;

            //ColorNames.Clear();

            //foreach (var field in typeof(Xamarin.Forms.Color).GetFields(BindingFlags.Static | BindingFlags.Public))
            //{
            //    if (field != null && !String.IsNullOrEmpty(field.Name))
            //        ColorNames.Add(field.Name);
            //}

            //SelectedTextColor = "Black";
            IsEnabled = true;
        }

        public NotesDetailPageViewModel(NotesModel model)
        {
            Text = model.Text;
            Title = model.Title;
            SelectedTextColor = model.TextColor;
        }
    }
}
