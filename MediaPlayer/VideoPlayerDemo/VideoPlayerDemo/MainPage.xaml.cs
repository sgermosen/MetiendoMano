using System;
using Xamarin.Forms;

namespace VideoPlayerDemo
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void VideoPlayer_Error(object sender, Xamarians.MediaPlayer.PlayerErrorEventArgs e)
        {
            await DisplayAlert("Error", e.Message, "OK");
        }

        private async void VideoPlayer_Prepared(object sender, EventArgs e)
        {
            await DisplayAlert("", "Prepared", "OK");
        }

        private async void VideoPlayer_Completed(object sender, EventArgs e)
        {
            await DisplayAlert("", "Completed", "OK");

        }
    }
}
