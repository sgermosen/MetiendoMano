using System.ComponentModel.DataAnnotations;

namespace Auth.Models
{
    public class RegisterViewModel
    {
        [Required, MaxLength(20)]
        public string Name { get; set; }
        [Required, MaxLength(50)]
        public string Lastname { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
