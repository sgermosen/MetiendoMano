// NOTE:: auto-select text largely taken from: https://forums.xamarin.com/discussion/comment/64776/#Comment_64776
// NOTE:: removing field underline largely taken from: https://gist.github.com/dkudelko/42f2d5bc1c8b3aba1d3d
using Android.Text.Method;
using LineDietXF.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Entry), typeof(CustomEntryRenderer))]
namespace LineDietXF.Droid.Renderers
{
    /// <summary>
    /// This renderer will auto-select the text within an entry field when it becomes focused and removes the underline normally seen in Android EditText controls
    /// NOTE:: This renderer affects all Entry elements, should instead target a derived Entry type if this is not desired
    /// </summary>
    public class CustomEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            // update native EditText control to auto-select all text on focus
            if (e.OldElement == null)
            {
                var nativeEditText = (global::Android.Widget.EditText)Control;
                nativeEditText.SetSelectAllOnFocus(true);
            }

            if (this.Control != null)
            {
                // HACK:: allows commas to be used for folks in locales where comma is used as a decimal
                this.Control.KeyListener = DigitsKeyListener.GetInstance("1234567890.,");
            }

            // setting this background color to transparent gets rid of the underline normally seen in Android edit fields
            this.Control?.SetBackgroundColor(Android.Graphics.Color.Transparent);
        }
    }
}