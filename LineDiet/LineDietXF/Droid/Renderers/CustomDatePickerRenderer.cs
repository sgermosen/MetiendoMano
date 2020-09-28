// NOTE:: largely taken from: https://gist.github.com/dkudelko/42f2d5bc1c8b3aba1d3d
using Xamarin.Forms;
using LineDietXF.Droid.Renderers;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(DatePicker), typeof(CustomDatePickerRenderer))]
namespace LineDietXF.Droid.Renderers
{
    /// <summary>
    /// This renderer will remove the underline normally seen in Android EditText controls
    /// NOTE:: This renderer affects all Entry elements, should instead target a derived Entry type if this is not desired
    /// </summary>
    public class CustomDatePickerRenderer : DatePickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.DatePicker> e)
        {
            base.OnElementChanged(e);

            // setting this background color to transparent gets rid of the underline normally seen in Android edit fields
            this.Control?.SetBackgroundColor(Android.Graphics.Color.Transparent);
        }
    }
}