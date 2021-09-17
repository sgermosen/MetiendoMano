using System;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace ImpNotes.Models
{
    public class Operation
    {
        [AutoIncrement, PrimaryKey]
        // public string Id { get; set; }
        public int Id { get; set; }

        public DateTime CreatedDate { get; set; }

        [ForeignKey(typeof(Account))]
        public int AffectedAccountId { get; set; }

        [ManyToOne]
        public Account AffectedAccount { get; set; }

        [ForeignKey(typeof(Account))]
        public int DestAccountId { get; set; }

        [ManyToOne]
        public Account DestAccount { get; set; }

        [ForeignKey(typeof(Concept))]
        public int ConceptId { get; set; }

        [ManyToOne]
        public Account Concept { get; set; }
        
        public decimal Amount { get; set; }

        public bool Recurrent { get; set; }

        [ForeignKey(typeof(Periodicity))]
        public int PeriodicityId { get; set; }

        [ManyToOne]
        public Periodicity Periodicity { get; set; }

        public string Observations { get; set; }
        
        public Operation()
        {
            CreatedDate = DateTime.UtcNow;
            // Id = Guid.NewGuid().ToString();
        }

        public override string ToString() => $"{CreatedDate} {Amount}";

    }
}
