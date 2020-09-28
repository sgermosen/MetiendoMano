using System;
using System.Diagnostics;
using System.Threading.Tasks;

using AppDieta.Helpers;
using AppDieta.Models;
using AppDieta.Views;

using Xamarin.Forms;

namespace AppDieta.ViewModels
{
    public class CuidadoViewModel : BaseViewModel
    {
        public ObservableRangeCollection<Cuidados> Items { get; set; }
        public Command LoadItemsCommand { get; set; }

        public CuidadoViewModel()
        {
            Title = "Browse";
            Items = new ObservableRangeCollection<Cuidados>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NewItemPage, Cuidados>(this, "AddItem", async (obj, item) =>
            {
                var _item = item as Cuidados;
                Items.Add(_item);
                await DataStoreCuidados.AddItemAsync(_item);
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
                var items = await DataStoreCuidados.GetItemsAsync(true);
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