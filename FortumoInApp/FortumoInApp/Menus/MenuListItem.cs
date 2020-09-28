using System;
using Xamarin.Forms;
using System.Collections.Generic;

namespace InApp.Menus
{
	public class MenuListItem
	{
		public string Title { get; set; }

		public string IconSource { get; set; }

		public Type TargetType { get; set; }
	}
}