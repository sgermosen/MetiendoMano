using AppDieta.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppDieta.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public Item Item { get; set; }

        public Alimento Alimento { get; set; }
        public Consejo Consejo { get; set; }
        public Cuidados Cuidados { get; set; }
        public Medicamento Medicamento { get; set; }
        public Pregunta Pregunta { get; set; }
        public Receta Receta { get; set; }
        public RecetaDetails RecetaDetails { get; set; }

        public ConsejoDetalle ConsejoDetalle { get; set; }

        public ItemDetailViewModel(Item item = null)
        {
            Title = item.Text;
            Item = item;
        }

        public ItemDetailViewModel(Receta item = null)
        {
            this.Item = new Item { Text = item.Text, Description = item.Description };
            Title = item.Text;
            Receta = item;
        }

        public ItemDetailViewModel(RecetaDetails item = null)
        {
            this.Item = new Item { Text = item.Text, Description = item.Description };
            Title = item.Text;
            RecetaDetails = item;
        }


        public ItemDetailViewModel(ConsejoDetalle item = null)
        {
            this.Item = new Item { Text = item.Text, Description = item.Description };
            Title = item.Text;
            ConsejoDetalle = item;
        }
        public ItemDetailViewModel(Alimento item = null)
        {
            this.Item = new Item { Text = item.Text, Description = item.Description };
            Title = item.Text;
            Alimento = item;
        }
        public ItemDetailViewModel(Consejo item = null)
        {
            this.Item = new Item { Text = item.Text, Description = item.Description };
            Title = item.Text;
            Consejo = item;
        }
        public ItemDetailViewModel(Cuidados item = null)
        {
            this.Item = new Item { Text = item.Text, Description = item.Description };
            Title = item.Text;
            Cuidados = item;
        }

        public ItemDetailViewModel(Pregunta item = null)
        {
            this.Item = new Item { Text = item.Text, Description = item.Description };
            Title = item.Text;
            Pregunta = item;
        }

        public ItemDetailViewModel(Medicamento item = null)
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