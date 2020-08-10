namespace Bible.Models
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class ContentResponse
    {
        [JsonProperty("hash")]
        public string Hash { get; set; }

        [JsonProperty("error_level")]
        public long ErrorLevel { get; set; }

        [JsonProperty("results")]
        public List<Content> Contents { get; set; }
    }
}