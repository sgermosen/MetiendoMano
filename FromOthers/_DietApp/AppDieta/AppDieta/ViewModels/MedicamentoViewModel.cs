using System;
using System.Diagnostics;
using System.Threading.Tasks;

using AppDieta.Helpers;
using AppDieta.Models;
using AppDieta.Views;

using Xamarin.Forms;

namespace AppDieta.ViewModels
{
    public class MedicamentoViewModel : BaseViewModel
    {
        public ObservableRangeCollection<Medicamento> Items { get; set; }
        public Command LoadItemsCommand { get; set; }

        public MedicamentoViewModel()
        {
            Title = "Browse";
            Items = new ObservableRangeCollection<Medicamento>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NewItemPage, Medicamento>(this, "AddItem", async (obj, item) =>
            {
                var _item = item as Medicamento;
                Items.Add(_item);
                await DataStoreMedicamento.AddItemAsync(_item);
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
                var items = await DataStoreMedicamento.GetItemsAsync(true);
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