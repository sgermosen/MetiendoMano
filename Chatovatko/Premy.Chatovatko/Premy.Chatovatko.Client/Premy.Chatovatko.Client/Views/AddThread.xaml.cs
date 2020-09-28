using Premy.Chatovatko.Client.Libs.Database;
using Premy.Chatovatko.Client.Libs.Database.InsertModels;
using Premy.Chatovatko.Client.Libs.Database.Models;
using Premy.Chatovatko.Client.Libs.Sync;
using Premy.Chatovatko.Client.Libs.UserData;
using Premy.Chatovatko.Client.ViewModels;
using Premy.Chatovatko.Libs.Logging;
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
	public partial class AddThread : ContentPage, IUpdatable, ILoggable
    {
        private App app;
        private TrustedContactsViewModel model;
        private SettingsCapsula settings;

        public AddThread (App app)
		{
			InitializeComponent ();
            this.settings = app.settings;
            this.app = app;
            app.AddUpdatable(this);
        }

        private async void OnContactTapped(object sender, ItemTappedEventArgs e)
        {
            try
            {
                if(nameLabel.Text == null || nameLabel.Text == "")
                {
                    throw new Exception("Please, enter the thread name!");
                }
                if (e.Item != null)
                {
                    var contact = (Contacts)e.Item;

                    CMessageThread thread;
                    using (Context context = new Context(settings.Config))
                    {
                        thread = new CMessageThread(context, nameLabel.Text, false, contact.PublicId, settings.UserPublicId);
                        PushOperations.Insert(context, thread, contact.PublicId, settings.UserPublicId);
                    }
                    await Navigation.PopModalAsync();
                    await app.MainPage.Navigation.PushModalAsync(new NavigationPage(new MessageView(app, thread.PublicId)));
                }
            }
            catch(Exception ex)
            {
                ShowError(ex);
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            model = new TrustedContactsViewModel(settings);
            BindingContext = model;
        }


        public void Update()
        {
            bool changed = false;
            var newModel = new TrustedContactsViewModel(settings);
            foreach (var areTheySame in newModel.Contacts.Zip(model.Contacts, (f, s) => f.ShowName.Equals(s.ShowName)))
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

        private void close_Clicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
            
        }

        private async void ShowError(Exception ex)
        {
            app.logger.LogException(this, ex);
            await DisplayAlert(ex.Source, ex.Message, "OK");
        }

        public string GetLogSource()
        {
            return "Add thread dialog";
        }

        private void nameLabel_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(e.NewTextValue == null || e.NewTextValue.Equals(""))
            {
                usersList.IsEnabled = false;
            }
            else
            {
                usersList.IsEnabled = true;
            }
        }
    }
}
