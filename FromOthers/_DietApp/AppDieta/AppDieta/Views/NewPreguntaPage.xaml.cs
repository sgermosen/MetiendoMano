using System;

using AppDieta.Models;

using Xamarin.Forms;

namespace AppDieta.Views
{
    public partial class NewPreguntaPage : ContentPage
    {
        public Pregunta Item { get; set; }

        public NewPreguntaPage()
        {
            InitializeComponent();

            Item = new Pregunta
            {
                Text = "Nombre de la pregunta",
                Description = "Descripción de la pregunta"
            };

            BindingContext = this;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddItem", Item);
            await Navigation.PopToRootAsync();
        }
    }
}