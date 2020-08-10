namespace Bible.Models
{
    using Newtonsoft.Json;

    public class Content
    {
        [JsonProperty("book_id")]
        public long BookId { get; set; }

        [JsonProperty("book_name")]
        public string BookName { get; set; }

        [JsonProperty("book_short")]
        public string BookShort { get; set; }

        [JsonProperty("book_raw")]
        public string BookRaw { get; set; }

        [JsonProperty("chapter_verse")]
        public string ChapterVerse { get; set; }

        [JsonProperty("chapter_verse_raw")]
        public object ChapterVerseRaw { get; set; }

        [JsonProperty("verses")]
        public Verses Verses { get; set; }

        [JsonProperty("verses_count")]
        public long VersesCount { get; set; }

        [JsonProperty("single_verse")]
        public bool SingleVerse { get; set; }

        [JsonProperty("nav")]
        public Nav Nav { get; set; }
    }
}