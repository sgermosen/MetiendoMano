using System;
using System.Collections.Generic;
using System.Text;

namespace TestShell.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Helpers;
    using Xamarin.Essentials;
    using Xamarin.Forms;

    public class SocialItem
    {
        public SocialItem()
        {
            OpenUrlCommand = new Command(async () => await OpenSocialUrl());
        }

        public string Icon { get; set; }
        public string Url { get; set; }

        public ICommand OpenUrlCommand { get; }

        async Task OpenSocialUrl()
        {
            if (Url.Contains("twitter"))
            {
                var launch = DependencyService.Get<ILaunchTwitter>();
                if (launch?.OpenUserName("shanselman") ?? false)
                    return;
            }
            await Browser.OpenAsync(Url);
        }
    }
}
