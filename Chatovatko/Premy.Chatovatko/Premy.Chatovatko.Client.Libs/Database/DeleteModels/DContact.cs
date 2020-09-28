using System;
using System.Collections.Generic;
using System.Text;
using Premy.Chatovatko.Client.Libs.Database.Models;

namespace Premy.Chatovatko.Client.Libs.Database.DeleteModels
{
    public class DContact : IDeleteModel
    {
        private readonly Contacts contact;
        private readonly long myUserId;

        public DContact(Contacts contact, long myUserId)
        {
            this.contact = contact;
            this.myUserId = myUserId;
        }

        public void DoDelete(Context context)
        {
            context.Contacts.Remove(contact);
            PushOperations.DeleteBlobMessage(context, contact.GetBlobId(), myUserId);
        }
    }
}
