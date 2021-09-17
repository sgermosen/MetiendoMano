using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImpNotes.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ImpNotes.Expenses.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ConceptDetailPage : ContentPage
	{
		public ConceptDetailPage (Concept concept)
		{
			InitializeComponent ();
           // BindingContext = new ConceptDetailViewModel();
        }
	}
}