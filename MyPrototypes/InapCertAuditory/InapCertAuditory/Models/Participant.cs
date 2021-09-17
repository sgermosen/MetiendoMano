using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InapCertAuditory.Models
{
    public class Participant
    {
        public int ParticipantId { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string Rnc { get; set; }

        public string Observations { get; set; }

        public string FullName => $"{Name} {LastName}";

        public Enterprice Enterprice { get; set; }


    }
}
