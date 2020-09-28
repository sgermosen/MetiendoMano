using LineDietXF.iOS.Renderers;
using ObjCRuntime;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Entry), typeof(AutoSelectEntryRenderer))]
namespace LineDietXF.iOS.Renderers
{
    /// <summary>
    /// This renderer will auto-select the text within an entry field when it becomes focused
    /// NOTE:: This renderer affects all Entry elements, should instead target a derived Entry type if this is not desired
    /// </summary>
    public class AutoSelectEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
                e.OldElement.Focused -= Entry_Focused;

            if (e.NewElement != null)
                e.NewElement.Focused += Entry_Focused;
        }

        void Entry_Focused(object sender, FocusEventArgs e)
        {
            var entry = sender as Entry;
            if (entry == null)
                return;

            var nativeTextField = (UITextField)Control;
            if (nativeTextField == null)
                return;

            nativeTextField.PerformSelector(new Selector("selectAll"), null, 0.0f);
        }
    }
}