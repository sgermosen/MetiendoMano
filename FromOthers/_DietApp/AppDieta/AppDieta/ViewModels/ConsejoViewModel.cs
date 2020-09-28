using System;
using System.Diagnostics;
using System.Threading.Tasks;

using AppDieta.Helpers;
using AppDieta.Models;
using AppDieta.Views;

using Xamarin.Forms;

namespace AppDieta.ViewModels
{
    public class ConsejoViewModel : BaseViewModel
    {
        public ObservableRangeCollection<Consejo> Items { get; set; }
        public Command LoadItemsCommand { get; set; }

        public ConsejoViewModel()
        {
            Title = "Browse";
            Items = new ObservableRangeCollection<Consejo>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NewItemPage, Consejo>(this, "AddItem", async (obj, item) =>
            {
                var _item = item as Consejo;
                Items.Add(_item);
                await DataStoreConsejo.AddItemAsync(_item);
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
                var items = await DataStoreConsejo.GetItemsAsync(true);
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