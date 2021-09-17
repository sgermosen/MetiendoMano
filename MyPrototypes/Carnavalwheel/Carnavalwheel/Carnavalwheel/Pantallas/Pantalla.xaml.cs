using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Carnavalwheel.Pantallas
{
    public partial class Pantalla : ContentPage
    {
        public Pantalla()
        {
            InitializeComponent();
            btnClick.Clicked += BtnClick_Clicked;
        }

        private void BtnClick_Clicked(object sender, EventArgs e)
        {
            txt2.Text = txt1.Text;
        }
    }
}
