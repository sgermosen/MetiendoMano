using LineDietXF.Enumerations;
using LineDietXF.Events;
using LineDietXF.Extensions;
using LineDietXF.iOS.Renderers;
using Prism.Events;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(NavigationPage), typeof(CustomNavigationPageRenderer))]
namespace LineDietXF.iOS.Renderers
{
    /// <summary>
    /// This custom renderer is responsible for setting the navigation bar colors both at startup and in response to 
    /// the ColorChangeEvent thrown from the common code
    /// </summary>
    public class CustomNavigationPageRenderer : NavigationRenderer
    {
        public static IEventAggregator EventAggregator { get; set; }

        bool _wiredUpEventHandler = false;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // set to default gray color
            this.NavigationBar.BarTintColor = BaseColorEnum.Gray.GetDarkColor().ToUIColor();
            this.NavigationBar.TintColor = UIColor.White;

            // Set status bar to have white status bar text
            UIApplication.SharedApplication.SetStatusBarStyle(UIStatusBarStyle.LightContent, false);
            this.SetNeedsStatusBarAppearanceUpdate();

            if (!_wiredUpEventHandler)
            {
                _wiredUpEventHandler = true;
                EventAggregator.GetEvent<ChangeColorEvent>().Subscribe(HandleChangeColorEvent);
            }
        }

        public override void ViewWillUnload()
        {
            base.ViewWillUnload();

            EventAggregator.GetEvent<ChangeColorEvent>().Unsubscribe(HandleChangeColorEvent);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            // NOTE:: there was a crash that could occur (Cannot access a disposed object) if the user pulled up a modal, 
            // closed it, and then the HandleChangeColorEvent event fired later. So we are manually un-wiring the event here.
            EventAggregator.GetEvent<ChangeColorEvent>().Unsubscribe(HandleChangeColorEvent);
        }

        void HandleChangeColorEvent(Color color)
        {
            this.NavigationBar.BarTintColor = color.ToUIColor();
        }
    }
}