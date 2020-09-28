using League.ViewModels;
using System;
using Xamarin.Forms;

namespace League.Views
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
        }

        void GoToStandings(object sender, EventArgs args)
        {
            Navigation.PushAsync(new StandingsPage(new StandingsViewModel()));
        }

        void ResetData(object sender, EventArgs args)
        {
            App.TeamService.Reset();
        }
    }
}
