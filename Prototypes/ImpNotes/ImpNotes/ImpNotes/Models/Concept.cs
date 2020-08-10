 using System;
using System.Collections.Generic;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace ImpNotes.Models
{
    public class Concept
    {
        
        [AutoIncrement, PrimaryKey]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal AmountWasted { get; set; }

        public decimal AmountGain { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Operation> Operations { get; set; }

    }
}
