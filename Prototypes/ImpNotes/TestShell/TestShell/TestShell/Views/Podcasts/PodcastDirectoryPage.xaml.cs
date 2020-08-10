using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestShell.Interfaces;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestShell.Views.Podcasts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Controls;
    using Interfaces;
    using Models;
    using ViewModels;
    using Views.Podcasts;
    using Xamarin.Essentials;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PodcastDirectoryPage : ContentPage, IPageHelpers
    {
        PodcastDirectoryViewModel VM { get; }
        public PodcastDirectoryPage()
        {
            InitializeComponent();
            VM = (PodcastDirectoryViewModel)BindingContext;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (DeviceInfo.Platform != DevicePlatform.UWP)
                OnPageVisible();

        }

        public void OnPageVisible()
        {
            if (VM.IsBusy || VM.Podcasts.Count > 0)
                return;

            VM.LoadPodcastsCommand.Execute(null);
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            if (sender is View view && view.BindingContext is Podcast podcast)
            {
               // await Navigation.PushAsync(new PodcastDetailsPage(podcast));
            }
        }
    }
}