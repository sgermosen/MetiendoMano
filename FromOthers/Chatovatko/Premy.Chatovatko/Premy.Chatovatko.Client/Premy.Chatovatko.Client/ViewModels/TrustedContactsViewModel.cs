using Premy.Chatovatko.Client.Helpers;
using Premy.Chatovatko.Client.Libs.Database.Models;
using Premy.Chatovatko.Client.Libs.UserData;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Premy.Chatovatko.Client.ViewModels
{
    public class TrustedContactsViewModel : BaseViewModel
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

        public TrustedContactsViewModel(SettingsCapsula settings)
        {

            using (Context context = new Context(settings.Config))
            {
                var contactList = context.Contacts
                    .Where(u => u.Trusted == 1)
                    .ToList();

                Contacts = new ObservableCollection<Contacts>(contactList);
            }
        }
    }
}
