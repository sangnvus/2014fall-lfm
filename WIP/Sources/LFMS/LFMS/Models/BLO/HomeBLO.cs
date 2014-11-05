using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LFMS.Models.DAO;
using LFMS.Utilities;
using Microsoft.Ajax.Utilities;
using System.Globalization;
 
namespace LFMS.Models.BLO
{
    public class HomeBLO
    {
        private StaffDAO staffDAO;
        public HomeDAO homeDAO;

        public HomeBLO()
        {
            staffDAO = new StaffDAO();
            homeDAO = new HomeDAO();
        }

        public List<Object> GetAllStaffCalendar(int staffId, string start, string end)
        {
            DateTime startDt = DateTime.ParseExact(start, "MM/dd/yyyy HH:mm:ss", null);
            DateTime endDt = DateTime.ParseExact(end, "MM/dd/yyyy HH:mm:ss", null);
            return homeDAO.GetAllStaffCalendar(staffId, startDt, endDt);
        }

        public List<Object> GetAllStaffCalendarInOE(int staffId, string start, string end)
        {
            DateTime startDt = DateTime.ParseExact(start, "MM/dd/yyyy HH:mm:ss", null);
            DateTime endDt = DateTime.ParseExact(end, "MM/dd/yyyy HH:mm:ss", null);
            return homeDAO.GetAllStaffCalendarInOE(staffId, startDt, endDt);
        }

        public int AddCalendarEvent(int staffId, string title, string start, string end, string className)
        {
            if (staffId != 0 && !title.IsNullOrWhiteSpace() && !start.IsNullOrWhiteSpace() && !end.IsNullOrWhiteSpace() && !className.IsNullOrWhiteSpace()) 
            {
                DateTime startDt = DateTime.ParseExact(start, "MM/dd/yyyy HH:mm:ss", null);
                DateTime endDt = DateTime.ParseExact(end, "MM/dd/yyyy HH:mm:ss", null);
                int result = homeDAO.AddCalendarEvent(staffId, title, startDt, endDt, className);

                return result;
            }
            return 0;
        }

        public string UpdateCalendarEvent(int Id, string title, string start, string end, string className, string type)
        {
            if (Id != 0 && !title.IsNullOrWhiteSpace() && !start.IsNullOrWhiteSpace() && !end.IsNullOrWhiteSpace() && !className.IsNullOrWhiteSpace())
            {
                DateTime startDt = DateTime.ParseExact(start, "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                DateTime endDt = DateTime.ParseExact(end, "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                bool result = homeDAO.UpdateCalendarEvent(Id, title, startDt, endDt, className, type);
                if (result)
                {
                    return "success";
                }
                return "fail";
            }
            return "fail";
        }

        public string DeleteCalendarEvent(int calendarId, string type)
        {
            bool result = homeDAO.DeleteCalendarEvent(calendarId, type);
            if(result)
            {
                return "success";
            }
            return "fail";
        }

        public List<Object> GetAllSelectStaff(int staffId)
        {
            return homeDAO.GetAllSelectStaff(staffId);
        }

        public int GetStaffIdByCalId(int calId,string type)
        {
            return homeDAO.getStaffIdByCalId(calId,type);
        }
    }
}
