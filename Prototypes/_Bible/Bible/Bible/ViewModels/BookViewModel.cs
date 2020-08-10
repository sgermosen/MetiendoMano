namespace Bible.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Reflection;
    using Models;
    using Services;
    using Xamarin.Forms;

    public class BookViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        private Book book;
        private bool isRefreshing;
        private ContentResponse contentResponse;
        private ObservableCollection<Verse> verses;
        #endregion

        #region Properties
        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { SetValue(ref this.isRefreshing, value); }
        }

        public ObservableCollection<Verse> Verses
        {
            get { return this.verses; }
            set { SetValue(ref this.verses, value); }
        }
        #endregion

        #region Constructors
        public BookViewModel(Book book)
        {
            this.apiService = new ApiService();
            this.book = book;
            this.LoadContent();
        }
        #endregion

        #region Methods
        private async void LoadContent()
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

            var response = await this.apiService.Get<ContentResponse>(
                "http://api.biblesupersearch.com",
                "/api",
                string.Format(
                    "?bible={0}&reference={1}", 
                    MainViewModel.GetInstance().SelectedModule, 
                    this.book.Shortname));

            if (!response.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Accept");
                return;
            }

            this.contentResponse = (ContentResponse)response.Result;
            this.IsRefreshing = false;

            var contentResult = contentResponse.Contents[0];

            var type = typeof(Verses);
            var properties = type.GetRuntimeFields();
            Bible bible = null;

            foreach (var property in properties)
            {
                bible = (Bible)property.GetValue(contentResult.Verses);
                if (bible != null)
                {
                    break;
                }
            }

            if (bible == null)
            {
                return;
            }

            type = typeof(Bible);
            properties = type.GetRuntimeFields();
            Dictionary<string, Verse> chapter = null;

            foreach (var property in properties)
            {
                if (property.Name.StartsWith("<Chapter"))
                {
                    chapter = (Dictionary<string, Verse>)property.GetValue(bible);

                    if (chapter != null)
                    {
                        break;
                    }                
                }
            }

            var myVerses = chapter.Select(v => new Verse
            {
                Book = v.Value.Book,
                Chapter = v.Value.Chapter,
                Id = v.Value.Id,
                Italics = v.Value.Italics,
                Text = v.Value.Text,
                VerseNumber = v.Value.VerseNumber,
            });      

            this.Verses = new ObservableCollection<Verse>(myVerses);
        }
        #endregion
    }
}