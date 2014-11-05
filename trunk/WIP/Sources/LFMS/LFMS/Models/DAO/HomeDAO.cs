using LFMS.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web; 

namespace LFMS.Models.DAO
{
    public class HomeDAO
    {
        LFMSEntities db;

        public HomeDAO()
        {
            db = new LFMSEntities();
        }

        public List<CalendarEvent> GetAllCalendarEvent()
        {
            var calendarList = db.CalendarEvents.ToList();
            return calendarList;
        }
        public CalendarEvent GetCalendarEventById(int id)
        {
            var calEvent = db.CalendarEvents.Where(e => e.CalendarEventId == id).FirstOrDefault();
            return calEvent;
        }

        public List<Object> GetAllStaffCalendar(int staffId, DateTime startDt, DateTime endDt)
        {
            var calendarList = db.CalendarEvents.Where(c => c.BeginTime <= endDt && c.EndTime >= startDt && c.StaffId == staffId).ToList();
            var level1 = new List<string>();
            level1.Add("Staffs");
            var list = new List<object>();
            foreach (var cal in calendarList)
            {
                var result = UtilityClass.ConvertDynamicObjectWithFullAttr(cal, level1);
                list.Add(result);
            }
            return list;
        }

        public List<Object> GetAllStaffCalendarInOE(int staffId, DateTime startDt, DateTime endDt)
        {
            var oEventList = db.OperationalEvents.Where(o => o.BeginTime != null && o.BeginTime <= endDt && o.EndTime >= startDt && o.CreatorId == staffId).ToList();
            var level1 = new List<string>();
            level1.Add("Case");
            var list = new List<object>();
            foreach (var cal in oEventList)
            {
                var result = UtilityClass.ConvertDynamicObjectWithFullAttr(cal, level1);
                list.Add(result);
            }
            return list;
        }


        public List<Object> GetAllSelectStaff(int staffId)
        {
            var staffList = db.Staffs.Where(c => c.StaffId != staffId).ToList();
            var list = new List<object>(); 
            var level1 = new List<string>();
            level1.Add("Accounts");
            var except = new List<string>();
            except.Add("Password");
            foreach (var staff in staffList)
            {
                var result = UtilityClass.ConvertDynamicObjectWithCustomAttr(staff,except,level1);
                list.Add(result);
            }
            return list;
        }
        public int AddCalendarEvent(int staffId, string title, DateTime startTime, DateTime endTime, string className)
        {
            CalendarEvent cal = new CalendarEvent();
            cal.StaffId = staffId;
            cal.Title = title;
            cal.BeginTime = startTime;
            cal.EndTime = endTime;
            cal.Priority = className;
            try
            {
                db.CalendarEvents.Add(cal);
                db.SaveChanges();
                return cal.CalendarEventId;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return 0;
            }
        }

        public bool UpdateCalendarEvent(int id, string title, DateTime startTime, DateTime endTime, string className, string type)
        {
            if(type.Equals("CE"))
            {
                CalendarEvent cal = GetCalendarEventById(id);

                cal.Title = title;
                cal.BeginTime = startTime;
                cal.EndTime = endTime;
                cal.Priority = className;
                try
                {
                    db.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return false;
                }
            }
            else if (type.Equals("OE"))
            {
                OperationalEvent ope = db.OperationalEvents.Where(o => o.OperationalEventId == id).FirstOrDefault();
                ope.Title = title;
                ope.BeginTime = startTime;
                ope.EndTime = endTime;
                try
                {
                    db.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return false;
                }
            }
            return false;
        }

        public bool DeleteCalendarEvent(int calendarId, string type)
        {
            if (type.Equals("CE"))
            {
                CalendarEvent cal = GetCalendarEventById(calendarId);
                try
                {
                    db.CalendarEvents.Remove(cal);
                    db.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return false;
                }
            }
            else if(type.Equals("OE"))
            {
                OperationalEvent ope = db.OperationalEvents.Where(o => o.OperationalEventId == calendarId).FirstOrDefault();
                try
                {
                    db.OperationalEvents.Remove(ope);
                    db.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return false;
                }
            }
            return false;
        }

        public int getStaffIdByCalId(int calId, string type)
        {
            try
            {
                if (type.Equals("CE"))
                {
                    var cal = new CalendarEvent();
                    cal = db.CalendarEvents.Where(cc => cc.CalendarEventId == calId).FirstOrDefault();
                    return cal.StaffId;
                }
                else if (type.Equals("OE"))
                {
                    var cal = new OperationalEvent();
                    cal = db.OperationalEvents.Where(cc => cc.OperationalEventId == calId).FirstOrDefault();
                    return cal.CreatorId;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return 0;
            }
            return 0;
        }
    }
}