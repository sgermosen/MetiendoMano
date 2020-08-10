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
	public partial class PodcastPage : ContentPage
	{
        PodcastViewModel ViewModel => BindingContext as PodcastViewModel;


        public PodcastPage(MenuType item)
        {
            InitializeComponent();
            BindingContext = new PodcastViewModel(item);

            listView.ItemTapped += (sender, args) =>
            {
                if (listView.SelectedItem == null)
                    return;
                Navigation.PushAsync(new PodcastPlaybackPage
                    (listView.SelectedItem as FeedItem));
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