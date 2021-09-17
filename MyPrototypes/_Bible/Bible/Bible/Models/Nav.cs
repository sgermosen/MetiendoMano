namespace Bible.Models
{
    using Newtonsoft.Json;

    public class Nav
    {
        [JsonProperty("prev_book")]
        public object PrevBook { get; set; }

        [JsonProperty("next_book")]
        public string NextBook { get; set; }

        [JsonProperty("next_chapter")]
        public string NextChapter { get; set; }

        [JsonProperty("prev_chapter")]
        public object PrevChapter { get; set; }
    }
}