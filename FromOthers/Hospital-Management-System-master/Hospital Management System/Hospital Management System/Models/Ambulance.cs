using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hospital_Management_System.Models
{
    public class Ambulance
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Ambulance Id")]
        public string AmbulanceId { get; set; }       
        
        [Display(Name = "Ambulance Status")]
        public string AmbulanceStatus { get; set; }

        public AmbulanceDriver AmbulanceDriver { get; set; }

        public int AmbulanceDriverId { get; set; }
    }
}