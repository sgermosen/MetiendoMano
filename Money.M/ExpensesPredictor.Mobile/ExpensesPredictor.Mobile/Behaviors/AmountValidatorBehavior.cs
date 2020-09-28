using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpensesPredictor.Mobile.Behaviors.Abstract;

namespace ExpensesPredictor.Mobile.Behaviors
{
    public class AmountValidatorBehavior: ValidationBehavior
    {
        protected override bool Validate(string newvalue)
        {
            if (string.IsNullOrWhiteSpace(newvalue)) return true;
            var result = 0d;

            if (!double.TryParse(newvalue, out result))return false;

            return result >= 0;
        }
    }
}
