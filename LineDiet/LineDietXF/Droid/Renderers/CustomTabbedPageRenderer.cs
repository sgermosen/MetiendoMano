// largely taken from https://forums.xamarin.com/discussion/comment/205661/#Comment_205661

using LineDietXF.Droid.Renderers;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Platform.Android.AppCompat;

[assembly: ExportRenderer(typeof(TabbedPage), typeof(CustomTabbedPageRenderer))]
namespace LineDietXF.Droid.Renderers
{
    /// <summary>
    /// This renderer is to keep a TabbedPage from swiping between tabs. Useful if the tab contains something which should be swipe-able.
    /// </summary>
    public class CustomTabbedPageRenderer : TabbedPageRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<TabbedPage> e)
        {
            // Disable animations only when UseAnimations is queried for enabling gestures
            var fieldInfo = typeof(TabbedPageRenderer).GetField("_useAnimations", BindingFlags.Instance | BindingFlags.NonPublic);
            if (fieldInfo != null)
                fieldInfo.SetValue(this, false);

            base.OnElementChanged(e);

            // Re-enable animations for everything else
            if (fieldInfo != null)
                fieldInfo.SetValue(this, true);
        }
    }
}