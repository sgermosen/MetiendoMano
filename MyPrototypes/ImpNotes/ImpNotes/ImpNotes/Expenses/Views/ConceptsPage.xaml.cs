
using System;
using ImpNotes.Expenses.ViewModels;
using ImpNotes.Models;
using ImpNotes.Notes.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ImpNotes.Expenses.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ConceptsPage : ContentPage
	{
        ConceptsViewModel viewModel;
        public ConceptsPage ()
		{
			InitializeComponent ();
            BindingContext = viewModel = new ConceptsViewModel();
            viewModel.Initilize();
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync((new AddConceptPage()));
        }
        async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as Concept;
            if (item == null)
                return;

              await Navigation.PushAsync(new ConceptDetailPage((item)));

            //// Manually deselect item.
            ItemsListView.SelectedItem = null;

          
        }


    }
}