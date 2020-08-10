using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestShell.Models;
using TestShell.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestShell.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Channel9VideosPage : ContentPage
	{
        Channel9VideosViewModel ViewModel => BindingContext as Channel9VideosViewModel;


        public Channel9VideosPage()
        {
            InitializeComponent();
            BindingContext = new Channel9VideosViewModel();

            listView.ItemTapped += (sender, args) =>
            {
                if (listView.SelectedItem == null)
                    return;
                Navigation.PushAsync(new Channel9VideoPlaybackPage
                    (listView.SelectedItem as VideoFeedItem));
                listView.SelectedItem = null;
            };
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (ViewModel == null || !ViewModel.CanLoadMore || ViewModel.IsBusy || ViewModel.FeedItems.Count > 0)
                return;

            ViewModel.LoadItemsCommand.Execute(null);
        }
    }
}