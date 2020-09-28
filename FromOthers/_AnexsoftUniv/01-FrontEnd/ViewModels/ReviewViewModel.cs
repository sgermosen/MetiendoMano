using System.ComponentModel.DataAnnotations;

namespace FrontEnd.ViewModels
{
    public class ReviewViewModel
    {
        public int CourseId { get; set; }
        [Required]
        public string Comment { get; set; }
        [Required]
        public int Vote { get; set; }
    }
}
