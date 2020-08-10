namespace Bible.Models
{
    using Newtonsoft.Json;

    public partial class Verses
    {
        [JsonProperty("kjv")]
        public Bible Kjv { get; set; }

        [JsonProperty(PropertyName = "kjv_strongs")]
        public Bible KjvStrongs { get; set; }

        [JsonProperty(PropertyName = "tyndale")]
        public Bible Tyndale { get; set; }

        [JsonProperty(PropertyName = "coverdale")]
        public Bible Coverdale { get; set; }

        [JsonProperty(PropertyName = "bishops")]
        public Bible Bishops { get; set; }

        [JsonProperty(PropertyName = "geneva")]
        public Bible Geneva { get; set; }

        [JsonProperty(PropertyName = "tr")]
        public Bible Tr { get; set; }

        [JsonProperty(PropertyName = "trparsed")]
        public Bible Trparsed { get; set; }

        [JsonProperty(PropertyName = "rv_1858")]
        public Bible Rv1858 { get; set; }

        [JsonProperty(PropertyName = "rv_1909")]
        public Bible Rv1909 { get; set; }

        [JsonProperty(PropertyName = "sagradas")]
        public Bible Sagradas { get; set; }

        [JsonProperty(PropertyName = "rvg")]
        public Bible Rvg { get; set; }

        [JsonProperty(PropertyName = "martin")]
        public Bible Martin { get; set; }

        [JsonProperty(PropertyName = "epee")]
        public Bible Epee { get; set; }

        [JsonProperty(PropertyName = "oster")]
        public Bible Oster { get; set; }

        [JsonProperty(PropertyName = "afri")]
        public Bible Afri { get; set; }

        [JsonProperty(PropertyName = "svd")]
        public Bible Svd { get; set; }

        [JsonProperty(PropertyName = "bkr")]
        public Bible Bkr { get; set; }

        [JsonProperty(PropertyName = "stve")]
        public Bible Stve { get; set; }

        [JsonProperty(PropertyName = "finn")]
        public Bible Finn { get; set; }

        [JsonProperty(PropertyName = "luther")]
        public Bible Luther { get; set; }

        [JsonProperty(PropertyName = "diodati")]
        public Bible Diodati { get; set; }

        [JsonProperty(PropertyName = "synodal")]
        public Bible Synodal { get; set; }

        [JsonProperty(PropertyName = "karoli")]
        public Bible Karoli { get; set; }

        [JsonProperty(PropertyName = "lith")]
        public Bible Lith { get; set; }

        [JsonProperty(PropertyName = "maori")]
        public Bible Maori { get; set; }

        [JsonProperty(PropertyName = "cornilescu")]
        public Bible Cornilescu { get; set; }

        [JsonProperty(PropertyName = "thaikjv")]
        public Bible Thaikjv { get; set; }

        [JsonProperty(PropertyName = "wlc")]
        public Bible Wlc { get; set; }
    }
}