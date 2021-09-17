using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Notif.Backend.Models
{
    using System.ComponentModel.DataAnnotations;

    public class ResetPasswordViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string Token { get; set; }
    }
}
