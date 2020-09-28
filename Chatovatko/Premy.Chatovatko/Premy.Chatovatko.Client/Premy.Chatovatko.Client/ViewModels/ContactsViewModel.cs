using Premy.Chatovatko.Client.Helpers;
using Premy.Chatovatko.Client.Libs.Database.Models;
using Premy.Chatovatko.Client.Libs.UserData;
using Premy.Chatovatko.Client.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Premy.Chatovatko.Client.ViewModels
{
    public class ContactsViewModel : BaseViewModel
    {
        private ObservableCollection<Contacts> contacts;
        public ObservableCollection<Contacts> Contacts
        {
            get { return contacts; }
            set
            {
                contacts = value;
            }
        }

        public ContactsViewModel(SettingsCapsula settings)
        {

            using (Context context = new Context(settings.Config))
            {

                var contactList = context.Contacts.ToList();

                Contacts = new ObservableCollection<Contacts>(contactList);
            }
        }
    }
}
