namespace Bible.Models
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class BibleResponse
    {
        [JsonProperty("error_level")]
        public long ErrorLevel { get; set; }

        [JsonProperty("results")]
        public Dictionary<string, Bible> Bibles { get; set; }
    }
}