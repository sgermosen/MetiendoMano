using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

using InApp.Menus;


namespace InApp.Pages
{
    public partial class MenuPage : ContentPage
    {
        public ListView Menu {get; private set;}

        public MenuPage()
        {
            InitializeComponent();

            List<MenuListItem> data = new MenuListData();
            TheMenu.ItemsSource = data;

            Menu = TheMenu;
        }
    }
}
