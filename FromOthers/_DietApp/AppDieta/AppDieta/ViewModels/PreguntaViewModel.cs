using System;
using System.Diagnostics;
using System.Threading.Tasks;

using AppDieta.Helpers;
using AppDieta.Models;
using AppDieta.Views;

using Xamarin.Forms;

namespace AppDieta.ViewModels
{
    public class PreguntaViewModel : BaseViewModel
    {
        public ObservableRangeCollection<Pregunta> Items { get; set; }
        public Command LoadItemsCommand { get; set; }

        public PreguntaViewModel()
        {
            Title = "Browse";
            Items = new ObservableRangeCollection<Pregunta>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NewPreguntaPage, Pregunta>(this, "AddItem", async (obj, item) =>
            {
                var _item = item as Pregunta;
                Items.Add(_item);
                await DataStorePregunta.AddItemAsync(_item);
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
                var items = await DataStorePregunta.GetItemsAsync(true);
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