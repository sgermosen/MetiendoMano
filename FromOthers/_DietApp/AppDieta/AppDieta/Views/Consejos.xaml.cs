
using AppDieta.Models;
using AppDieta.ViewModels;
using AppDieta.Views;
using System;
using Xamarin.Forms;
namespace AppDieta
{
    public partial class Consejos : ContentPage
    {
        ConsejoViewModel viewModel;
        public Consejos()
        {
            InitializeComponent();
            BindingContext = viewModel = new ConsejoViewModel();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Consejo;
            if (item == null)
                return;

            await Navigation.PushAsync(new ConsejoDetails(new ConsejoDetailViewModel(item)));

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
