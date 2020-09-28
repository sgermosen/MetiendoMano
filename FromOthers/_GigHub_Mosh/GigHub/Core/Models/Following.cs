
namespace GigHub.Core.Models
{
    // Alternatively, this class could be called Relationship.
    public class Following
    {
        public string FollowerId { get; set; }

        public string FolloweeId { get; set; }

        public ApplicationUser Follower { get; set; }

        public ApplicationUser Followee { get; set; }
    }
}