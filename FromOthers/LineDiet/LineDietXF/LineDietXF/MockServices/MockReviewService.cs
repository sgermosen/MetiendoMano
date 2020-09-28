using LineDietXF.Interfaces;

namespace LineDietXF.MockServices
{
    public class MockReviewService : IReviewService
    {
        public bool LeaveAReview()
        {
            return true;
        }
    }
}