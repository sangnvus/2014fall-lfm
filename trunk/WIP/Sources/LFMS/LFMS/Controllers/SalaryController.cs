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
using LFMS.Models.ConcerEntity;

namespace LFMS.Controllers
{
    public class SalaryController : AdminController
    {
        SalaryBLO salaryBLO = new SalaryBLO();
        //
        // GET: /Staff/

        public ActionResult Salary()
        {
            return View();
        }


        public JsonResult GetAllTax()
        {
            List<Tax> tax = salaryBLO.GetAllTax();
            var list = new List<Object>();
            foreach (var of in tax)
            {
                var result = UtilityClass.ConvertDynamicObjectWithFullAttr(of);
                list.Add(result);
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetSelectBenefit()
        {
            List<Benefit> benefit = salaryBLO.GetSelectBenefit();
            var list = new List<Object>();
            foreach (var of in benefit)
            {
                var result = UtilityClass.ConvertDynamicObjectWithFullAttr(of);
                list.Add(result);
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSelectAssurance()
        {
            List<Assurance> assu = salaryBLO.GetSelectAssurance();
            var list = new List<Object>();
            foreach (var of in assu)
            {
                var result = UtilityClass.ConvertDynamicObjectWithFullAttr(of);
                list.Add(result);
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }


        //public JsonResult TimesheetDetail(int IdTimeSheet)
        //{
        //    var result = salaryBLO.getStaffTimesheetId(IdTimeSheet);
        //    //ViewBag.IdTimeSheet = id;

        //    return Json(result, JsonRequestBehavior.AllowGet);
        //}

        public JsonResult GetSalary()
        {
            var list = salaryBLO.GetAllSalaryJson();
            return Json(new { html = RenderPartialViewToString("_AllSalary", null), list }, JsonRequestBehavior.AllowGet);

        }
        public JsonResult GetSalaryUpdate()
        {
            SalaryBLO salaryBLO = new SalaryBLO();
            var list = salaryBLO.GetAllSalaryJson();
            return Json(list, JsonRequestBehavior.AllowGet);

        }
        //public JsonResult getSalarybyMonth(int monthId)
        //{

        //    var list = salaryBLO.getSalarybyMonthJson(monthId);
        //    return Json(new { list }, JsonRequestBehavior.AllowGet);
        //    //return Json(list, JsonRequestBehavior.AllowGet);

        //}

        public JsonResult getSalarybyMonthandYear(int monthId, int yearId)
        {

            var list = salaryBLO.getSalarybyMonthandYear(monthId, yearId);
            return Json(new { list }, JsonRequestBehavior.AllowGet);
            //return Json(list, JsonRequestBehavior.AllowGet);

        }


        public JsonResult getMonthInYear()
        {
            var list = salaryBLO.getMonthInYear();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getYear()
        {
            var list = salaryBLO.getYear();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetTaxs()
        {
            var list = salaryBLO.GetAllTaxJson();
            return Json(new { html = RenderPartialViewToString("_Tax", null), list }, JsonRequestBehavior.AllowGet);

        }

        //public JsonResult getStaffTimesheetId(int StaffId)
        //{
        //    var result = salaryBLO.getStaffTimesheetId(StaffId);

        //    return Json(result, JsonRequestBehavior.AllowGet);
        //}


        public string AddStaffSalary()
        {
            //Thue
            //int selectStaffTax = int.Parse(Request.Params["selectStaffTax"]);

            //StaffId
            int selectStaffId = int.Parse(Request.Params["selectStaffName"]);

            //Ngay tinh luong
            string txtDateCountSalary = Request.Params["txtStaffDayofbith"].Trim();

            //List Id của trợ cấp
            string cboOffice = Request.Params["cboOfficeVal"];
            string[] officeList = cboOffice.Split(',');

            //List Id của thue
            string cboAss = Request.Params["cboAssUVal"];
            string[] assList = cboAss.Split(',');
            if (!salaryBLO.CheckDateImpress(txtDateCountSalary))
            {
                if (!salaryBLO.IsCountedSalary(selectStaffId, txtDateCountSalary))
                {
                    bool result = salaryBLO.AddStaffSalary(selectStaffId, txtDateCountSalary, officeList, assList);
                    if (result == false)
                    {
                        return "error";
                    }
                    return "successful";
                }
                else return "exist";
            }
            else return "dateinValide";
        }

        public string UpdateStaffSalary()
        {

            int salaryId = int.Parse(Request.Params["salaryId"]);
            int selectStaffId = int.Parse(Request.Params["selectStaffId"]);

            //Ngay tinh luong
            string detailDate = Request.Params["detailDate"].Trim();

            //List Id của trợ cấp
            string cboOffice = Request.Params["cboStaffBenefitDetail"];
            string[] officeList = cboOffice.Split(',');

            //List Id của bảo hiểm
            string cboAss = Request.Params["cboStaffAssuranceDetail"];
            string[] assList = cboAss.Split(',');

            if (!salaryBLO.CheckDateEditSalary(detailDate))
            {
                bool result = salaryBLO.UpdateStaffSalary(salaryId, selectStaffId, detailDate, officeList, assList);
                if (result == false)
                {
                    return "error";
                }
                return "successful";
            }
            else return "notpermit";
        }

        public JsonResult getAllAssurance()
        {
            var list = salaryBLO.getAllAssuranceJson();
            return Json(new { html = RenderPartialViewToString("_Assurance", null), list }, JsonRequestBehavior.AllowGet);

        }

        public JsonResult getAllBenefit()
        {
            var list = salaryBLO.getAllBenefit();
            return Json(new { html = RenderPartialViewToString("_Benefit", null), list }, JsonRequestBehavior.AllowGet);

        }

        public JsonResult getAllImpress()
        {
            var list = salaryBLO.getAllImpressJson();
            return Json(new { html = RenderPartialViewToString("_Impress", null), list }, JsonRequestBehavior.AllowGet);

        }
        public JsonResult getImpressByStaffId(int staffId, int monthImpressId, int yearImpressId)
        {
            var list = salaryBLO.getImpressByStaffId(staffId, monthImpressId, yearImpressId);
            return Json(new { list }, JsonRequestBehavior.AllowGet);

        }
        public JsonResult getAllReward()
        {
            var list = salaryBLO.getAllRewardJson();
            return Json(new { html = RenderPartialViewToString("_Reward", null), list }, JsonRequestBehavior.AllowGet);

        }
        public JsonResult getRewardbyStaffId(int staffId, int monthRewwardId, int yearRewardId)
        {
            var list = salaryBLO.getRewardbyStaffId(staffId, monthRewwardId, yearRewardId);
            return Json(new { list }, JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetTimeSheetByStaffId(int staffId, int monthTimsheetId, int yearTimsheetId)
        {
            var list = salaryBLO.GetTimeSheetByStaffId(staffId, monthTimsheetId, yearTimsheetId);
            return Json(new { list }, JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetTimesheet()
        {
            var list = salaryBLO.GetTimesheet();
            return Json(new { html = RenderPartialViewToString("_TimeSheet", null), list }, JsonRequestBehavior.AllowGet);

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



        //public JsonResult GetUpdateStaff()
        //{
        //    var salaryBLO = new SalaryBLO();
        //    var list = salaryBLO.GetAllStaffJson();
        //    return Json(list, JsonRequestBehavior.AllowGet);

        //}


        public string AddTax()
        {
            string taxName = Request.Params["txtTaxName"];
            //double taxRate = Request.Params["txtTaxRate"];
            double taxRate;
            double.TryParse(Request.Params["txtTaxRate"], out taxRate);

            double taxIndex;
            double.TryParse(Request.Params["txtTaxIndex"], out taxIndex);

            double taxFrom;
            double.TryParse(Request.Params["txtTaxFrom"], out taxFrom);

            double taxTo;
            double.TryParse(Request.Params["txtTaxTo"], out taxTo);

            if (!salaryBLO.CheckExistTaxName(taxName))
            {
                bool result = salaryBLO.AddNewTax(taxName, taxRate, taxIndex, taxFrom, taxTo);
                if (result == false)
                {
                    return "Error";
                }
                return "Successful";
            }
            else return "exist";

        }

        //public string addnewTimeSheet()
        //{
        //    bool result = salaryBLO.addnewTimeSheet();
        //    if (result == false)
        //    {
        //        return "Error";
        //    }
        //    return "Successful";

        //}

        public string AddAssurance()
        {
            string assuranceName = Request.Params["txtAssuranceName"];
            double assuranceRate;
            double.TryParse(Request.Params["txtAssuranceIndex"], out assuranceRate);
            if (!salaryBLO.CheckExistAssuranName(assuranceName))
            {

                bool result = salaryBLO.AddAssurance(assuranceName, assuranceRate);
                if (result == false)
                {
                    return "Error";
                }
                return "Successful";
            }
            else return "exist";
        }

        public string AddImpress()
        {
            int selectStaffImpress = int.Parse(Request.Params["selectStaffImpress"]);
            string dateStaffImpress = Request.Params["dateStaffImpress"];

            double txtmoneyImpress;
            double.TryParse(Request.Params["txtmoneyImpress"], out txtmoneyImpress);
            if (!salaryBLO.CheckDateImpress(dateStaffImpress))
            {
                bool result = salaryBLO.AddImpress(selectStaffImpress, dateStaffImpress, txtmoneyImpress);
                if (result == false)
                {
                    return "Error";
                }
                return "Successful";
            }
            else return "dateinValide";
        }

        public string AddReward()
        {
            int selectStaffReward = int.Parse(Request.Params["selectStaffReward"]);
            string dateStaffReward = Request.Params["dateStaffReward"];


            double txtmoneyReward;
            double.TryParse(Request.Params["txtmoneyReward"], out txtmoneyReward);
            string txtDescription = Request.Params["txtDescription"];
            if (!salaryBLO.CheckDateImpress(dateStaffReward))
            {
                bool result = salaryBLO.AddReward(selectStaffReward, dateStaffReward, txtmoneyReward, txtDescription);
                if (result == false)
                {
                    return "Error";
                }
                return "Successful";
            }
            else return "dateinValide";
        }

        public string AddStaffTimesheet()
        {
            int selectStaffTimesheet = int.Parse(Request.Params["selectStaffTimesheet"]);
            string dateStaffTimesheet = Request.Params["dateStaffTimesheet"];
            string txtDetail = Request.Params["txtDetail"];
            double txttimeLeave;
            double.TryParse(Request.Params["txttimeLeave"], out txttimeLeave);
            bool staffAllow;
            bool.TryParse(Request.Params["staffAllow"], out staffAllow);
            if (!salaryBLO.CheckDateIsSunday(dateStaffTimesheet))
            {
                if (!salaryBLO.CheckDateImpress(dateStaffTimesheet))
                {
                    if (!salaryBLO.IsOverHoursInDay(dateStaffTimesheet, selectStaffTimesheet, txttimeLeave))
                    {
                        bool result = salaryBLO.AddStaffTimesheet(selectStaffTimesheet, dateStaffTimesheet, txtDetail, txttimeLeave, staffAllow);
                        if (result == false)
                        {
                            return "Error";
                        }
                        return "Successful";
                    }
                    else return "overload";
                }
                else return "dateinValide";
            }
            else return "sang";
        }

        public string addNewBenefit()
        {
            string benefitName = Request.Params["benefitName"];
            double benefitRate;
            double.TryParse(Request.Params["benefitRate"], out benefitRate);
            if (!salaryBLO.CheckExistBenefitName(benefitName))
            {
                bool result = salaryBLO.addNewBenefit(benefitName, benefitRate);
                if (result == false)
                {
                    return "Error";
                }
                return "Successful";
            }
            else return "exist";

        }

        public String UpdateTax()
        {
            int txTaxId;
            Int32.TryParse(Request.Params["txTaxId"], out txTaxId);

            string taxName = Request.Params["txtTaxName"];
            //double taxRate = Request.Params["txtTaxRate"];
            double taxRate;
            double.TryParse(Request.Params["txtTaxRate"], out taxRate);

            double taxIndex;
            double.TryParse(Request.Params["txtTaxIndex"], out taxIndex);

            double taxFrom;
            double.TryParse(Request.Params["txtTaxFrom"], out taxFrom);

            double taxTo;
            double.TryParse(Request.Params["txtTaxTo"], out taxTo);

            //if (!salaryBLO.CheckExistTaxName(taxName))
            //{
            bool result = salaryBLO.UpdateTax(txTaxId, taxName, taxRate, taxIndex, taxFrom, taxTo);
            if (result == false)
            {
                return "Error";
            }
            return "Successful";
            //}
            //else return "exist";
        }

        public String UpdateAssurance()
        {
            int assuranceId;
            Int32.TryParse(Request.Params["assuranceId"], out assuranceId);

            string assuranceName = Request.Params["txtEditName"];

            double assuranceRate;
            double.TryParse(Request.Params["txtEditRate"], out assuranceRate);

            bool result = salaryBLO.UpdateAssurance(assuranceId, assuranceName, assuranceRate);
            if (result == false)
            {
                return "Error";
            }
            return "Successful";

        }

        public string UpdateTimeSheet()
        {
            int txtTimeSheetId;
            Int32.TryParse(Request.Params["txtTimeSheetId"], out txtTimeSheetId);
            int txtEditName = int.Parse(Request.Params["txtEditName"]);
            string txtEditDate = Request.Params["txtEditDate"];
            string txtEditDetail = Request.Params["txtEditDetail"];
            int txtEdittimeLeave = int.Parse(Request.Params["txtEdittimeLeave"]);
            bool editstaffAllow;
            bool.TryParse(Request.Params["editstaffAllow"], out editstaffAllow);

            if (!salaryBLO.CheckDateIsSunday(txtEditDate))
            {
                if (!salaryBLO.CheckDateImpress(txtEditDate))
                {
                    if (!salaryBLO.IsOverHoursInDay(txtEditDate, txtTimeSheetId, txtEdittimeLeave))
                    {
                        bool result = salaryBLO.UpdateTimeSheet(txtTimeSheetId, txtEditName, txtEditDate, txtEditDetail, txtEdittimeLeave, editstaffAllow);
                        if (result == false)
                        {
                            return "Error";
                        }
                        return "Successful";
                    }
                    else return "overload";
                }
                else return "dateinValide";
            }
            else return "sang";
        }

        public String UpdateBenefit()
        {
            int benefitId;
            Int32.TryParse(Request.Params["benefitId"], out benefitId);

            string benefitName = Request.Params["txtEditName"];

            double benefiteRate;
            double.TryParse(Request.Params["txtEditRate"], out benefiteRate);

            bool result = salaryBLO.UpdateBenefit(benefitId, benefitName, benefiteRate);
            if (result == false)
            {
                return "Error";
            }
            return "Successful";

        }

        public String UpdateImpress()
        {
            int txtImpressId;
            Int32.TryParse(Request.Params["txtImpressId"], out txtImpressId);

            int txtEditName;
            Int32.TryParse(Request.Params["txtEditName"], out txtEditName);

            string txtEditDate = Request.Params["txtEditDate"];

            double txtEditMoney;
            double.TryParse(Request.Params["txtEditMoney"], out txtEditMoney);
            if (!salaryBLO.CheckDateImpress(txtEditDate))
            {
                bool result = salaryBLO.UpdateImpress(txtImpressId, txtEditName, txtEditDate, txtEditMoney);
                if (result == false)
                {
                    return "Error";
                }
                return "Successful";

            }
            else return "dateinValide";
        }

        public String UpdateReward()
        {
            int txtRewardId;
            Int32.TryParse(Request.Params["txtRewardId"], out txtRewardId);

            int txtEditName;
            Int32.TryParse(Request.Params["txtEditName"], out txtEditName);

            string txtEditDate = Request.Params["txtEditDate"];

            double txtEditMoney;
            double.TryParse(Request.Params["txtEditMoney"], out txtEditMoney);

            string editDescription = Request.Params["editDescription"];

            if (!salaryBLO.CheckDateImpress(txtEditDate))
            {
                bool result = salaryBLO.UpdateReward(txtRewardId, txtEditName, txtEditDate, txtEditMoney, editDescription);
                if (result == false)
                {
                    return "Error";
                }
                return "Successful";

            }
            else return "dateinValide";
        }

        public String DeleteTax()
        {
            int txTaxId;
            Int32.TryParse(Request.Params["txTaxId"], out txTaxId);
            return salaryBLO.DeleteTax(txTaxId);

        }

        public String deleteSalary()
        {
            int salaryId;
            Int32.TryParse(Request.Params["salaryId"], out salaryId);
            bool result = salaryBLO.deleteSalary(salaryId);
            if (result == true)
            {
                return "Successful";
            }
            return "Error";
        }
        public String DeleteAssurance()
        {
            int assuranceId;
            Int32.TryParse(Request.Params["assuranceId"], out assuranceId);
            return salaryBLO.DeleteAssurance(assuranceId);

        }

        public String deleteImpress()
        {
            int txtImpressId;
            Int32.TryParse(Request.Params["txtImpressId"], out txtImpressId);
            bool result = salaryBLO.deleteImpress(txtImpressId);
            if (result == true)
            {
                return "Successful";
            }
            return "Error";
        }

        public String deleteReward()
        {
            int txtRewardId;
            Int32.TryParse(Request.Params["txtRewardId"], out txtRewardId);
            bool result = salaryBLO.deleteReward(txtRewardId);
            if (result == true)
            {
                return "Successful";
            }
            return "Error";
        }
        public String DeleteBenefit()
        {
            int benefitId;
            Int32.TryParse(Request.Params["benefitId"], out benefitId);
            return salaryBLO.DeleteBenefit(benefitId);
        }

        public String deleteTimeSheet()
        {
            int txtTimeSheetId;
            Int32.TryParse(Request.Params["txtTimeSheetId"], out txtTimeSheetId);
            bool result = salaryBLO.deleteTimeSheet(txtTimeSheetId);
            if (result == true)
            {
                return "Successful";
            }
            return "Error";
        }

        /// <summary>
        /// Export Excel 
        /// </summary>
        /// <param name="monthId"></param>
        /// <returns></returns>
        public String ExportExcel(string monthId, string yearId)
        {
            var month = Convert.ToInt32(monthId.Trim());
            var year = Convert.ToInt32(yearId.Trim());
            var resultExport = string.Empty;
            try
            {
                IList<object> listSalaryResult = new List<object>();
                var listSalary = salaryBLO.GetSalaryByMonthExcel(month, year);
                object[] headObject = { listSalary };
                listSalaryResult.Add(listSalary.ListSalary);
                string excelTemplatePath = HttpContext.Server.MapPath("~/ExcelReport/SalaryTemplate.xlsx");
                string xmlTemplatePath = HttpContext.Server.MapPath("~/ExcelReport/SalaryTemplate.xml");
                string savePath = HttpContext.Server.MapPath("~/ExcelReport/ExportSalary " + month + "-" + year + " (" + DateTime.Now.ToString("HH-mm-ss tt") + ")" + ".xlsx");
                using (ExcelBLO excel = new ExcelBLO(TemplatePattern.FULL, xmlTemplatePath, excelTemplatePath, "Salary"))
                {
                    excel.ExportObjectsToFullArea(savePath, listSalaryResult, headObject);
                    resultExport = "Successful";
                }
            }
            catch (Exception)
            {
                resultExport = "Error";
            }
            return resultExport;
        }
    }
}
