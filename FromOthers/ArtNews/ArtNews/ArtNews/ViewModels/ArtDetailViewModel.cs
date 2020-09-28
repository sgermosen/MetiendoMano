using ArtNews.Models;
using ArtNews.ViewModels.Base;

namespace ArtNews.ViewModels
{
    public class ArtDetailViewModel : ViewModelBase
    {
        private ArtItem _artItem;

        public ArtDetailViewModel(object parameter)
        {
            if (parameter is ArtItem)
            {
                ArtItem = (ArtItem)parameter;
            }
        }

        public ArtItem ArtItem
        {
            get { return _artItem; }
            set
            {
                _artItem = value;
                OnPropertyChanged();
            }
        }
    }
}