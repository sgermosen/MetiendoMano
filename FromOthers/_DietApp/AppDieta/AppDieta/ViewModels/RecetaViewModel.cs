using System;
using System.Diagnostics;
using System.Threading.Tasks;

using AppDieta.Helpers;
using AppDieta.Models;
using AppDieta.Views;

using Xamarin.Forms;

namespace AppDieta.ViewModels
{
    public class RecetaViewModel : BaseViewModel
    {
        public ObservableRangeCollection<Receta> Items { get; set; }
        public Command LoadItemsCommand { get; set; }

        public RecetaViewModel()
        {
            Title = "Browse";
            Items = new ObservableRangeCollection<Receta>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NewItemPage, Receta>(this, "AddItem", async (obj, item) =>
            {
                var _item = item as Receta;
                Items.Add(_item);
                await DataStoreReceta.AddItemAsync(_item);
            });
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStoreReceta.GetItemsAsync(true);
                Items.ReplaceRange(items);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                MessagingCenter.Send(new MessagingCenterAlert
                {
                    Title = "Error",
                    Message = "Unable to load items.",
                    Cancel = "OK"
                }, "message");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}