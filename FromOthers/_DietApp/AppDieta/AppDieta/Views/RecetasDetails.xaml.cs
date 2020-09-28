
using AppDieta.Helpers;
using AppDieta.Models;
using AppDieta.ViewModels;
using AppDieta.Views;
using System;
using Xamarin.Forms;

namespace AppDieta
{
    public partial class RecetasDetails : ContentPage
    {
        RecetaDetailsViewModel viewModel;

        // Note - The Xamarin.Forms Previewer requires a default, parameterless constructor to render a page.
        public RecetasDetails()
        {
            InitializeComponent();
            BindingContext = viewModel = new RecetaDetailsViewModel();
        }

        public RecetasDetails(RecetaDetailsViewModel viewModel)
        {
            InitializeComponent();
            //var selected = viewModels;
            BindingContext = this.viewModel = viewModel;
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as RecetaDetails;
            if (item == null)
                return;

            await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(item)));

            // Manually deselect item
            ItemsListView.SelectedItem = null;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Items.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }

    }
}
