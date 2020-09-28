using System;
using System.Diagnostics;
using System.Threading.Tasks;

using AppDieta.Helpers;
using AppDieta.Models;
using AppDieta.Views;

using Xamarin.Forms;

namespace AppDieta.ViewModels
{
    public class AlimentoViewModel : BaseViewModel
    {
        public ObservableRangeCollection<Alimento> Items { get; set; }
        public Command LoadItemsCommand { get; set; }

        public AlimentoViewModel()
        {
            Title = "Browse";
            Items = new ObservableRangeCollection<Alimento>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NewItemPage, Alimento>(this, "AddItem", async (obj, item) =>
            {
                var _item = item as Alimento;
                Items.Add(_item);
                await DataStoreAlimento.AddItemAsync(_item);
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
                var items = await DataStoreAlimento.GetItemsAsync(true);
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