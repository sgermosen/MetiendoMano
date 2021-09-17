using System;
using System.Collections.Generic;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace ImpNotes.Models
{
    public class Wallet
    {
        [AutoIncrement, PrimaryKey]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Amount { get; set; }

        [OneToMany(CascadeOperations=CascadeOperation.All)]
        public List<Account> Accounts { get; set; }

    }
}
