namespace TestShell.Views
{
    using Xamarin.Forms.Xaml;
    using Xamarin.Essentials;
    using Xamarin.Forms;
    using Helpers;
    using Interfaces;
    using Models;
    using ViewModels;


    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TwitterPage : ContentPage, IPageHelpers
    {
        TwitterViewModel ViewModel => BindingContext as TwitterViewModel;

        public TwitterPage()
        {
            InitializeComponent();

            BindingContext = new TwitterViewModel();


            listView.ItemTapped += async (sender, args) =>
            {
                if (listView.SelectedItem == null)
                    return;


                var tweet = listView.SelectedItem as Tweet;

                //try to launch twitter or tweetbot app, else launch browser
                var launch = DependencyService.Get<ILaunchTwitter>();
                if (launch == null || !launch.OpenStatus(tweet.StatusID.ToString()))
                    await Browser.OpenAsync("http://twitter.com/shanselman/status/" + tweet.StatusID);

                listView.SelectedItem = null;
            };
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (DeviceInfo.Platform != DevicePlatform.UWP)
                OnPageVisible();
        }

        public void OnPageVisible()
        {
            if (ViewModel == null || !ViewModel.CanLoadMore || ViewModel.IsBusy || ViewModel.Tweets.Count > 0)
                return;

            ViewModel.LoadCommand.Execute(null);
        }
    }
}