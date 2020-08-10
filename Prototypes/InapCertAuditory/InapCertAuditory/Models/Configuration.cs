using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InapCertAuditory.Models
{
    public class Configuration
    {
        public int ConfigurationId { get; set; }

        public string Rnc { get; set; }

        public string Name { get; set; }

        public string Header1 { get; set; }

        public string Header2 { get; set; }

        public string Header3 { get; set; }

        public string Footer1 { get; set; }

        public string Footer2 { get; set; }

        public string Footer3 { get; set; }

        public string Director { get; set; }

        public string HeaderImage { get; set; }

        public string FooterImage { get; set; }

        public string LateralImage { get; set; }
    }
}
