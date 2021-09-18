using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hospital_Management_System.Models;

namespace Hospital_Management_System.CollectionViewModels
{
    public class CollectionOfAll
    {
        public IEnumerable<Ambulance> Ambulances { get; set; }
        public IEnumerable<Department> Departments { get; set; }
        public IEnumerable<Doctor> Doctors { get; set; }
        public IEnumerable<Patient> Patients { get; set; }
        public IEnumerable<Medicine> Medicines { get; set; }
        public IEnumerable<Appointment> ActiveAppointments { get; set; }
        public IEnumerable<Appointment> PendingAppointments { get; set; }
        public IEnumerable<AmbulanceDriver> AmbulanceDrivers { get; set; }
        public IEnumerable<Announcement> Announcements { get; set; }
    }
}