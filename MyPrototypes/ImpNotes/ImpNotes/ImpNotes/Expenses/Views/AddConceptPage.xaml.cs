using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImpNotes.Expenses.ViewModels;
using ImpNotes.Notes.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ImpNotes.Expenses.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddConceptPage : ContentPage
	{
        ConceptsViewModel viewModel;
        public AddConceptPage()
		{
			InitializeComponent ();
           // BindingContext = new ConceptsViewModel();
            BindingContext = viewModel = new ConceptsViewModel();
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            viewModel.SaveAdd();
            await Navigation.PopAsync();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}