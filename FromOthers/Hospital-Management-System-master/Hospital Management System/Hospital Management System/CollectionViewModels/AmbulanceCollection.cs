using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hospital_Management_System.Models;

namespace Hospital_Management_System.CollectionViewModels
{
    public class AmbulanceCollection
    {
        public Ambulance Ambulance { get; set; }
        public List<AmbulanceDriver> AmbulanceDrivers { get; set; }

        public string Title
        {
            get
            {
                if (Ambulance != null && Ambulance.Id != 0)
                {
                    return "Edit Ambulance";
                }

                return "Add Ambulance";
            }
        }
    }
}