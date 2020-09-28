using Foundation;
using Google.Analytics;
using LineDietXF.Interfaces;
using System;
using System.Diagnostics;

namespace LineDietXF.iOS.Services
{
    /// <summary>
    /// iOS implementation of Google Analytics for IAnalyticsService
    /// </summary>
    public class AnalyticsService : IAnalyticsService
    {
        // constant strings by iOS GA library
        const string AllowTrackingKey = "AllowTracking";
        const string AppEvent = "AppEvent";

        ITracker Tracker { get; set; }
        string TrackingID { get; set; }

        public void Initialize(string trackingID, string appName, int dispatchPeriodInSeconds)
        {
            TrackingID = trackingID;

            var optionsDict = NSDictionary.FromObjectAndKey(new NSString("YES"), new NSString(AllowTrackingKey));
            NSUserDefaults.StandardUserDefaults.RegisterDefaults(optionsDict);

            Gai.SharedInstance.OptOut = !NSUserDefaults.StandardUserDefaults.BoolForKey(AllowTrackingKey);

            Gai.SharedInstance.DispatchInterval = dispatchPeriodInSeconds;
            Gai.SharedInstance.TrackUncaughtExceptions = true;

            Tracker = Gai.SharedInstance.GetTracker(appName, TrackingID);
        }
        public void TrackPageView(string pageName)
        {
            try
            {
                Debug.WriteLine($"{nameof(TrackPageView)}({pageName})");

                Gai.SharedInstance.DefaultTracker.Set(GaiConstants.ScreenName, pageName);
                Gai.SharedInstance.DefaultTracker.Send(DictionaryBuilder.CreateScreenView().Build());
            }
            catch (Exception ex)
            {
                if (Debugger.IsAttached)
                    Debugger.Break();

                Debug.WriteLine(ex);
            }
        }

        public void TrackEvent(string category, string action, string label, int value)
        {
            try
            {
                Debug.WriteLine($"{nameof(TrackEvent)}({category}, {action}, {label}, {value})");

                Gai.SharedInstance.DefaultTracker.Send(DictionaryBuilder.CreateEvent(category, action, label, value).Build());
                Gai.SharedInstance.Dispatch(); // Manually dispatch the event immediately       
            }
            catch (Exception ex)
            {
                if (Debugger.IsAttached)
                    Debugger.Break();

                Debug.WriteLine(ex);
            }
        }

        public void TrackEvent(string category, string action, int value)
        {
            TrackEvent(category, action, string.Empty, value);
        }

        public void TrackError(string text)
        {
            TrackError(text, false);
        }

        public void TrackFatalError(string text, Exception ex)
        {
            TrackError($"{text}{Environment.NewLine}{ex}", true);
        }

        public void TrackFatalError(string text)
        {
            TrackError(text, true);
        }

        void TrackError(string errorText, bool isFatal)
        {
            if (Debugger.IsAttached)
                Debugger.Break(); // break here so we know an error has occurred while debugging

            try
            {
                Debug.WriteLine($"{nameof(TrackError)}({errorText})");
                Gai.SharedInstance.DefaultTracker.Send(DictionaryBuilder.CreateException(errorText, isFatal).Build());
            }
            catch (Exception ex)
            {
                if (Debugger.IsAttached)
                    Debugger.Break();

                Debug.WriteLine(ex);
            }
        }
    }
}