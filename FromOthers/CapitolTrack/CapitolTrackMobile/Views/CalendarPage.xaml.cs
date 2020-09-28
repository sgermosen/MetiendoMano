using System;
using Xamarin.Forms;

namespace CapitolTrackMobile
{
	public partial class CalendarPage : ContentPage
	{
		public CalendarPage()
		{
			InitializeComponent();
		}

		async void OnUpcomingAppointmentsButtonClicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new UpcomingAppointmentsPage());
		}
	}
}

