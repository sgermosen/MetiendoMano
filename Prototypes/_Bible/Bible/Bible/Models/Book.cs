namespace Bible.Models
{
    using Newtonsoft.Json;

    public class Book
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("shortname")]
        public string Shortname { get; set; }
    }
}