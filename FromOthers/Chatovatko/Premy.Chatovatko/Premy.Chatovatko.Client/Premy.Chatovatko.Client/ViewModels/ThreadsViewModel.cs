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
    public class ThreadsViewModel : BaseViewModel
    {
        private ObservableCollection<MessagesThread> messagesThreads;
        public ObservableCollection<MessagesThread> MessagesThreads
        {
            get { return messagesThreads; }
            set
            {
                messagesThreads = value;
            }
        }

        public ThreadsViewModel(SettingsCapsula settings)
        {

            using (Context context = new Context(settings.Config))
            {
                var threadsList = context.MessagesThread.ToList();
                MessagesThreads = new ObservableCollection<MessagesThread>(threadsList);
            }
        }
    }
}
