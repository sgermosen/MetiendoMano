using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Microsoft.Ajax.Utilities;

namespace Hospital_Management_System.Models
{
    public class Prescription
    {
        public int Id { get; set; }

        public Doctor Doctor { get; set; }
        public int DoctorId { get; set; }

        public string DoctorName { get; set; }
        public string DoctorSpecialization { get; set; }

        public Patient Patient { get; set; }
        [Display(Name = "Patient Name")]
        public int PatientId { get; set; }

        public string UserName { get; set; }
        public string PatientName { get; set; }
        public string PatientGender { get; set; }
        public string PatientAge { get; set; }

        [Display(Name = "Medical Tests")]
        public string MedicalTest1 { get; set; }
        public string MedicalTest2 { get; set; }
        public string MedicalTest3 { get; set; }
        public string MedicalTest4 { get; set; }

        [Display(Name = "Medicine")]
        public string Medicine1 { get; set; }
        [Display(Name = " ")]
        public bool Morning1 { get; set; }
        public bool Afternoon1 { get; set; }
        public bool Evening1 { get; set; }

        public string Medicine2 { get; set; }
        public bool Morning2 { get; set; }
        public bool Afternoon2 { get; set; }
        public bool Evening2 { get; set; }

        public string Medicine3 { get; set; }
        public bool Morning3 { get; set; }
        public bool Afternoon3 { get; set; }
        public bool Evening3 { get; set; }

        public string Medicine4 { get; set; }
        public bool Morning4 { get; set; }
        public bool Afternoon4 { get; set; }
        public bool Evening4 { get; set; }

        public string Medicine5 { get; set; }
        public bool Morning5 { get; set; }
        public bool Afternoon5 { get; set; }
        public bool Evening5 { get; set; }

        public string Medicine6 { get; set; }
        public bool Morning6 { get; set; }
        public bool Afternoon6 { get; set; }
        public bool Evening6 { get; set; }

        public string Medicine7 { get; set; }
        public bool Morning7 { get; set; }
        public bool Afternoon7 { get; set; }
        public bool Evening7 { get; set; }

        [Display(Name = "Checkup After Days")]
        public int CheckUpAfterDays { get; set; }

        public DateTime PrescriptionAddDate { get; set; }

        public string DoctorTiming { get; set; }

    }
}