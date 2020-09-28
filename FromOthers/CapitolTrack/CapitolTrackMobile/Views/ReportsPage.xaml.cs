using System;
using Xamarin.Forms;
using System.Collections.Generic;

namespace CapitolTrackMobile
{
	public partial class ReportsPage : ContentPage
	{
		public ReportsPage()
		{
			InitializeComponent();
			listView.ItemsSource = new List<ReportCategoryList>(){
				new ReportCategoryList() { ReportCategoryTitle = "All Office Reports"},
				new ReportCategoryList() { ReportCategoryTitle = "Amanda Browne"},
				new ReportCategoryList() { ReportCategoryTitle = "Andrew Ulmer"},
				new ReportCategoryList() { ReportCategoryTitle = "Ariel Soriano"},
				new ReportCategoryList() { ReportCategoryTitle = "Ashley Martin"},
				new ReportCategoryList() { ReportCategoryTitle = "Bills By Due Date"},
				new ReportCategoryList() { ReportCategoryTitle = "Bill Count"},
				new ReportCategoryList() { ReportCategoryTitle = "Brad"},
				new ReportCategoryList() { ReportCategoryTitle = "Chad"},
				new ReportCategoryList() { ReportCategoryTitle = "Committees"},
				new ReportCategoryList() { ReportCategoryTitle = "Committees By Room"},
				new ReportCategoryList() { ReportCategoryTitle = "Dan"},
				new ReportCategoryList() { ReportCategoryTitle = "Frank"},
				new ReportCategoryList() { ReportCategoryTitle = "Gary"},
			};

		}

		async void ReportCategoryClicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new ReportCategoryPage());
		}

	}


	public class ReportCategoryList
	{
		public string ReportCategoryTitle { get; set; }

	}
}

