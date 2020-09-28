using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFriends.Models
{
    [DataTable("PeopleBase")]
    public class Friend
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string Age { get; set; }

        public string Description { get; set; }

        public string FacebookPage { get; set; }

        public string Image { get; set; }

        public string CompleteName
        {
            get { return Name + " " + LastName; }
        }

        [Version]
        public string AzureVersion { get; set; }
    }
}
