namespace Bible.ViewModels
{
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Models;
    using Views;
    using Xamarin.Forms;

    public class BibleItemViewModel : Bible
    {
        #region Commands
        public ICommand SelectBibleCommand
        {
            get
            {
                return new RelayCommand(SelectBible);
            }
        }

        private async void SelectBible()
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.Bible = new BibleViewModel(this);
            mainViewModel.SelectedModule = Module;
            await Application.Current.MainPage.Navigation.PushAsync(new BiblePage());
        }
        #endregion
    }
}