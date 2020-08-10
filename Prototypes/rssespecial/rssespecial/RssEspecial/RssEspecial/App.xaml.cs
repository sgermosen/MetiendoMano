using RssEspecial.Locator;
using RssEspecial.Views;
using Xamarin.Forms;


namespace RssEspecial
{
    public partial class App : Application
    {
    
        private static ViewModelLocator _locator;
        public static ViewModelLocator Locator => _locator ?? (_locator = new ViewModelLocator());

        public App()
        {
            InitializeComponent();
            XF.Material.Forms.Material.Init(this);
            MainPage =new NavigationPage( new HomePage());
            //////
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
