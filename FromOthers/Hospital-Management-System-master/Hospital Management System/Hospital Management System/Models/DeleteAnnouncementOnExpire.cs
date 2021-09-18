using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace Hospital_Management_System.Models
{
    public static class DeleteAnnouncementOnExpire
    {
        private static ApplicationDbContext dbContext = new ApplicationDbContext();

        public static void DeleteAnnouncement()
        {
            var date = DateTime.Now.Date;
            dbContext.Announcements.Where(c => c.End < date)
                .ToList().ForEach(p => dbContext.Announcements.Remove(p));
            dbContext.Appointments.Where(c => c.AppointmentDate < date)
                .ToList().ForEach(p => dbContext.Appointments.Remove(p));
            dbContext.SaveChanges();
        }
    }
}