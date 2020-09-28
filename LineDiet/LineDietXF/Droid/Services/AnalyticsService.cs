using Android.Content;
using Android.Gms.Analytics;
using LineDietXF.Interfaces;
using System;
using System.Diagnostics;

namespace LineDietXF.Droid.Services
{
    /// <summary>
    /// Android implementation of Google Analytics for IAnalyticsService
    /// </summary>
    public class AnalyticsService : IAnalyticsService
    {
        Context _appContext;

        static GoogleAnalytics GAInstance;
        static Tracker GATracker;

        string TrackingID { get; set; }

        public AnalyticsService(Context appContext)
        {
            _appContext = appContext;
        }

        public void Initialize(string trackingID, string appName, int dispatchPeriodInSeconds)
        {
            TrackingID = trackingID;

            GAInstance = GoogleAnalytics.GetInstance(_appContext);
            GAInstance.SetLocalDispatchPeriod(dispatchPeriodInSeconds);

            GATracker = GAInstance.NewTracker(TrackingID);
            GATracker.EnableExceptionReporting(true);
            GATracker.EnableAdvertisingIdCollection(true);
        }

        public void TrackPageView(string pageName)
        {
            try
            {
                Debug.WriteLine($"{nameof(TrackPageView)}({pageName})");

                GATracker.SetScreenName(pageName);
                GATracker.Send(new HitBuilders.ScreenViewBuilder().Build());
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

                HitBuilders.EventBuilder builder = new HitBuilders.EventBuilder();
                builder.SetCategory(category);
                builder.SetAction(action);
                builder.SetLabel(label);
                builder.SetValue(value);

                GATracker.Send(builder.Build());
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

        void TrackError(string errorText, bool isFatal)
        {
            if (Debugger.IsAttached)
                Debugger.Break(); // break here so we know an error has occurred while debugging

            try
            {
                Debug.WriteLine($"{nameof(TrackError)}({errorText})");

                HitBuilders.ExceptionBuilder builder = new HitBuilders.ExceptionBuilder();
                builder.SetDescription(errorText);
                builder.SetFatal(isFatal);

                GATracker.Send(builder.Build());
            }
            catch (Exception ex)
            {
                if (Debugger.IsAttached)
                    Debugger.Break();

                Debug.WriteLine(ex);
            }
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
    }
}