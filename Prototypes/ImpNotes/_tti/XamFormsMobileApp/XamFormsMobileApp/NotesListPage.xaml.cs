using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamFormsMobileApp.Interface;
using XamFormsMobileApp.Model;

namespace XamFormsMobileApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotesListPage : ContentPage
    {
        public static bool ShouldShowAdd = true;

             
        NotesListPageViewModel viewModel;
        public NotesListPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new NotesListPageViewModel();
            viewModel.Initilize();
            ShouldShowAdd = true;



            //   BannerView.AdUnitId = Ads.AdConstant.UnitId;
        }

        


       
    }
}