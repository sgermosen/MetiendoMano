using System.Collections.Generic;

namespace ExpensesPredictor.Mobile.Models
{
    public static class ExpenseFrecuencies
    {
        public static List<ExpenseOption> Get()
        {
            return new List<ExpenseOption>()
            {
                new ExpenseOption() { Label = "Monthly", Frecuency = ExpenseFrecuency.Monthly},
                new ExpenseOption() { Label = "Weekly", Frecuency = ExpenseFrecuency.Weekly},
                new ExpenseOption() { Label = "Daily", Frecuency = ExpenseFrecuency.Daily}
            };
        }
    }

    public class ExpenseOption
    {
        public string Label { get; set; }
        public ExpenseFrecuency Frecuency { get; set; }

        public override string ToString() => Label;
    }
}
