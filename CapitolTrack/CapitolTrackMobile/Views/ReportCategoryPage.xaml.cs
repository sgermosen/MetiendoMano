using System;
using System.ComponentModel;
using System.Collections.ObjectModel;


using Xamarin.Forms;

namespace CapitolTrackMobile
{
	public partial class ReportCategoryPage : ContentPage
	{

		public ObservableCollection<ReportListViewModel> reports { get; set; }
		public ReportCategoryPage()
		{
			reports = new ObservableCollection<ReportListViewModel>();
			InitializeComponent();
			reportList.ItemsSource = reports;
			reports.Add(new ReportListViewModel { ReportName = "All Bills", ReportDescricption = "Descricption will go here", ReportLastEditDate = "MM/DD/YYYY" });
			reports.Add(new ReportListViewModel { ReportName = "All Keyword Search Report", ReportDescricption = "Short One", ReportLastEditDate = "MM/DD/YYYY" });
			reports.Add(new ReportListViewModel { ReportName = "Daily Report", ReportDescricption = "This is a long descricption we'll see what happens when one is really long.", ReportLastEditDate = "MM/DD/YYYY" });
			reports.Add(new ReportListViewModel { ReportName = "Todays New Bills", ReportDescricption = "Descricption will go here", ReportLastEditDate = "MM/DD/YYYY" });
			reports.Add(new ReportListViewModel { ReportName = "Untracked Water Keyword Search", ReportDescricption = "Another one", ReportLastEditDate = "MM/DD/YYYY" });
			reports.Add(new ReportListViewModel { ReportName = "Need Attention", ReportDescricption = "Stuff Here", ReportLastEditDate = "MM/DD/YYYY" });
			reports.Add(new ReportListViewModel { ReportName = "Bill Watch List", ReportDescricption = "Descricption will go here", ReportLastEditDate = "MM/DD/YYYY" });
			reports.Add(new ReportListViewModel { ReportName = "All Bills", ReportDescricption = "Descricption will go here", ReportLastEditDate = "MM/DD/YYYY" });
			reports.Add(new ReportListViewModel { ReportName = "All Keyword Search Report", ReportDescricption = "Short One", ReportLastEditDate = "MM/DD/YYYY" });
			reports.Add(new ReportListViewModel { ReportName = "Daily Report", ReportDescricption = "This is a long descricption we'll see what happens when one is really long.", ReportLastEditDate = "MM/DD/YYYY" });
			reports.Add(new ReportListViewModel { ReportName = "Todays New Bills", ReportDescricption = "Descricption will go here", ReportLastEditDate = "MM/DD/YYYY" });
			reports.Add(new ReportListViewModel { ReportName = "Untracked Water Keyword Search", ReportDescricption = "Another one", ReportLastEditDate = "MM/DD/YYYY" });
			reports.Add(new ReportListViewModel { ReportName = "Need Attention", ReportDescricption = "Stuff Here", ReportLastEditDate = "MM/DD/YYYY" });
			reports.Add(new ReportListViewModel { ReportName = "Bill Watch List", ReportDescricption = "Descricption will go here", ReportLastEditDate = "MM/DD/YYYY" });
		}
	}
}