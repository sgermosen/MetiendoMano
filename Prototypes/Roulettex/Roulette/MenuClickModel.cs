using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace Roulette
{
	public class MenuClickModel: INotifyPropertyChanged
	{
		public MenuClickModel()
		{
		}

		public Command MenuButtonClicked
		{
			get {
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
