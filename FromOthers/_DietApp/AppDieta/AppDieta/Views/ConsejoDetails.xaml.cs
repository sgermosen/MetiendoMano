
using AppDieta.Helpers;
using AppDieta.Models;
using AppDieta.ViewModels;
using AppDieta.Views;
using System;
using Xamarin.Forms;

namespace AppDieta
{
    public partial class ConsejoDetails : ContentPage
    {
        ConsejoDetailViewModel viewModel;

        // Note - The Xamarin.Forms Previewer requires a default, parameterless constructor to render a page.
        public ConsejoDetails()
        {
            InitializeComponent();
            BindingContext = viewModel = new ConsejoDetailViewModel();
        }

        public ConsejoDetails(ConsejoDetailViewModel viewModel)
        {
            InitializeComponent();
            //var selected = viewModels;
            BindingContext = this.viewModel = viewModel;
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as ConsejoDetalle;
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
