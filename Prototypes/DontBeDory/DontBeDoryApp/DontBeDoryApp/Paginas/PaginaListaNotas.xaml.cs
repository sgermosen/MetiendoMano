using System;
using Xamarin.Forms;
using DontBeDoryApp.Clases;

namespace DontBeDoryApp.Paginas
{
	public partial class PaginaListaNotas : ContentPage
	{
		public PaginaListaNotas ()
		{
			InitializeComponent ();
		}

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            actInd.IsRunning = true;
            lsvNotas.ItemsSource = await App.AzureService.ObtenerNotas();
            actInd.IsRunning = false;
        }

        private void lsvNotas_Selected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                PaginaNota pagina = new PaginaNota();
                pagina.Nota = e.SelectedItem as Notas;
                Navigation.PushAsync(pagina);
            }
        }

        void btnNuevo_Click(object sender, EventArgs a)
        {
            PaginaNota pagina = new PaginaNota();
            pagina.Nota = new Notas() { Id = "" };
            Navigation.PushAsync(pagina);
        }
    }
}
