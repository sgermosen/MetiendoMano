using System;

namespace LineDietXF.Interfaces
{
    public interface IAnalyticsService
    {
        void Initialize(string trackingID, string appName, int dispatchPeriodInSeconds);
        void TrackPageView(string pageName);
        void TrackEvent(string category, string action, string label, int value);
        void TrackEvent(string category, string action, int value);
        void TrackError(string text);
        void TrackFatalError(string text, Exception ex);
        void TrackFatalError(string text);
    }
}