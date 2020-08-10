using System;
using System.Collections.Generic;
using System.Text;

namespace TestShell.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using Models;

    public interface IDataService
    {
        Task<IEnumerable<Podcast>> GetPodcastsAsync(bool forceRefresh);
        Task<IEnumerable<PodcastEpisode>> GetPodcastEpisodesAsync(bool forceRefresh);
        Task<IEnumerable<BlogFeedItem>> GetBlogItemsAsync(bool forceRefresh);
        Task<IEnumerable<Tweet>> GetTweetsAsync(bool forceRefresh);
    }
}
