using System;
using System.Threading.Tasks;
using ImpNotes.Interface;
using ImpNotes.Models;
using ImpNotes.Notes.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ImpNotes.Notes.Views
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

            ////// //   BannerView.AdUnitId = Ads.AdConstant.UnitId;
        }

        //async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        //{
        //    var item = args.SelectedItem as Item;
        //    if (item == null)
        //        return;

        //   // await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(item)));

        //    // Manually deselect item.
        //    ItemsListView.SelectedItem = null;
        //}

        //async void AddItem_Clicked(object sender, EventArgs e)
        //{
        //    await Navigation.PushAsync((new AddNotePage()));
        //}

        protected override async void OnAppearing()
        {
            viewModel.Initilize();
            base.OnAppearing();

            // viewModel.LoadItemsCommand.Execute(null);
            //if (viewModel.Items.Count == 0)
            //    viewModel.LoadItemsCommand.Execute(null);
            while (true)
            {
                if (ShouldShowAdd)
                {
                    DependencyService.Get<IAdInterstitial>().ShowAd();
                    await Task.Delay(100);

                }
                else
                {
                    break;
                }

            };
        }

        //private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        //{
        //    if (sender is Label label)
        //    {
        //        if (label.BindingContext is NotesModel notesModel)
        //        {
        //            // viewModel.DeleteNotes(notesModel);
        //            viewModel.Navigate(notesModel);

        //        }
        //    }

        //    if (sender is StackLayout stackLayout)
        //    {
        //        if (stackLayout.BindingContext is NotesModel notesModel)
        //        {
        //            // viewModel.DeleteNotes(notesModel);
        //            viewModel.Navigate(notesModel);

        //        }
        //    }
        //}

        //async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        //{
        //    var item = e.SelectedItem as NotesModel;
        //    if (item == null)
        //        return;

        //    //await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(item)));
        //    await Navigation.PushAsync(new NotesDetailPage((item)));

        //    //// Manually deselect item.
        //    ItemsListView.SelectedItem = null;

        //    //var listView = (ListView)sender;
        //    //if (listView.SelectedItem == null) return;

        //    //if (listView.SelectedItem is NotesModel notesModel)
        //    //{
        //    //    // viewModel.DeleteNotes(notesModel);
        //    //    viewModel.Navigate(notesModel);
        //    //    await Navigation.PushAsync(new NotesDetailPage(notesModel));
        //    //}

        //     //listView.SelectedItem = null;
        //}




    }
}