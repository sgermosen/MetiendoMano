using System;
using System.Collections.Generic;
using System.Text;

namespace TestShell.Models
{
    public class VideoFeedItem
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public string PublishDate { get; set; }
        public string ThumbnailUrl { get; set; }
        public string Category { get; set; }
        public List<VideoContentItem> VideoUrls { get; set; }
    }
}
