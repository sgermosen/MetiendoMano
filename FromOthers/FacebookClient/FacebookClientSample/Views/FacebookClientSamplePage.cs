using FacebookClientSample.ViewModels;
using Xamarin.Forms;  

namespace FacebookClientSample
{
    public partial class FacebookClientSamplePage : ContentPage
    { 
        public FacebookClientSamplePage()
        {
			InitializeComponent();

			BindingContext = new ProfileDataViewModel();

            if(Device.RuntimePlatform == Device.iOS)
            {
                this.Padding = new Thickness(0, 20, 0, 0);
            } 
		}
    }
}
