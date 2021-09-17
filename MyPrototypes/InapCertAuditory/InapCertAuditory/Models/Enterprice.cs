using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InapCertAuditory.Models
{
    public class Enterprice
    {
        public int EnterpriceId { get; set; }

        public string Name { get; set; }

        public string Tel { get; set; }

        public string Address { get; set; }

        public string Observations { get; set; }

        public ICollection<Participant> Participants { get; set; }
}
}
