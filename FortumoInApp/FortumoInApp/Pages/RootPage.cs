using System;
using Xamarin.Forms;
using System.Linq;

using InApp.Menus;


namespace InApp.Pages
{
	public class RootPage : MasterDetailPage
	{
		MenuPage menuPage;

		public RootPage ()
		{
            MasterBehavior = MasterBehavior.Popover;
 
			menuPage = new MenuPage ();

			menuPage.Menu.ItemSelected += (sender, e) => NavigateTo (e.SelectedItem as MenuListItem);

			Master = menuPage;
			Detail = new NavigationPage (new ShopPage ());
		}

		void NavigateTo (MenuListItem menu)
		{
			if (menu == null)
				return;
			
			Page displayPage = (Page)Activator.CreateInstance (menu.TargetType);

			Detail = new NavigationPage (displayPage);

			menuPage.Menu.SelectedItem = null;
			IsPresented = false;
		}
	}
}