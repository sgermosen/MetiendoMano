using System;
using System.Collections.Generic;
using System.Text;

namespace Notif.Transversal.Models
{
    using System;
    using Newtonsoft.Json;

    public class TokenResponse
    {
        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("expiration")]
        public DateTimeOffset Expiration { get; set; }
    }

}
