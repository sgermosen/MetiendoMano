using System;
using Xamarin.Forms;
using System.Collections.Generic;

using InApp.Pages;


namespace InApp.Menus
{
	public class MenuListData : List<MenuListItem>
	{
		public MenuListData ()
		{
			this.Add (new MenuListItem () { 
				Title = "Shop", 
				IconSource = "shop.png", 
				TargetType = typeof(ShopPage)
			});

			this.Add (new MenuListItem () { 
				Title = "Purchases", 
				IconSource = "purchases.png", 
				TargetType = typeof(PurchasesPage)
			});
		}
	}
}