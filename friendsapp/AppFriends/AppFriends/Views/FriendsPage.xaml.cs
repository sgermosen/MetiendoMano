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
    public partial class FriendsPage : ContentPage
    {

        public FriendsPage()
        {
            InitializeComponent();
            ListViewFriends.ItemSelected += ListViewFriends_ItemSelected;
        }

        /* 
         O código verifica se existe um elemento selecionado e depois utiliza a API predefinida
            Navigation para colocar (push) uma nova página. Finalmente, o código remove a seleção do elemento.
        */
        private async void ListViewFriends_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var selectedFriend = e.SelectedItem as Friend;
            if (selectedFriend != null)
            {
                await Navigation.PushAsync(new Views.DetailsPage(selectedFriend));
                ListViewFriends.SelectedItem = null;
            }
        }
    }
}

