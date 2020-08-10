namespace Bible.ViewModels
{
    public class MainViewModel
    {
        #region ViewModels
        public BiblesViewModel Bibles
        {
            get;
            set;
        }

        public BibleViewModel Bible
        {
            get;
            set;
        }

        public BookViewModel Book
        {
            get;
            set;
        }
        #endregion

        #region Properties
        public string SelectedModule
        {
            get;
            set;
        }
        #endregion

        #region Constructors
        public MainViewModel()
        {
            instance = this;

            this.Bibles = new BiblesViewModel();
        }
        #endregion

        #region Singleton
        private static MainViewModel instance;

        public static MainViewModel GetInstance()
        {
            if (instance == null)
            {
                return new MainViewModel();
            }

            return instance;
        }
        #endregion
    }
}