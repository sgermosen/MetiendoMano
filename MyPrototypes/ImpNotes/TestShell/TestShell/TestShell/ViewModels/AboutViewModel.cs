 

namespace TestShell.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Helpers;
    using Models;
    using MvvmHelpers;

    public class AboutViewModel : BaseViewModel
    {
        public List<SocialItem> SocialItems { get; }
        public AboutViewModel()
        {
            SocialItems = new List<SocialItem>
            {
                new SocialItem
                {
                    Icon = IconConstants.TwitterCircle,
                    Url = "https://www.twitter.com/shanselman"
                },
                new SocialItem
                {
                    Icon = IconConstants.FacebookBox,
                    Url = "https://www.facebook.com/shanselman"
                },
                new SocialItem
                {
                    Icon = IconConstants.Instagram,
                    Url = "https://www.instagram.com/shanselman"
                }
            };
        }
    }
}