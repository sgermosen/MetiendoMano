using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cabanas.API.Models.ViewModels
{
    public class UserDevice
    {
        [Required]
        public string Imei { get; set; }
    }
}
