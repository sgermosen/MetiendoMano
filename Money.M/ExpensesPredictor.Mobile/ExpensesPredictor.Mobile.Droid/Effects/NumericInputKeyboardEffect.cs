using Android.Text;
using Android.Widget;
using ExpensesPredictor.Mobile.Droid.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ResolutionGroupName("SomosTechies")]
[assembly: ExportEffect(typeof(NumericInputKeyboardEffect), "NumericInputKeyboardEffect")]

namespace ExpensesPredictor.Mobile.Droid.Effects
{
    public class NumericInputKeyboardEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            var input = (EditText) Control;
            input.InputType= InputTypes.ClassNumber|InputTypes.NumberFlagDecimal|InputTypes.NumberFlagSigned;
        }

        protected override void OnDetached()
        {
            var input = (EditText)Control;
            input.InputType = InputTypes.Null;
        }
    }

}