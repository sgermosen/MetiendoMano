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
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Interfaces;
    using Models;
    using ViewModels;
    using Xamarin.Forms;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BlogPage : ContentPage, IPageHelpers
    {
        BlogFeedViewModel viewModel;
        BlogFeedViewModel ViewModel => viewModel ?? (viewModel = BindingContext as BlogFeedViewModel);


        public BlogPage()
        {
            InitializeComponent();
            BindingContext = new BlogFeedViewModel();

            listView.ItemTapped += (sender, args) =>
            {
                if (listView.SelectedItem == null)
                    return;
                ViewModel.BlogSelectedCommand.Execute(null);
                listView.SelectedItem = null;
            };
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        public void OnPageVisible()
        {
            if (ViewModel == null || !ViewModel.CanLoadMore || ViewModel.IsBusy || ViewModel.FeedItems.Count > 0)
                return;

            ViewModel.LoadCommand.Execute(null);
        }
    }
}