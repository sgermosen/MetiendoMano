using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppFriends.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppFriends.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailsPage : ContentPage
    {
        Friend SelectedFriend;
        public DetailsPage(Friend selectedFriend)
        {
            InitializeComponent();

            this.SelectedFriend = selectedFriend;
            BindingContext = this.SelectedFriend;

            ButtonFacebookPage.Clicked += ButtonFacebookPage_Clicked;
        }

        private void ButtonFacebookPage_Clicked(object sender, EventArgs e)
        {
            if (SelectedFriend.FacebookPage.StartsWith("http"))
            {
                Device.OpenUri(new Uri(SelectedFriend.FacebookPage));
            }
        }
    }
}
