using ArtNews.Models;
using ArtNews.Services;
using ArtNews.ViewModels.Base;

namespace ArtNews.ViewModels
{
    public class AuthorViewModel : ViewModelBase
    {
        private Author _author;

        public AuthorViewModel()
        {
            LoadAuthorInfo();
        }

        public Author Author
        {
            get { return _author; }
            set
            {
                _author = value;
                OnPropertyChanged();
            }
        }

        private void LoadAuthorInfo()
        {
            Author = ArtService.Instance.GetAuthorInfo();
        }
    }
}