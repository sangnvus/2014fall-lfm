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
    public class StaffController : AdminController
    {
        StaffBLO staffBLO = new StaffBLO();
        //
        // GET: /Staff/

        public ActionResult Staff()
        {
            return View();
        }
        public JsonResult GetStaff()
        {
            var list = staffBLO.GetAllStaffJson();
            return Json(new { html = RenderPartialViewToString("_AllStaff", null), list }, JsonRequestBehavior.AllowGet);

        }


        public JsonResult ListStaffRole()
        {
            var list = staffBLO.GetAllRoleJson();

            return Json(list, JsonRequestBehavior.AllowGet);
        }
        //public JsonResult GetRoles()
        //{
        //    var list = staffBLO.GetAllRoleJson();
        //    return Json(new { html = RenderPartialViewToString("_Role", null), list }, JsonRequestBehavior.AllowGet);

        //}

        public JsonResult ListStaffGroup()
        {
            var list = staffBLO.GetAllGroupJson();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ListAllStaff()
        {
            var list = staffBLO.ListAllStaff();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetGroups()
        {
            var list = staffBLO.GetAllGroupJson();
            return Json(new { html = RenderPartialViewToString("_Group", null), list }, JsonRequestBehavior.AllowGet);

        }
        public JsonResult ListRoleGroup()
        {
            var db = new LFMSEntities();
            var role = db.Roles.ToList();
            var list = new List<object>();
            foreach (var rolerole in role)
            {
                var result = UtilityClass.ConvertDynamicObjectWithFullAttr(rolerole);
                list.Add(result);
            }

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        private string RenderPartialViewToString(string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
            {
                viewName = this.ControllerContext.RouteData.GetRequiredString("action");
            }

            this.ViewData.Model = model;
            var sw = new StringWriter();
            ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(this.ControllerContext, viewName);
            var viewContext = new ViewContext(this.ControllerContext, viewResult.View, this.ViewData, this.TempData, sw);
            viewResult.View.Render(viewContext, sw);
            viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
            return sw.GetStringBuilder().ToString();
        }
        public string AddStaff()
        {
            string staffName = Request.Params["staffName"].Trim();
            string staffPos = Request.Params["staffPos"].Trim();
            int roleId = int.Parse(Request.Params["roleId"]);

            int staffGroupId;
            Int32.TryParse(Request.Params["staffGroupId"], out staffGroupId);
            string txtStaffHome = Request.Params["txtStaffHome"].Trim();
            string txtStaffIdenNum = Request.Params["txtStaffIdenNum"].Trim();
            string txtStaffEmail = Request.Params["txtStaffEmail"].Trim();

            string txtStaffBankBranch = Request.Params["txtStaffBankBranch"].Trim();
            string txtStaffPhone = Request.Params["txtStaffPhone"].Trim();
            string txtStaffImage = Request.Params["txtStaffImage"];
            string txtStaffSex = Request.Params["txtStaffSex"];
            string txtStaffAdd = Request.Params["txtStaffAdd"].Trim();
            string txtStaffDayofbith = Request.Params["txtStaffDayofbith"].Trim();
            string txtStaffPlacebirth = Request.Params["txtStaffPlacebirth"].Trim();
            string txtStaffIdenDay = Request.Params["txtStaffIdenDay"].Trim();
            string txtStaffIdenPlace = Request.Params["txtStaffIdenPlace"].Trim();
            string txtStaffTax = Request.Params["txtStaffTax"].Trim();
            string txtStaffBankNum = Request.Params["txtStaffBankNum"].Trim();
            string txtUsername = Request.Params["txtUsername"].Trim();
            int txtAppendantPeople = int.Parse(Request.Params["txtAppendantPeople"]);

            string cboOffice = Request.Params["cboOfficeVal"];
            string[] officeList = cboOffice.Split(',');
            if ((roleId == 1 && int.Parse(Session["staffId"].ToString()) == 1) || roleId != 1)
            {
                if (!staffBLO.CheckExistUserName(txtUsername))
                {
                    bool result = staffBLO.AddStaff(staffName, staffPos, roleId, staffGroupId, txtStaffHome,
                        txtStaffIdenNum, txtStaffEmail, txtStaffBankBranch, txtStaffPhone,
                        txtStaffImage, txtStaffSex, txtStaffAdd, txtStaffDayofbith, txtStaffPlacebirth, txtStaffIdenDay,
                        txtStaffIdenPlace, txtStaffTax, txtStaffBankNum, txtUsername, txtAppendantPeople, officeList);
                    if (result == false)
                    {
                        return "error";
                    }
                    return "successful";
                }
                else return "exist";
            }
            return "PermissionDeny";
        }

        public string UpdateStaff()
        {

            int staffId = int.Parse(Request.Params["staffId"]);
            string staffName = Request.Params["staffName"].Trim();
            string staffPos = Request.Params["staffPos"].Trim();

            int newRoleId = int.Parse(Request.Params["roleId"]);

            int staffGroupId = int.Parse(Request.Params["staffGroupId"]);
            string txtStaffHome = Request.Params["txtStaffHome"].Trim();
            string txtStaffIdenNum = Request.Params["txtStaffIdenNum"].Trim();
            string txtStaffEmail = Request.Params["txtStaffEmail"].Trim();
            string txtStaffBankBranch = Request.Params["txtStaffBankBranch"].Trim();
            string txtStaffImage = Request.Params["txtStaffImage"];
            string txtStaffPhone = Request.Params["txtStaffPhone"].Trim();
            string txtStaffSex = Request.Params["txtStaffSex"];
            string txtStaffAdd = Request.Params["txtStaffAdd"].Trim();
            string txtStaffDayofbith = Request.Params["txtStaffDayofbith"].Trim();
            string txtStaffPlacebirth = Request.Params["txtStaffPlacebirth"].Trim();
            string txtStaffIdenDay = Request.Params["txtStaffIdenDay"].Trim();
            string txtStaffIdenPlace = Request.Params["txtStaffIdenPlace"].Trim();
            string txtStaffTax = Request.Params["txtStaffTax"].Trim();
            string txtStaffBankNum = Request.Params["txtStaffBankNum"].Trim();
            int detailAppendantPeople = int.Parse(Request.Params["detailAppendantPeople"]);

            string cboOffice2 = Request.Params["cboOfficeVal1"];
            String[] newOfficeList = cboOffice2.Split(',');
            if (staffBLO.CheckEditableStaff(int.Parse(Session["staffId"].ToString()), newRoleId, staffId))
            {

                if (staffBLO.CheckOfficeInWork(newOfficeList, newRoleId, staffId))
                {

                    bool result = staffBLO.UpdateStaff(staffId, staffName, staffPos, newRoleId, staffGroupId, txtStaffHome, txtStaffIdenNum, txtStaffEmail, txtStaffBankBranch, txtStaffPhone,
                        txtStaffImage, txtStaffSex, txtStaffAdd, txtStaffDayofbith, txtStaffPlacebirth, txtStaffIdenDay, txtStaffIdenPlace, txtStaffTax, txtStaffBankNum, detailAppendantPeople, newOfficeList);
                    if (result == false)
                    {
                        return "error";
                    }
                    return "successful";

                } return "OfficeInWork";

            } return "PermissionDeny";
        }
        public JsonResult GetUpdateStaff()
        {
            var staffBLO = new StaffBLO();
            var list = staffBLO.GetAllStaffJson();
            return Json(list, JsonRequestBehavior.AllowGet);

        }

        public String ResetStaffPass()
        {
            if (Session["staffId"].ToString() == "1")
            {
                int staffId = int.Parse(Request.Params["staffId"]);

                bool result = staffBLO.ResetStaffPass(staffId);
                if (result == false)
                {
                    return "error";
                }
                return "successful";
            }
            return "error";
        }
        public string AddStaffGroup()
        {
            string txtGroupName = Request.Params["txtGroupName"];
            string txtGroupDetail = Request.Params["txtGroupDetail"];

            double txtMoneySalary;
            double.TryParse(Request.Params["txtMoneySalary"], out txtMoneySalary);

            if (!staffBLO.CheckExistGroupName(txtGroupName))
            {
                bool result = staffBLO.AddStaffGroup(txtGroupName, txtGroupDetail, txtMoneySalary);
                if (result == false)
                {
                    return "Error";
                }
                return "Successful";
            }
            else return "exist";
        }

        public String UpdateStaffGroup()
        {
            int staffGrId;
            Int32.TryParse(Request.Params["staffGrId"], out staffGrId);
            string staffGrName = Request.Params["staffGrName"];
            string staffGrDetail = Request.Params["staffGrDetail"];
            double txtEditMoney;
            double.TryParse(Request.Params["txtEditMoney"], out txtEditMoney);

            bool result = staffBLO.UpdateStaffGroup(staffGrId, staffGrName, staffGrDetail, txtEditMoney);
            if (result == false)
            {
                return "Error";
            }
            return "Successful";

        }

        public String DeleteStaffGroup()
        {
            int staffGroupId;
            Int32.TryParse(Request.Params["staffGroupId"], out staffGroupId);
            bool result = staffBLO.DeleteStaffGroup(staffGroupId);
            if (result)
            {
                return "Successful";
            }
            return "Error";
        }

        public String SetStatusStaff()
        {
            int staffId;
            Int32.TryParse(Request.Params["staffId"], out staffId);
            var staff = staffBLO.GetStaffByID(staffId);
            if ((Session["RoleId"].ToString() == "1" && staff.RoleId != 1 && staff.StaffId != 1) || (Session["StaffId"].ToString() == "1" && staff.StaffId != 1))
            {
                return staffBLO.SetStatusStaff(staffId);
            }
            return "ErrorPermission";
        }
        //public String DeleteStaffInfo()
        //{
        //    int staffId;
        //    Int32.TryParse(Request.Params["staffId"], out staffId);
        //    bool result = staffBLO.DeleteStaffInfo(staffId);
        //    if (result)
        //    {
        //        return "Successful";
        //    }
        //    return "Error";
        //}
        //public String ActiveStaff()
        //{
        //    int staffId;
        //    Int32.TryParse(Request.Params["staffId"], out staffId);
        //    bool result = staffBLO.ActiveStaff(staffId);
        //    if (result)
        //    {
        //        return "Successful";
        //    }
        //    return "Error";
        //}

    }
}
