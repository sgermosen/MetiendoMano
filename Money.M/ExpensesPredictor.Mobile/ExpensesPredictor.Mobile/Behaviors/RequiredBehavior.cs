using ExpensesPredictor.Mobile.Behaviors.Abstract;

namespace ExpensesPredictor.Mobile.Behaviors
{
    public class RequiredBehavior: ValidationBehavior
    {
        protected override bool Validate(string newvalue)
        {
            return !string.IsNullOrWhiteSpace(newvalue);
        }
    }
}