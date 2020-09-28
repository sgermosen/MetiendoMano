using System;
using Newtonsoft.Json;

namespace ExpensesPredictor.Mobile.Models
{
    public class ExpenseFrecuency
    {
        public int TimesPerMonth { get; private set; }

        [JsonConstructor]
        public ExpenseFrecuency(int timesPerMonth)
        {
            TimesPerMonth = timesPerMonth;
        }

        public static ExpenseFrecuency Daily { get; } = new ExpenseFrecuency(30);

        public static ExpenseFrecuency Weekly { get; } = new ExpenseFrecuency(4);

        public static ExpenseFrecuency Monthly { get; } = new ExpenseFrecuency(1);

        public static ExpenseFrecuency TimesMontly(int times)
        {
            if(times <1 || times > 30) throw new ArgumentException(nameof(times));

            return new ExpenseFrecuency(times);
        }

        public static ExpenseFrecuency TimesWeekly(int times)
        {
            if(times < 1 || times > 7) throw new ArgumentException(nameof(times));

            return new ExpenseFrecuency(times);
        }
    }
}