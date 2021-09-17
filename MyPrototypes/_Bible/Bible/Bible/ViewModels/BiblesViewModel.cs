namespace Bible.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using Models;
    using Services;
    using Xamarin.Forms;

    public class BiblesViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        private BibleResponse bibleResponse;
        private ObservableCollection<BibleItemViewModel> bibles;
        private bool isRefreshing;
        #endregion

        #region Properties
        public ObservableCollection<BibleItemViewModel> Bibles
        {
            get { return this.bibles; }
            set { SetValue(ref this.bibles, value); }
        }

        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { SetValue(ref this.isRefreshing, value); }
        }
        #endregion

        #region Constructors
        public BiblesViewModel()
        {
            this.apiService = new ApiService();
            this.LoadBibles();
        }
        #endregion

        #region Methods
        private async void LoadBibles()
        {
            this.IsRefreshing = true;

            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(
                    "Error", 
                    connection.Message, 
                    "Accept");
                return;
            }

            var response = await this.apiService.Get<BibleResponse>(
                "http://api.biblesupersearch.com", 
                "/api", 
                "/bibles");
            
            if (!response.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Accept");
                return;
            }

            this.bibleResponse = (BibleResponse)response.Result;
            this.Bibles = new ObservableCollection<BibleItemViewModel>(
                this.ToBibleItemViewModel());
            this.IsRefreshing = false;
        }

        private IEnumerable<BibleItemViewModel> ToBibleItemViewModel()
        {
            return this.bibleResponse.Bibles.Select(b => new BibleItemViewModel
            {
                Copyright = b.Value.Copyright,
                Italics = b.Value.Italics,
                Lang = b.Value.Lang,
                LangShort = b.Value.LangShort,
                Module = b.Value.Module,
                Name = b.Value.Name,
                Rank = b.Value.Rank,
                Research = b.Value.Research,
                Shortname = b.Value.Shortname,
                Strongs = b.Value.Strongs,
                Year = b.Value.Year,
            });       
        }
        #endregion
    }
}