using System;
using System.ComponentModel.DataAnnotations;
using Notif.Backend.Data.Entities;

namespace Notif.Backend.Models
{
    public class Reaction:IEntity
    {
        public long Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string Email { get; set; }

        [Required]
        [MaxLength(500)]
        public string Observation { get; set; }

        [MaxLength(100)]
        public string ImageUrl { get; set; }

        [MaxLength(100)]
        public string SoundUrl { get; set; }

        [MaxLength(100)]
        public string VideoUrl { get; set; }
        
        public int Punctuation { get; set; }

        public double? Longitude { get; set; }

        public double? Latitude { get; set; }

        public bool Private { get; set; }

        [Display(Name = "Fecha")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm tt}", ApplyFormatInEditMode = false)]
        public DateTime Date { get; set; }

        [Display(Name = "Fecha")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm tt}", ApplyFormatInEditMode = false)]
        public DateTime? DateLocal => this.Date.ToLocalTime();

       // [Required]
        public ApplicationUser ApplicationUser { get; set; }

    }
}
