using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using DontBeDoryApp.Clases;

namespace DontBeDoryApp.Paginas
{
	public partial class PaginaNota : ContentPage
	{
        public Notas Nota;

		public PaginaNota ()
		{
			InitializeComponent ();
		}

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            if (Nota != null)
                BindingContext = Nota;
        }

        void btnGuardar_Click(object sender, EventArgs a)
        {
            string nota = txtNota.Text;
            string cuerpo = txtCuerpo.Text;

            if (Nota.Id == "")
                App.AzureService.AgregarNota(nota, cuerpo);
            else
                App.AzureService.ModificarNota(Nota.Id, nota, cuerpo);

            Navigation.PopAsync();
        }

        void btnEliminar_Click(object sender, EventArgs a)
        {
            if (Nota.Id != "")
            {
                App.AzureService.EliminarNota(Nota.Id);
                Navigation.PopAsync();
            }
        }
    }
}
