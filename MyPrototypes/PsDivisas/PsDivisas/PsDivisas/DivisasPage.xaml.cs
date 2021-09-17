using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace PsDivisas
{
    public partial class DivisasPage : ContentPage
    {
        public DivisasPage()
        {
            InitializeComponent();
        }

        public void calcularButtonClicked(Object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(pesosEntry.Text))
            {
                mensajeLabel.Text = "Debe ingresar un valor en peso";
                pesosEntry.Focus();
                return;
                            }
            decimal Pesos = decimal.Parse(pesosEntry.Text);
            dolaresEntry.Text = string.Format("US${0:N2}", Conversion.ToDolares(Pesos));
            eurosEntry.Text = string.Format("E${0:N2}", Conversion.ToEuros(Pesos));
            librasEntry.Text = string.Format("L${0:N2}", Conversion.ToLibras(Pesos));



        }
    }
}
