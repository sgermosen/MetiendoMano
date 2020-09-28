using LineDietXF.Enumerations;
using LineDietXF.Events;
using LineDietXF.Extensions;
using LineDietXF.iOS.Renderers;
using Prism.Events;
using System;
using System.Linq;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(TabbedPage), typeof(CustomTabbedPageRenderer))]
namespace LineDietXF.iOS.Renderers
{
    /// <summary>
    /// This renderer is responsible for styling the looks of the iOS tab bar, including listening for the ChangeColorEvent and updating its colors
    /// NOTE:: This renderer assumes that all tabs have icons, and similarly named icon files appended with "_unselected", otherwise it will crash
    /// </summary>
    public class CustomTabbedPageRenderer : TabbedRenderer
    {
        const string UnselectedFilenameExtension = "_unselected"; // appended to TabBar.Icon's filename for unselected image
        const int ImageVerticalShift_iPhone = 4;
        const int ImageVerticalShift_iPad = 6;

        public static IEventAggregator EventAggregator { get; set; }

        bool _wiredUpEventHandler = false;

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            var tabbedPage = (TabbedPage)this.Element; // parent containing all tabs

            if (tabbedPage.Children.Count != TabBar.Items.Count())
                throw new InvalidProgramException($"{nameof(CustomTabbedPageRenderer)} found instance were TabbedPage count didn't match native TabBar tab count!");

            // iterate through all of the Xamarin.Forms TabBar items, get a handle on the .Icon used, and use it as a base for setting iOS's UITabBarItem.Image
            // so that there can be different values for UITabBarItem.Image and UITabBarItem.SelectedImage
            var imageOffset = Device.Idiom == TargetIdiom.Phone ? ImageVerticalShift_iPhone : ImageVerticalShift_iPad;
            for (int i = 0; i < TabBar.Items.Count(); i++)
            {
                var tab = TabBar.Items[i];
                var tabPage = tabbedPage.Children[i];

                var iconFilename = tabPage.Icon.File + UnselectedFilenameExtension;
                var tabImageUnselected = new UIImage(iconFilename).ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal);

                tab.Image = tabImageUnselected; // set unselected image to semi-transparent version (same filename, appended with "_unselected")                
                tab.ImageInsets = new UIEdgeInsets(imageOffset, 0, -imageOffset, 0); // shift the icon down as we aren't showing text labels for tabs    
            }
        }

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            // Set default color to dark gray
            var darkGray = BaseColorEnum.Gray.GetDarkColor().ToUIColor();
            TabBar.TintColor = UIColor.White; // selected icon color
            TabBar.BarTintColor = darkGray; // bar fill color
            TabBar.BackgroundColor = UIColor.Purple; // unknown
            TabBar.Translucent = false;

            if (!_wiredUpEventHandler)
            {
                _wiredUpEventHandler = true;
                EventAggregator.GetEvent<ChangeColorEvent>().Subscribe(HandleChangeColorEvent);
            }
        }

        void HandleChangeColorEvent(Color color)
        {
            TabBar.BarTintColor = color.ToUIColor();
        }
    }
}