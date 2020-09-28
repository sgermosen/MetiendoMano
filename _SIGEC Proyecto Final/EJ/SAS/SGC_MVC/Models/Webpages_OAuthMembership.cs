using System;
using System.Collections.Generic;

namespace SGC_MVC.Models
{
    public partial class Webpages_OAuthMembership
    {
        public string Provider { get; set; }
        public string ProviderUserID { get; set; }
        public int UserID { get; set; }
    }
}
