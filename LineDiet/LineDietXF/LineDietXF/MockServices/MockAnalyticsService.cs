using LineDietXF.Interfaces;
using System;
using System.Diagnostics;

namespace LineDietXF.MockServices
{
    public class MockAnalyticsService : IAnalyticsService
    {
        public MockAnalyticsService()
        {
        }

        public void Initialize(string trackingID, string appName, int dispatchPeriodInSeconds)
        {
        }

        public void TrackEvent(string category, string action, int value)
        {
            Debug.WriteLine($"MockAnalyticsService.TrackEvent({category}, {action}, {value})");
        }

        public void TrackEvent(string category, string action, string label, int value)
        {
            Debug.WriteLine($"MockAnalyticsService.TrackEvent({category}, {action}, {label}, {value})");
        }

        public void TrackError(string text)
        {
            Debug.WriteLine($"MockAnalyticsService.TrackError({text})");
        }

        public void TrackFatalError(string text, Exception ex)
        {
            Debug.WriteLine($"MockAnalyticsService.TrackFatalError({text}, {ex})");
        }

        public void TrackFatalError(string text)
        {
            TrackFatalError(text, null);
        }

        public void TrackPageView(string pageName)
        {
            Debug.WriteLine($"MockAnalyticsService.TrackPageView({pageName})");
        }
    }
}