using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LFMS.Models.BLO;
using LFMS.Utilities;
using Microsoft.Ajax.Utilities;

namespace LFMS.Controllers
{
    public class HomeController : BaseController
    {
        CaseBLO caseBLO = new CaseBLO();
        HomeBLO homeBLO = new HomeBLO();
        OfficeBLO officeBLO = new OfficeBLO();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetAssignedCases()
        {
            var assignCase = caseBLO.GetAssignedCases(int.Parse(Session["StaffId"].ToString()));

            if (int.Parse(Session["RoleId"].ToString()) == 1)
            {
                var outOffice = new List<Object>();
                List<Office> officeList = officeBLO.GetActiveOffice();
                foreach (var off in officeList)
                {
                    var office = UtilityClass.ConvertDynamicObjectWithFullAttr(off);
                    outOffice.Add(office);
                }
                return Json(new { outOffice, assignCase }, JsonRequestBehavior.AllowGet);

            }
            else
            {
                var outOffice = officeBLO.GetOfficesByStaffId(int.Parse(Session["StaffId"].ToString()));
                return Json(new { outOffice, assignCase }, JsonRequestBehavior.AllowGet);
            }
        }

        //lay lich lam viec cua User dang dang nhap (Session)
        public JsonResult JsonCalendarEvent()
        {
            string start = Request.Params["start"];
            string end = Request.Params["end"];
            int staffId = int.Parse(Session["StaffId"].ToString());
            var listCE = homeBLO.GetAllStaffCalendar(staffId, start, end);
            var listOE = homeBLO.GetAllStaffCalendarInOE(staffId, start, end);
            return Json(new { listCE, listOE }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public String AddCalendarEvent()
        {
            int staffId = int.Parse(Session["StaffId"].ToString());
            string title = Request.Params["title"];
            string start = Request.Params["start"];
            string end = Request.Params["end"];
            string className = Request.Params["className"];
            return homeBLO.AddCalendarEvent(staffId, title, start, end, className) + "";
        }

        [HttpPost]
        public String UpdateCalendarEvent()
        {
            int calId = int.Parse(Request.Params["id"]);
            string title = Request.Params["title"];
            string start = Request.Params["start"];
            string end = Request.Params["end"];
            string className = Request.Params["className"];
            string type = Request.Params["type"];

            int creator = homeBLO.GetStaffIdByCalId(calId, type);
            int staffId = int.Parse(Session["StaffId"].ToString());
            if (creator == staffId)
            {
                return homeBLO.UpdateCalendarEvent(calId, title, start, end, className, type);
            }
            return "fail";
        }

        [HttpPost]
        public String DeleteCalendarEvent()
        {
            int calendarId = int.Parse(Request.Params["calendarId"]);
            string type = Request.Params["type"];

            int creator = homeBLO.GetStaffIdByCalId(calendarId, type);
            int staffId = int.Parse(Session["StaffId"].ToString());
            if (creator == staffId)
            {
                return homeBLO.DeleteCalendarEvent(calendarId, type);
            }
            return "fail";
        }

        public JsonResult GetAllStaffCalendar()
        {
            string staffId = (Request.Params["staffId"]);
            string start = Request.Params["start"];
            string end = Request.Params["end"];
            if (!staffId.IsNullOrWhiteSpace() && !start.IsNullOrWhiteSpace() && !end.IsNullOrWhiteSpace())
            {
                int id = int.Parse(staffId);
                var listCE = homeBLO.GetAllStaffCalendar(id, start, end);
                var listOE = homeBLO.GetAllStaffCalendarInOE(id, start, end);
                return Json(new { listCE, listOE }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { }, JsonRequestBehavior.AllowGet); ;
        }

        public JsonResult GetAllSelectStaff()
        {
            int staffId = int.Parse(Session["StaffId"].ToString());
            var result = homeBLO.GetAllSelectStaff(staffId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
