namespace Bible.Models
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class BookResponse
    {
        [JsonProperty("error_level")]
        public long ErrorLevel { get; set; }

        [JsonProperty("results")]
        public List<Book> Books { get; set; } 
    }
}