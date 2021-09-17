using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Xamarin.Forms;

namespace Carnavalwheel
{
    public class MenuClickModel : INotifyPropertyChanged
    {
        public MenuClickModel()
        {
        }

        public Command MenuButtonClicked
        {
            get
            {
                return new Command((obj) =>
                {
                    string param = (string)obj;

                    var roulettePanel = new RoulettePaneel(param);
                    Application.Current.MainPage.Navigation.PushAsync(roulettePanel);
                });
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
