namespace Bible.Models
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class Bible
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("shortname")]
        public string Shortname { get; set; }

        [JsonProperty("module")]
        public string Module { get; set; }

        [JsonProperty("year")]
        public string Year { get; set; }

        [JsonProperty("lang")]
        public string Lang { get; set; }

        [JsonProperty("lang_short")]
        public string LangShort { get; set; }

        [JsonProperty("copyright")]
        public long Copyright { get; set; }

        [JsonProperty("italics")]
        public long Italics { get; set; }

        [JsonProperty("strongs")]
        public long Strongs { get; set; }

        [JsonProperty("rank")]
        public long Rank { get; set; }

        [JsonProperty("research")]
        public long Research { get; set; }

        [JsonProperty("1")]
        public Dictionary<string, Verse> Chapter1 { get; set; }

        [JsonProperty("2")]
        public Dictionary<string, Verse> Chapter2 { get; set; }

        [JsonProperty("3")]
        public Dictionary<string, Verse> Chapter3 { get; set; }

        [JsonProperty("4")]
        public Dictionary<string, Verse> Chapter4 { get; set; }

        [JsonProperty("5")]
        public Dictionary<string, Verse> Chapter5 { get; set; }

        [JsonProperty("6")]
        public Dictionary<string, Verse> Chapter6 { get; set; }

        [JsonProperty("7")]
        public Dictionary<string, Verse> Chapter7 { get; set; }

        [JsonProperty("8")]
        public Dictionary<string, Verse> Chapter8 { get; set; }

        [JsonProperty("9")]
        public Dictionary<string, Verse> Chapter9 { get; set; }

        [JsonProperty("10")]
        public Dictionary<string, Verse> Chapter10 { get; set; }

        [JsonProperty("11")]
        public Dictionary<string, Verse> Chapter11 { get; set; }

        [JsonProperty("12")]
        public Dictionary<string, Verse> Chapter12 { get; set; }

        [JsonProperty("13")]
        public Dictionary<string, Verse> Chapter13 { get; set; }

        [JsonProperty("14")]
        public Dictionary<string, Verse> Chapter14 { get; set; }

        [JsonProperty("15")]
        public Dictionary<string, Verse> Chapter15 { get; set; }

        [JsonProperty("16")]
        public Dictionary<string, Verse> Chapter16 { get; set; }

        [JsonProperty("17")]
        public Dictionary<string, Verse> Chapter17 { get; set; }

        [JsonProperty("18")]
        public Dictionary<string, Verse> Chapter18 { get; set; }

        [JsonProperty("19")]
        public Dictionary<string, Verse> Chapter19 { get; set; }

        [JsonProperty("20")]
        public Dictionary<string, Verse> Chapter20 { get; set; }

        [JsonProperty("21")]
        public Dictionary<string, Verse> Chapter21 { get; set; }

        [JsonProperty("22")]
        public Dictionary<string, Verse> Chapter22 { get; set; }

        [JsonProperty("23")]
        public Dictionary<string, Verse> Chapter23 { get; set; }

        [JsonProperty("24")]
        public Dictionary<string, Verse> Chapter24 { get; set; }

        [JsonProperty("25")]
        public Dictionary<string, Verse> Chapter25 { get; set; }

        [JsonProperty("26")]
        public Dictionary<string, Verse> Chapter26 { get; set; }

        [JsonProperty("27")]
        public Dictionary<string, Verse> Chapter27 { get; set; }

        [JsonProperty("28")]
        public Dictionary<string, Verse> Chapter28 { get; set; }

        [JsonProperty("29")]
        public Dictionary<string, Verse> Chapter29 { get; set; }

        [JsonProperty("30")]
        public Dictionary<string, Verse> Chapter30 { get; set; }

        [JsonProperty("31")]
        public Dictionary<string, Verse> Chapter31 { get; set; }

        [JsonProperty("32")]
        public Dictionary<string, Verse> Chapter32 { get; set; }

        [JsonProperty("33")]
        public Dictionary<string, Verse> Chapter33 { get; set; }

        [JsonProperty("34")]
        public Dictionary<string, Verse> Chapter34 { get; set; }

        [JsonProperty("35")]
        public Dictionary<string, Verse> Chapter35 { get; set; }

        [JsonProperty("36")]
        public Dictionary<string, Verse> Chapter36 { get; set; }

        [JsonProperty("37")]
        public Dictionary<string, Verse> Chapter37 { get; set; }

        [JsonProperty("38")]
        public Dictionary<string, Verse> Chapter38 { get; set; }

        [JsonProperty("39")]
        public Dictionary<string, Verse> Chapter39 { get; set; }

        [JsonProperty("40")]
        public Dictionary<string, Verse> Chapter40 { get; set; }

        [JsonProperty("41")]
        public Dictionary<string, Verse> Chapter41 { get; set; }

        [JsonProperty("42")]
        public Dictionary<string, Verse> Chapter42 { get; set; }

        [JsonProperty("43")]
        public Dictionary<string, Verse> Chapter43 { get; set; }

        [JsonProperty("44")]
        public Dictionary<string, Verse> Chapter44 { get; set; }

        [JsonProperty("45")]
        public Dictionary<string, Verse> Chapter45 { get; set; }

        [JsonProperty("46")]
        public Dictionary<string, Verse> Chapter46 { get; set; }

        [JsonProperty("47")]
        public Dictionary<string, Verse> Chapter47 { get; set; }

        [JsonProperty("48")]
        public Dictionary<string, Verse> Chapter48 { get; set; }

        [JsonProperty("49")]
        public Dictionary<string, Verse> Chapter49 { get; set; }

        [JsonProperty("50")]
        public Dictionary<string, Verse> Chapter50 { get; set; }

        [JsonProperty("51")]
        public Dictionary<string, Verse> Chapter51 { get; set; }

        [JsonProperty("52")]
        public Dictionary<string, Verse> Chapter52 { get; set; }

        [JsonProperty("53")]
        public Dictionary<string, Verse> Chapter53 { get; set; }

        [JsonProperty("54")]
        public Dictionary<string, Verse> Chapter54 { get; set; }

        [JsonProperty("55")]
        public Dictionary<string, Verse> Chapter55 { get; set; }

        [JsonProperty("56")]
        public Dictionary<string, Verse> Chapter56 { get; set; }

        [JsonProperty("57")]
        public Dictionary<string, Verse> Chapter57 { get; set; }

        [JsonProperty("58")]
        public Dictionary<string, Verse> Chapter58 { get; set; }

        [JsonProperty("59")]
        public Dictionary<string, Verse> Chapter59 { get; set; }

        [JsonProperty("60")]
        public Dictionary<string, Verse> Chapter60 { get; set; }

        [JsonProperty("61")]
        public Dictionary<string, Verse> Chapter61 { get; set; }

        [JsonProperty("62")]
        public Dictionary<string, Verse> Chapter62 { get; set; }

        [JsonProperty("63")]
        public Dictionary<string, Verse> Chapter63 { get; set; }

        [JsonProperty("64")]
        public Dictionary<string, Verse> Chapter64 { get; set; }

        [JsonProperty("65")]
        public Dictionary<string, Verse> Chapter65 { get; set; }

        [JsonProperty("66")]
        public Dictionary<string, Verse> Chapter66 { get; set; }

        [JsonProperty("67")]
        public Dictionary<string, Verse> Chapter67 { get; set; }

        [JsonProperty("68")]
        public Dictionary<string, Verse> Chapter68 { get; set; }

        [JsonProperty("69")]
        public Dictionary<string, Verse> Chapter69 { get; set; }

        [JsonProperty("70")]
        public Dictionary<string, Verse> Chapter70 { get; set; }

        [JsonProperty("71")]
        public Dictionary<string, Verse> Chapter71 { get; set; }

        [JsonProperty("72")]
        public Dictionary<string, Verse> Chapter72 { get; set; }

        [JsonProperty("73")]
        public Dictionary<string, Verse> Chapter73 { get; set; }

        [JsonProperty("74")]
        public Dictionary<string, Verse> Chapter74 { get; set; }

        [JsonProperty("75")]
        public Dictionary<string, Verse> Chapter75 { get; set; }

        [JsonProperty("76")]
        public Dictionary<string, Verse> Chapter76 { get; set; }

        [JsonProperty("77")]
        public Dictionary<string, Verse> Chapter77 { get; set; }

        [JsonProperty("78")]
        public Dictionary<string, Verse> Chapter78 { get; set; }

        [JsonProperty("79")]
        public Dictionary<string, Verse> Chapter79 { get; set; }

        [JsonProperty("80")]
        public Dictionary<string, Verse> Chapter80 { get; set; }

        [JsonProperty("81")]
        public Dictionary<string, Verse> Chapter81 { get; set; }

        [JsonProperty("82")]
        public Dictionary<string, Verse> Chapter82 { get; set; }

        [JsonProperty("83")]
        public Dictionary<string, Verse> Chapter83 { get; set; }

        [JsonProperty("84")]
        public Dictionary<string, Verse> Chapter84 { get; set; }

        [JsonProperty("85")]
        public Dictionary<string, Verse> Chapter85 { get; set; }

        [JsonProperty("86")]
        public Dictionary<string, Verse> Chapter86 { get; set; }

        [JsonProperty("87")]
        public Dictionary<string, Verse> Chapter87 { get; set; }

        [JsonProperty("88")]
        public Dictionary<string, Verse> Chapter88 { get; set; }

        [JsonProperty("89")]
        public Dictionary<string, Verse> Chapter89 { get; set; }

        [JsonProperty("90")]
        public Dictionary<string, Verse> Chapter90 { get; set; }

        [JsonProperty("91")]
        public Dictionary<string, Verse> Chapter91 { get; set; }

        [JsonProperty("92")]
        public Dictionary<string, Verse> Chapter92 { get; set; }

        [JsonProperty("93")]
        public Dictionary<string, Verse> Chapter93 { get; set; }

        [JsonProperty("94")]
        public Dictionary<string, Verse> Chapter94 { get; set; }

        [JsonProperty("95")]
        public Dictionary<string, Verse> Chapter95 { get; set; }

        [JsonProperty("96")]
        public Dictionary<string, Verse> Chapter96 { get; set; }

        [JsonProperty("97")]
        public Dictionary<string, Verse> Chapter97 { get; set; }

        [JsonProperty("98")]
        public Dictionary<string, Verse> Chapter98 { get; set; }

        [JsonProperty("99")]
        public Dictionary<string, Verse> Chapter99 { get; set; }

        [JsonProperty("100")]
        public Dictionary<string, Verse> Chapter100 { get; set; }

        [JsonProperty("101")]
        public Dictionary<string, Verse> Chapter101 { get; set; }

        [JsonProperty("102")]
        public Dictionary<string, Verse> Chapter102 { get; set; }

        [JsonProperty("103")]
        public Dictionary<string, Verse> Chapter103 { get; set; }

        [JsonProperty("104")]
        public Dictionary<string, Verse> Chapter104 { get; set; }

        [JsonProperty("105")]
        public Dictionary<string, Verse> Chapter105 { get; set; }

        [JsonProperty("106")]
        public Dictionary<string, Verse> Chapter106 { get; set; }

        [JsonProperty("107")]
        public Dictionary<string, Verse> Chapter107 { get; set; }

        [JsonProperty("108")]
        public Dictionary<string, Verse> Chapter108 { get; set; }

        [JsonProperty("109")]
        public Dictionary<string, Verse> Chapter109 { get; set; }

        [JsonProperty("110")]
        public Dictionary<string, Verse> Chapter110 { get; set; }

        [JsonProperty("111")]
        public Dictionary<string, Verse> Chapter111 { get; set; }

        [JsonProperty("112")]
        public Dictionary<string, Verse> Chapter112 { get; set; }

        [JsonProperty("113")]
        public Dictionary<string, Verse> Chapter113 { get; set; }

        [JsonProperty("114")]
        public Dictionary<string, Verse> Chapter114 { get; set; }

        [JsonProperty("115")]
        public Dictionary<string, Verse> Chapter115 { get; set; }

        [JsonProperty("116")]
        public Dictionary<string, Verse> Chapter116 { get; set; }

        [JsonProperty("117")]
        public Dictionary<string, Verse> Chapter117 { get; set; }

        [JsonProperty("118")]
        public Dictionary<string, Verse> Chapter118 { get; set; }

        [JsonProperty("119")]
        public Dictionary<string, Verse> Chapter119 { get; set; }

        [JsonProperty("120")]
        public Dictionary<string, Verse> Chapter120 { get; set; }

        [JsonProperty("121")]
        public Dictionary<string, Verse> Chapter121 { get; set; }

        [JsonProperty("122")]
        public Dictionary<string, Verse> Chapter122 { get; set; }

        [JsonProperty("123")]
        public Dictionary<string, Verse> Chapter123 { get; set; }

        [JsonProperty("124")]
        public Dictionary<string, Verse> Chapter124 { get; set; }

        [JsonProperty("125")]
        public Dictionary<string, Verse> Chapter125 { get; set; }

        [JsonProperty("126")]
        public Dictionary<string, Verse> Chapter126 { get; set; }

        [JsonProperty("127")]
        public Dictionary<string, Verse> Chapter127 { get; set; }

        [JsonProperty("128")]
        public Dictionary<string, Verse> Chapter128 { get; set; }

        [JsonProperty("129")]
        public Dictionary<string, Verse> Chapter129 { get; set; }

        [JsonProperty("130")]
        public Dictionary<string, Verse> Chapter130 { get; set; }

        [JsonProperty("131")]
        public Dictionary<string, Verse> Chapter131 { get; set; }

        [JsonProperty("132")]
        public Dictionary<string, Verse> Chapter132 { get; set; }

        [JsonProperty("133")]
        public Dictionary<string, Verse> Chapter133 { get; set; }

        [JsonProperty("134")]
        public Dictionary<string, Verse> Chapter134 { get; set; }

        [JsonProperty("135")]
        public Dictionary<string, Verse> Chapter135 { get; set; }

        [JsonProperty("136")]
        public Dictionary<string, Verse> Chapter136 { get; set; }

        [JsonProperty("137")]
        public Dictionary<string, Verse> Chapter137 { get; set; }

        [JsonProperty("138")]
        public Dictionary<string, Verse> Chapter138 { get; set; }

        [JsonProperty("139")]
        public Dictionary<string, Verse> Chapter139 { get; set; }

        [JsonProperty("140")]
        public Dictionary<string, Verse> Chapter140 { get; set; }

        [JsonProperty("141")]
        public Dictionary<string, Verse> Chapter141 { get; set; }

        [JsonProperty("142")]
        public Dictionary<string, Verse> Chapter142 { get; set; }

        [JsonProperty("143")]
        public Dictionary<string, Verse> Chapter143 { get; set; }

        [JsonProperty("144")]
        public Dictionary<string, Verse> Chapter144 { get; set; }

        [JsonProperty("145")]
        public Dictionary<string, Verse> Chapter145 { get; set; }

        [JsonProperty("146")]
        public Dictionary<string, Verse> Chapter146 { get; set; }

        [JsonProperty("147")]
        public Dictionary<string, Verse> Chapter147 { get; set; }

        [JsonProperty("148")]
        public Dictionary<string, Verse> Chapter148 { get; set; }

        [JsonProperty("149")]
        public Dictionary<string, Verse> Chapter149 { get; set; }

        [JsonProperty("150")]
        public Dictionary<string, Verse> Chapter150 { get; set; }
    }
}