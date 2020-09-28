
using AppDieta.Models;
using AppDieta.ViewModels;
using AppDieta.Views;
using System;
using Xamarin.Forms;

namespace AppDieta
{
    public partial class Recetas : ContentPage
    {
        RecetaViewModel viewModel;
        public Recetas()
        {
            InitializeComponent();
            BindingContext = viewModel = new RecetaViewModel();
        }


        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Receta;
            if (item == null)
                return;

            await Navigation.PushAsync(new RecetasDetails(new RecetaDetailsViewModel(item)));

            // Manually deselect item
            ItemsListView.SelectedItem = null;
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewItemPage());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Items.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }
    }
}
