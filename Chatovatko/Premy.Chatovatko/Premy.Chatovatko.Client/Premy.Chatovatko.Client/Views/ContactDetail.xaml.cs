using Premy.Chatovatko.Client.Helpers;
using Premy.Chatovatko.Client.Libs.Database;
using Premy.Chatovatko.Client.Libs.Database.InsertModels;
using Premy.Chatovatko.Client.Libs.Database.Models;
using Premy.Chatovatko.Client.Libs.Database.UpdateModels;
using Premy.Chatovatko.Client.Libs.UserData;
using Premy.Chatovatko.Libs.Cryptography;
using Premy.Chatovatko.Libs.DataTransmission.JsonModels.SearchContact;
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
	public partial class ContactDetail : ContentPage, ILoggable
	{
        private bool _adding;
        private bool Adding
        {
            get
            {
                return _adding;
            }
            set
            {
                _adding = value;
                if (value)
                {
                    ToolbarItem item = new ToolbarItem("Add", "add.png", AddUser);
                    ToolbarItems.Add(item);
                }
                else
                {
                    ToolbarItem item = new ToolbarItem("Save", "checkmark.png", SaveUser);
                    ToolbarItems.Add(item);
                }
            }
        }

        private Contacts contact;
        private readonly SettingsCapsula settings;
        private readonly App app;

		public ContactDetail (SettingsCapsula settings, long userPublicId, App app)
		{
			InitializeComponent ();
            this.settings = settings;
            this.app = app;
            Adding = false;
            Contacts contact;
            using (Context context = new Context(settings.Config))
            {
                contact = context.Contacts
                    .Where(u => u.PublicId == userPublicId)
                    .Single();
            }

            LoadContact(contact);
		}


        public ContactDetail(SettingsCapsula settings, SearchCServerCapsula user, App app)
        {
            InitializeComponent();
            this.settings = settings;
            this.app = app;

            using (Context context = new Context(settings.Config))
            {
                var tryContact = context.Contacts
                    .Where(u => u.PublicId == user.UserId)
                    .SingleOrDefault();
                if(tryContact != null)
                {
                    throw new Exception("This user is already saved");
                }
            }

            Adding = true;
            Contacts contact = new Contacts()
            {
                PublicId = user.UserId,
                UserName = user.UserName,
                AlarmPermission = 0,
                NickName = null,
                Trusted = 0,
                SendAesKey = null,
                PublicCertificate = user.PemCertificate,
                ReceiveAesKey = null
            };

            LoadContact(contact);
        }

        private void LoadContact(Contacts contact)
        {
            this.contact = contact;

            publicIdLabel.Text = contact.PublicId.ToString();
            userNameEntry.Text = contact.UserName;
            alarmSwitch.IsToggled = contact.AlarmPermission == 1;
            nickNameEntry.Text = contact.NickName;

            trustedSwitch.IsToggled = contact.Trusted == 1;
            sendAesLabel.Text = (contact.SendAesKey != null).ToString();
            receiveAesLabel.Text = (contact.ReceiveAesKey != null).ToString();
            sha256Label.Text = SHA256Utils.ComputeCertHash(contact.PublicCertificate);
        }

        private void DiscardChanges()
        {
            Navigation.PopModalAsync();
        }

        private async void AddUser()
        {
            try
            {
                CContact contact = new CContact()
                {
                    AlarmPermission = alarmSwitch.IsToggled,
                    NickName = nickNameEntry.Text,
                    PublicCertificate = this.contact.PublicCertificate,
                    PublicId = this.contact.PublicId,
                    Trusted = this.contact.Trusted == 1,
                    UserName = this.contact.UserName
                };

                using (Context context = new Context(settings.Config))
                {
                    PushOperations.Insert(context, contact, settings.UserPublicId, settings.UserPublicId);
                }

                if (contact.Trusted != trustedSwitch.IsToggled)
                {
                    await SaveTrustification(trustedSwitch.IsToggled);
                }

                await Navigation.PopModalAsync();
            }
            catch (Exception ex)
            {
                app.logger.LogException(this, ex);
                ShowError(ex.Source, ex.Message);
            }
            
        }

        private async Task SaveTrustification(bool trust)
        {
            using (LoadingLock theLock = new LoadingLock(this, "Saving trust changes to server..."))
            {
                switch (trustedSwitch.IsToggled)
                {
                    case true:
                        await Task.Run(() => app.connection.TrustContact((int)contact.PublicId));
                        break;

                    case false:
                        await Task.Run(() => app.connection.UntrustContact((int)contact.PublicId));
                        break;
                }
            }
        }

        private async void SaveUser()
        {
            try
            {
                UContact contact = new UContact(this.contact);

                contact.NickName = nickNameEntry.Text;
                contact.AlarmPermission = alarmSwitch.IsToggled;

                using (Context context = new Context(settings.Config))
                {
                    PushOperations.Update(context, contact, settings.UserPublicId, settings.UserPublicId);
                }

                if (contact.Trusted != trustedSwitch.IsToggled)
                {
                    await SaveTrustification(trustedSwitch.IsToggled);
                }

                await Navigation.PopModalAsync();
            }
            catch (Exception ex)
            {
                app.logger.LogException(this, ex);
                ShowError(ex.Source, ex.Message);
            }
            
        }

        private async void ShowError(String name, String message)
        {
            await DisplayAlert(name, message, "OK");
        }

        public string GetLogSource()
        {
            return "Contact detail";
        }
    }
}
