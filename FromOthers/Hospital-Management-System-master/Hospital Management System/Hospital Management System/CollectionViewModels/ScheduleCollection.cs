using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hospital_Management_System.Models;

namespace Hospital_Management_System.CollectionViewModels
{
    public class ScheduleCollection
    {
        public Schedule Schedule { get; set; }
        public IEnumerable<Doctor> Doctors { get; set; }
    }
}