using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FrontEnd.ViewModels
{
    public class LessonOrderViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int Order { get; set; }
    }

    public class LessonCreateViewModel
    {
        [Required]
        public int CourseId { get; set; }

        [Required]
        public string Name { get; set; }
    }

    public class LessonUpdateViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public int Order { get; set; }

        public string Video { get; set; }
    }
}
