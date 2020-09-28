using System;
using System.Collections.Generic;
using System.Linq;

namespace ExpensesPredictor.Mobile.Models
{
    public class Expense: ValidableBindableBase
    {
        private string _title;
        private string _description;
        private double _amount;
        private ExpenseFrecuency _frecuency;
        
        public Guid Id { get; set; }

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title,value); }
        }

        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description,value); }
        }

        public double Amount
        {
            get { return _amount; }
            set { SetProperty(ref _amount,value);}
        }

        public ExpenseFrecuency Frecuency
        {
            get { return _frecuency; }
            set {SetProperty(ref _frecuency,value); }
        }

        public override bool IsValid
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Title)) return false;

                if (Amount <= 0) return false;

                if (Frecuency == null) return false;

                return true;
            }
        }

        public static double CalculateTotalEstimatedForThisMonth(IEnumerable<Expense> expenses)
        {
            var total = expenses.Sum(x => x.Amount*x.Frecuency.TimesPerMonth);
            return total;
        }
    }
}
