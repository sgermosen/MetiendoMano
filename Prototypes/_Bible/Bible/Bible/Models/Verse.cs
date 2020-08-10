namespace Bible.Models
{
    using Newtonsoft.Json;

    public class Verse
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("book")]
        public long Book { get; set; }

        [JsonProperty("chapter")]
        public long Chapter { get; set; }

        [JsonProperty("verse")]
        public long VerseNumber { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("italics")]
        public string Italics { get; set; }
    }
}