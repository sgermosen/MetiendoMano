using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamFormsMobileApp.Model;

namespace XamFormsMobileApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TextToImagePage : ContentPage
	{
        TextToImagePageViewModel viewModel;
        public TextToImagePage (NotesModel notes)
		{
			InitializeComponent ();
            BindingContext = viewModel = new TextToImagePageViewModel();
            viewModel.Initilize(notes);
		}

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }
    }
}