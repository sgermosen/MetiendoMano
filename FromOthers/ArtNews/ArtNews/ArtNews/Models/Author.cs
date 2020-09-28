using System.Collections.Generic;

namespace ArtNews.Models
{
    public class Author
    {
        public string Name { get; set; }
        public string Dates { get; set; }
        public string Description { get; set; }
        public List<ArtItem> Highlights { get; set; }
    }
}
