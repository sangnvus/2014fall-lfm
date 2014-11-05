using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LFMS.Models.DAO
{
    public class UserControlDAO
    {
        LFMSEntities db;

        public UserControlDAO()
        {
            db = new LFMSEntities();
        }

        public List<OperationalEvent> GetOperationEventByStaffId(int staffId)
        {
            DateTime today = DateTime.Now.Date;
            DateTime tomorrow = DateTime.Now.AddDays(1).Date;

            var opera = db.OperationalEvents.Where(o => o.CreatorId == staffId && o.BeginTime < tomorrow && o.EndTime >= today).ToList();
            return opera;
        }

        public List<CalendarEvent> GetCalendarEventByStaffId(int staffId)
        {
            DateTime today = DateTime.Now.Date;
            DateTime tomorrow = DateTime.Now.AddDays(1).Date;

            var calendar = db.CalendarEvents.Where(c => c.StaffId == staffId && c.BeginTime < tomorrow && c.EndTime >= today).ToList();
            return calendar;
        }
    }
}
