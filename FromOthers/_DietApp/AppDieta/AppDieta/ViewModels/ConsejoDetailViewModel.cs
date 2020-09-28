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

    public class ConsejoDetailViewModel : BaseViewModel
    {
        public ObservableRangeCollection<ConsejoDetalle> Items { get; set; }
        public Command LoadItemsCommand { get; set; }


        public Item Item { get; set; }

        public Alimento Alimento { get; set; }
        public Consejo Consejo { get; set; }
        public Cuidados Cuidados { get; set; }
        public Medicamento Medicamento { get; set; }
        public Pregunta Pregunta { get; set; }
        public Receta Receta { get; set; }
        public ConsejoDetalle ConsejoDetalle { get; set; }



        public ConsejoDetailViewModel()
        {
            Title = "Browse";
            Items = new ObservableRangeCollection<ConsejoDetalle>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NewItemPage, ConsejoDetalle>(this, "AddItem", async (obj, item) =>
            {
                var _item = item as ConsejoDetalle;
                Items.Add(_item);
                await DataStoreConsejoDetalle.AddItemAsync(_item);
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
                var items = await DataStoreConsejoDetalle.GetItemsAsync(true);
                foreach (var item in items)
                {
                    if (item.Concejo == Title)
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

        public ConsejoDetailViewModel(Item item = null)
        {
            Title = item.Text;
            Item = item;
        }

        public ConsejoDetailViewModel(Receta item = null)
        {
            this.Item = new Item { Text = item.Text, Description = item.Description };
            Items = new ObservableRangeCollection<ConsejoDetalle>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            Title = item.Text;
            Receta = item;
        }
        public ConsejoDetailViewModel(Alimento item = null)
        {
            this.Item = new Item { Text = item.Text, Description = item.Description };
            Title = item.Text;
            Alimento = item;
        }
        public ConsejoDetailViewModel(Consejo item = null)
        {
            this.Item = new Item { Text = item.Text, Description = item.Description };
            Items = new ObservableRangeCollection<ConsejoDetalle>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            Title = item.Text;
            Consejo = item;
        }
        public ConsejoDetailViewModel(Cuidados item = null)
        {
            this.Item = new Item { Text = item.Text, Description = item.Description };
            Title = item.Text;
            Cuidados = item;
        }

        public ConsejoDetailViewModel(Pregunta item = null)
        {
            this.Item = new Item { Text = item.Text, Description = item.Description };
            Title = item.Text;
            Pregunta = item;
        }

        public ConsejoDetailViewModel(Medicamento item = null)
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