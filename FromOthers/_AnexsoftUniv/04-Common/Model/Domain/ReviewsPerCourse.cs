using Common;
using Common.CustomFilters;
using Model.Auth;
using Model.Helper;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Domain
{
    public class ReviewsPerCourse : AuditEntity, ISoftDeleted
    {
        public int Id { get; set; }
        [Required]
        public decimal Vote { get; set; }
        [Required]
        public string Comment { get; set; }

        public Course Course { get; set; }
        [Required]
        public int CourseId { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
        [Required]
        public string UserId { get; set; }

        public bool Deleted { get; set; }
    }
}
