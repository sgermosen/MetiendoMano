using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImpNotes.Interface;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ImpNotes.Notes.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NotesMainPage : ContentPage
	{
        NotesMainPageViewModel viewModel;
        public NotesMainPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new NotesMainPageViewModel();
            viewModel.Initilize();
            //////
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            DependencyService.Get<IAdInterstitial>().ShowAd();

        }
    }
}