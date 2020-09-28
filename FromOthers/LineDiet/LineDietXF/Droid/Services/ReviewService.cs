using Android.Content;
using LineDietXF.Interfaces;
using System;

namespace LineDietXF.Droid.Services
{
    /// <summary>
    /// Google Play specific IReviewService for leaving a review
    /// </summary>
    public class ReviewService : IReviewService
    {
        Context Context { get; set; }
        string PackageName { get; set; }

        public ReviewService(Context context, string packageName)
        {
            Context = context;
            PackageName = packageName;
        }

        public bool LeaveAReview()
        {
            Intent intent = new Intent(Intent.ActionView);
            intent.SetData(Android.Net.Uri.Parse($"market://details?id={PackageName}"));
            intent.AddFlags(ActivityFlags.NoHistory | ActivityFlags.NewDocument | ActivityFlags.MultipleTask);

            try
            {
                Context.StartActivity(intent);
                return true;
            }
            catch (Exception ex)
            {
                MainActivity.AnalyticsService.TrackFatalError($"{nameof(LeaveAReview)} - an exception occurred launching the review activity.", ex);
                return false;
            }
        }
    }
}