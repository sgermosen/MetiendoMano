using System;
using System.Collections.Generic;
using System.Text;

namespace TestShell.Models
{
    using System.Collections.Generic;
using Xamarin.Essentials;

    public class PodcastService
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public List<DevicePlatform> SupportedPlatforms { get; set; }
    }
}
