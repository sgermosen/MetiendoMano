using Foundation;
using LineDietXF.Interfaces;
using UIKit;

namespace LineDietXF.iOS.Services
{
    /// <summary>
    /// iTunes App Store specific IReviewService for leaving a review
    /// </summary>
    public class ReviewService : IReviewService
    {
        public bool LeaveAReview()
        {
            string reviewUrlString = $"itms-apps://itunes.apple.com/app/id{Constants.iTunesAppID}";

            var reviewUrl = new NSUrl(reviewUrlString);
            if (!UIApplication.SharedApplication.CanOpenUrl(reviewUrl))
                return false;

            UIApplication.SharedApplication.OpenUrl(reviewUrl);
            return true;
        }
    }
}