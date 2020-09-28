
using AppDieta.Models;
using AppDieta.ViewModels;
using AppDieta.Views;
using System;
using Xamarin.Forms;

namespace AppDieta
{
    public partial class Medicamentos : ContentPage
    {
        MedicamentoViewModel viewModel;
        public Medicamentos()
        {
            InitializeComponent();
            BindingContext = viewModel = new MedicamentoViewModel();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Medicamento;
            if (item == null)
                return;

            await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(item)));

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
