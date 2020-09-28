using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FrontEnd.ViewModels
{
    public class TestViewModel
    {
        [Required]
        [Display(Name = "Nombre")]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Edad")]
        public string Age { get; set; }
    }
}