using System;
using System.Collections.Generic;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace ImpNotes.Models
{
    public class Account
    {
        [AutoIncrement, PrimaryKey]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Amount { get; set; }

        //[ForeignKey(typeof(Wallet))]
        //public int WalletId { get; set; }

        //[ManyToOne]
        //public Wallet Wallet { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Operation> Operations { get; set; }

        //public AccountType AccountType { get; set; }

        //public Wallet Wallet { get; set; }

        //public Currency Currency { get; set; }

    }
}
