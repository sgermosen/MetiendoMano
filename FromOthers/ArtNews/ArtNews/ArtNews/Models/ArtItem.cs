using System.Collections.Generic;

namespace ArtNews.Models
{
    public class ArtItem
    {
        public string Name { get; set; }
        public int Number { get; set; }
        public string NumberText { get; set; }
        public string Banner { get; set; }
        public List<string> Images { get; set; }
        public string Author { get; set; }
        public string Size { get; set; }
        public string BriefIntroducction { get; set; }
        public string ArtAppreciation { get; set; }
        public List<ArtItem> Related { get; set; }
    }
}