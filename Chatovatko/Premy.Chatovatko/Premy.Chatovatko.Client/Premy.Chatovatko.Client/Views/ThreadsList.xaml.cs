using Premy.Chatovatko.Client.Libs.Database.Models;
using Premy.Chatovatko.Client.Libs.Sync;
using Premy.Chatovatko.Client.Libs.UserData;
using Premy.Chatovatko.Client.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Premy.Chatovatko.Client.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ThreadsList : ContentPage, IUpdatable
    {
        private SettingsCapsula settings;
        private App app;
        private ThreadsViewModel model;

        public ThreadsList(App app, SettingsCapsula settings)
        {
            InitializeComponent();
            this.settings = settings;
            this.app = app;
            app.AddUpdatable(this);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            model = new ThreadsViewModel(settings);
            BindingContext = model;
        }

        private async void OnThreadTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item != null)
            {
                var thread = (MessagesThread)e.Item;
                await Navigation.PushModalAsync(new NavigationPage(new MessageView(app, thread.PublicId)));
            }
        }



        public void Update()
        {
            bool changed = false;
            var newModel = new ThreadsViewModel(settings);
            foreach (var areTheySame in newModel.MessagesThreads.Zip(model.MessagesThreads, (f, s) => f.Name.Equals(s.Name)))
            {
                changed = changed || !areTheySame;
            }

            if (changed)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    base.OnAppearing();
                    BindingContext = newModel;
                });
            }
        }

        private async void addThread_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new AddThread(app)));
        }

        private async void settings_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new Settings(app)));
        }
    }
}
