using Xamarin.Forms;
using System.Collections.Generic;
using System.Collections;

namespace CapitolTrackMobile
{
	public partial class SearchPage : ContentPage
	{
		public SearchPage()
		{
			InitializeComponent();
			searchListView.ItemsSource = new List<BillSearchRow>(){
				new BillSearchRow() { Measure = "AB 1", Author = "Eldridge", Topic = "Guns and Ammo"},
				new BillSearchRow() { Measure = "AB 2", Author = "Smith", Topic = "Drugs"},
				new BillSearchRow() { Measure = "SB 120", Author = "Jackson", Topic = "Education"}
			};

		}
	}

	public class BillSearchRow
	{
		public string Measure { get; set; }
		public string Author { get; set; }
		public string Topic { get; set; }

	}
}

