using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hospital_Management_System.Models
{
    public class Doctor
    {
        public int Id { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
        public string ApplicationUserId { get; set; }

        public string FullName { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [Required]
        public string Designation { get; set; }

        public Department Department { get; set; }
        [Display(Name = "Department")]
        public int DepartmentId { get; set; }

        public string Address { get; set; }
        [Display(Name = "Phone No")]
        public string PhoneNo { get; set; }

        [Required]
        [Display(Name = "Mobile No")]
        public string ContactNo { get; set; }

        [Required]
        public string Specialization { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        [Display(Name = "Blood Group")]
        public string BloodGroup { get; set; }

        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DateOfBirth { get; set; }

        [Display(Name = "Education/Degree")]
        public string Education { get; set; }

        [Required]
        public string Status { get; set; }

    }
}