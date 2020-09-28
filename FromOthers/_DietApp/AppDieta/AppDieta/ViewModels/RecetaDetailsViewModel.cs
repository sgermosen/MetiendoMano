using System;
using System.Diagnostics;
using System.Threading.Tasks;

using AppDieta.Helpers;
using AppDieta.Models;
using AppDieta.Views;

using Xamarin.Forms;
using System.Collections.Generic;

namespace AppDieta.ViewModels
{

    public class RecetaDetailsViewModel : BaseViewModel
    {
        public ObservableRangeCollection<RecetaDetails> Items { get; set; }
        public Command LoadItemsCommand { get; set; }


        public Item Item { get; set; }

        public Alimento Alimento { get; set; }
        public Consejo Consejo { get; set; }
        public Cuidados Cuidados { get; set; }
        public Medicamento Medicamento { get; set; }
        public Pregunta Pregunta { get; set; }
        public Receta Receta { get; set; }
        public RecetaDetails RecetaDetails { get; set; }



        public RecetaDetailsViewModel()
        {
            Title = "Browse";
            Items = new ObservableRangeCollection<RecetaDetails>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NewItemPage, RecetaDetails>(this, "AddItem", async (obj, item) =>
            {
                var _item = item as RecetaDetails;
                Items.Add(_item);
                await DataStoreRecetaDetalle.AddItemAsync(_item);
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
                var items = await DataStoreRecetaDetalle.GetItemsAsync(true);
                foreach (var item in items)
                {
                    if (item.Receta == Title)
                    {
                        Items.Add(item);
                    }
                }
                
                
                
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

        public RecetaDetailsViewModel(Item item = null)
        {
            Title = item.Text;
            Item = item;
        }

        public RecetaDetailsViewModel(Receta item = null)
        {
            this.Item = new Item { Text = item.Text, Description = item.Description };
            Items = new ObservableRangeCollection<RecetaDetails>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            Title = item.Text;
            Receta = item;
        }
        public RecetaDetailsViewModel(Alimento item = null)
        {
            this.Item = new Item { Text = item.Text, Description = item.Description };
            Title = item.Text;
            Alimento = item;
        }
        public RecetaDetailsViewModel(Consejo item = null)
        {
            this.Item = new Item { Text = item.Text, Description = item.Description };
            Title = item.Text;
            Consejo = item;
        }
        public RecetaDetailsViewModel(Cuidados item = null)
        {
            this.Item = new Item { Text = item.Text, Description = item.Description };
            Title = item.Text;
            Cuidados = item;
        }

        public RecetaDetailsViewModel(Pregunta item = null)
        {
            this.Item = new Item { Text = item.Text, Description = item.Description };
            Title = item.Text;
            Pregunta = item;
        }

        public RecetaDetailsViewModel(Medicamento item = null)
        {
            this.Item = new Item { Text = item.Text, Description = item.Description };
            Title = item.Text;
            Medicamento = item;
        }


        int quantity = 1;
        public int Quantity
        {
            get { return quantity; }
            set { SetProperty(ref quantity, value); }
        }
    }
}