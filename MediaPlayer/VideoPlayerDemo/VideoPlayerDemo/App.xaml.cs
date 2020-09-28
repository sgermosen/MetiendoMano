using Xamarin.Forms;

namespace VideoPlayerDemo
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var btn = new Button { Text = "Click" };
            btn.Clicked += (s, e) =>
            {
                MainPage = new VideoPlayerDemo.MainPage();
            };
            var testPage = new ContentPage
            {
                Content = btn
            };

            //  MainPage = testPage; // new VideoPlayerDemo.MainPage();
            MainPage = new VideoPlayerDemo.MainPage();
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
