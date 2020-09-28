using System;
using System.ComponentModel;

namespace CapitolTrackMobile
{
	public class ReportListViewModel : INotifyPropertyChanged
	{

		#region INotifyPropertyChanged implementation

		public event PropertyChangedEventHandler PropertyChanged;

		#endregion

		public string ReportName { get; set; }
		public string ReportDescricption { get; set; }
		public string ReportLastEditDate { get; set; }
		public ReportListViewModel()
		{
		}


	}
}

