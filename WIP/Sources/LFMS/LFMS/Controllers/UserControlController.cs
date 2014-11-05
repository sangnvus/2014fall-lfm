using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Providers.Entities;
using LFMS.Models.BLO;
using LFMS.Utilities;
using Microsoft.Ajax.Utilities;

namespace LFMS.Controllers
{
    public class UserControlController : BaseController
    {
        UserControlBLO userControlBLO = new UserControlBLO();

        public ActionResult _UserControl()
        {
            return PartialView();
        }

        [HttpPost]
        public JsonResult GetCalendarsByStaffId(int staffId)
        {
            var list = new List<Object>();

            List<OperationalEvent> opera = userControlBLO.GetOperationEventByStaffId(staffId);
            var level1 = new List<string>();
            level1.Add("Case");
            foreach (var ca in opera)
            {
                var result = UtilityClass.ConvertDynamicObjectWithFullAttr(ca, level1);
                list.Add(result);
            }

            List<CalendarEvent> calendar = userControlBLO.GetCalendarEventByStaffId(staffId);
            foreach (var ca in calendar)
            {
                var result = UtilityClass.ConvertDynamicObjectWithFullAttr(ca);
                list.Add(result);
            }

            return Json(list, JsonRequestBehavior.AllowGet);
        }
      
        public String UpdateStaffPass()
        {
            var staffBLO = new StaffBLO();

            int staffId = int.Parse(Session["StaffId"].ToString());
            string oldpassword = Request.Params["oldPassword"].Trim();
            string password2 = Request.Params["staffPassword2"].Trim();

            if (staffBLO.CheckPassword(staffId, oldpassword))
            {
                bool result = staffBLO.UpdateStaffPass(staffId, password2);
                if (result)
                return "successful";
            }
             return "notMatch";
        }
    }
}
