using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Notif.Backend.Models
{
    using System;
    using Newtonsoft.Json;

    public class ReactionResponse
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("observation")]
        public string Observation { get; set; }

        [JsonProperty("imageUrl")]
        public string ImageUrl { get; set; }

        [JsonProperty("soundUrl")]
        public string SoundUrl { get; set; }

        [JsonProperty("videoUrl")]
        public string VideoUrl { get; set; }

        [JsonProperty("punctuation")]
        public int Punctuation { get; set; }

        [JsonProperty("longitude")]
        public double? Longitude { get; set; }

        [JsonProperty("latitude")]
        public double? Latitude { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }
    }
}
