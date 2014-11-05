using System;
using LFMS.Models.DAO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Ajax.Utilities;
using LFMS.Utilities;

namespace LFMS.Models.BLO
{
    public class UserControlBLO
    {
        private UserControlDAO userControlDAO;

        public UserControlBLO()
        {
            userControlDAO = new UserControlDAO();
        }

        public List<OperationalEvent> GetOperationEventByStaffId(int staffId)
        {
            return userControlDAO.GetOperationEventByStaffId(staffId);
        }

        public List<CalendarEvent> GetCalendarEventByStaffId(int staffId)
        {
            return userControlDAO.GetCalendarEventByStaffId(staffId);
        }
    }
}
