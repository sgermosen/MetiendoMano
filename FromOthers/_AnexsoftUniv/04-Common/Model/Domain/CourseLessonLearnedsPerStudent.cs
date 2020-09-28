using Common;
using Common.CustomFilters;
using Model.Auth;
using Model.Helper;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Domain
{
    public class CourseLessonLearnedsPerStudent : AuditEntity, ISoftDeleted
    {
        public int Id { get; set; }

        public LessonsPerCourse Lesson { get; set; }
        [Required]
        public int LessonId { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
        [Required]
        public string UserId { get; set; }

        public bool Deleted { get; set; }
    }
}
