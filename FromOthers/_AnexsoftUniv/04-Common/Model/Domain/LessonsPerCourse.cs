using Common;
using Common.CustomFilters;
using Model.Helper;
using System.ComponentModel.DataAnnotations;

namespace Model.Domain
{
    public class LessonsPerCourse : AuditEntity, ISoftDeleted
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Content { get; set; }
        public string Video { get; set; }

        public Course Course { get; set; }
        [Required]
        public int CourseId { get; set; }

        public int Order { get; set; }

        public bool Deleted { get; set; }
    }
}
