using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LFMS.Models.BLO;
using LFMS.Utilities;

namespace LFMS.Controllers
{
    public class OfficeController : AdminController
    {
        OfficeBLO officeBLO = new OfficeBLO();

        public ActionResult Office()
        {
            return View();
        }

        public JsonResult GetAllOfficeJson()
        {
            List<Office> office = officeBLO.GetAllOffice();
            var list = new List<Object>();
            foreach (var of in office)
            {
                var result = UtilityClass.ConvertDynamicObjectWithFullAttr(of);
                list.Add(result);
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetOfficeStaff()
        {
            List<Office> office = officeBLO.GetOfficeStaff();
            var list = new List<Object>();
            foreach (var of in office)
            {
                var result = UtilityClass.ConvertDynamicObjectWithFullAttr(of);
                list.Add(result);
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public String AddOffice()
        {
            if (Session["StaffId"].ToString() == "1")
            {
                string offName = Request.Params["offName"].Trim();
                string offManager = Request.Params["offManager"].Trim();
                string offTaxcode = Request.Params["offTaxcode"].Trim();
                string offAdd = Request.Params["offAdd"].Trim();
                string offPhone = Request.Params["offPhone"].Trim();
                string offFax = Request.Params["offFax"].Trim();
                string offEmail = Request.Params["offEmail"].Trim();
                string offWebsite = Request.Params["offWebsite"].Trim();
                string offbankAccount = Request.Params["offbankAccount"].Trim();
                string offbankName = Request.Params["offbankName"].Trim();
                if (!officeBLO.CheckExistOfficeName(offName))
                {
                    bool result = officeBLO.AddOffice(offName, offManager, offTaxcode, offAdd, offPhone, offFax, offEmail, offWebsite, offbankAccount, offbankName);
                    if (result == false)
                    {
                        return "error";
                    }
                    return "successful";
                }
                else return "exist";
            }
            return "error";
        }

        public String UpdateOffice()
        {
            if (Session["StaffId"].ToString() == "1")
            {
                int offId;
                Int32.TryParse(Request.Params["offId"], out offId);
                string offName = Request.Params["offName"].Trim();
                string offManager = Request.Params["offManager"].Trim();
                string offTaxcode = Request.Params["offTaxcode"].Trim();
                string offAdd = Request.Params["offAdd"].Trim();
                string offPhone = Request.Params["offPhone"].Trim();
                string offFax = Request.Params["offFax"].Trim();
                string offEmail = Request.Params["offEmail"].Trim();
                string offWebsite = Request.Params["offWebsite"].Trim();
                string offbankAccount = Request.Params["offbankAccount"].Trim();
                string offbankName = Request.Params["offbankName"].Trim();
                bool result = officeBLO.UpdateOffice(offId, offName, offManager, offTaxcode, offAdd, offPhone, offFax,
                    offEmail, offWebsite, offbankAccount, offbankName);
                if (result == false)
                {
                    return "error";
                }
                return "successful";
            } return "error";
        }
        public String SetStatusOffice()
        {
            if (Session["StaffId"].ToString() == "1")
            {
                int officeId;
                Int32.TryParse(Request.Params["officeId"], out officeId);
                return officeBLO.SetStatusOffice(officeId);
            }
            return "Error";
        }
    }
}