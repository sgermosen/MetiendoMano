using LineDietXF.Enumerations;
using LineDietXF.Events;
using LineDietXF.Extensions;
using Prism.Events;

namespace LineDietXF.Services
{
    /// <summary>
    /// This service is responsible for updating the colors within the app. This is done by updating App resources as well as throwing
    /// the ChangeColorEvent event which is consumed at the platform level (ex: Android/iOS)
    /// </summary>
    public class WindowColorService : IWindowColorService
    {
        IEventAggregator EventAggregator { get; set; }

        public WindowColorService(IEventAggregator eventAggregator)
        {
            EventAggregator = eventAggregator;
        }

        public void ChangeAppBaseColor(BaseColorEnum colorEnum)
        {
            var lightColor = colorEnum.GetLightColor();
            var darkColor = colorEnum.GetDarkColor();

            App.Current.Resources["PrimaryResultColor"] = darkColor;
            App.Current.Resources["PrimaryFillColor"] = lightColor;

            EventAggregator.GetEvent<ChangeColorEvent>().Publish(darkColor);
        }

        public void ResetAppBaseColor()
        {
            ChangeAppBaseColor(BaseColorEnum.Gray);
        }
    }
}