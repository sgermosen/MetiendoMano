namespace Bible.ViewModels
{
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Models;
    using Views;
    using Xamarin.Forms;

    public class BookItemViewModel : Book
    {
        #region Commands
        public ICommand SelectBookCommand
        {
            get
            {
                return new RelayCommand(SelectBook);
            }
        }

        private async void SelectBook()
        {
            MainViewModel.GetInstance().Book = new BookViewModel(this);
            await Application.Current.MainPage.Navigation.PushAsync(new BookPage());
        }
        #endregion
    }
}