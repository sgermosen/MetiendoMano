using Xamarin.Forms;

namespace Roulette
{
	public partial class RoulettePage : ContentPage
	{
		public RoulettePage()
		{
			InitializeComponent();

			this.BindingContext = new MenuClickModel();
		}
	}
}
