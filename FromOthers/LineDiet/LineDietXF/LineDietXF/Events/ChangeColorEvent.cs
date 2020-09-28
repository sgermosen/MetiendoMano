using Prism.Events;
using Xamarin.Forms;

namespace LineDietXF.Events
{
    /// <summary>
    /// Thrown when the app needs to change colors, consumer is at the platform level (ex: Android/iOS)
    /// </summary>
    public class ChangeColorEvent : PubSubEvent<Color>
    {
    }
}