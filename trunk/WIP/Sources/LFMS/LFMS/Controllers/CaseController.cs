using System;
using System.Collections;
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
    public class CaseController : BaseController
    {
        CaseBLO caseBLO = new CaseBLO();
        OfficeBLO officeBLO = new OfficeBLO();
        StaffBLO staffBLO = new StaffBLO();
        CustomerBLO customerBLO = new CustomerBLO();

        public ActionResult Case(int id)
        {
            ViewBag.caseId = id;
            var listAuthorize = (List<String>)Session["UserAuthorize"];
            ViewBag.IsAuthorize = "false";
            if (listAuthorize.Any())
            {
                if (listAuthorize.IndexOf(id.ToString()) != -1 || "All".Equals(listAuthorize[0]))
                {
                    ViewBag.IsAuthorize = "true";
                }
            }
            ViewBag.CanEditLawyer = "false";
            var canEdit = caseBLO.CanEditLawyer(int.Parse(Session["StaffId"].ToString()), id);
            if (canEdit)
            {
                ViewBag.CanEditLawyer = "true";
            }
            return View();
        }

        public ActionResult ListCase()
        {
            return View();
        }

        // Case List ================================================================================

        [HttpPost]
        public JsonResult GetAllJsonCase()
        {
            int displayNum = int.Parse(Request.Params["displayNum"].Trim());
            int orderKey = int.Parse(Request.Params["orderKey"].Trim());
            string orderType = Request.Params["orderType"].Trim();

            string caseCode = Request.Params["caseCode"].Trim();
            int pageNum = int.Parse(Request.Params["pageNum"].Trim());
         var author = (List<String>)Session["UserAuthorize"];

            object listCases = caseBLO.GetCases(displayNum, orderKey, orderType, pageNum, caseCode, author);

            var casePage = listCases.GetType().GetProperty("casePage").GetValue(listCases, null);
            var totalRecord = listCases.GetType().GetProperty("total").GetValue(listCases, null);

            var list = new List<object>();
            var level1 = new List<string>();
            level1.Add("Office");
            foreach (var cs in (IEnumerable<object>)casePage)
            {
                var result = UtilityClass.ConvertDynamicObjectWithFullAttr(cs, level1);
                list.Add(result);
            }

            return Json(new { list, totalRecord });
        }

        [HttpPost]
        public String AddCase()
        {
            if (Session["RoleId"].ToString() == "1" || Session["RoleId"].ToString() == "2")
            {
                string caseCode = Request.Params["caseCode"].Trim();
                int creatorId = int.Parse(Session["StaffId"].ToString());
                string receiptDate = Request.Params["receiptDate"].Trim();
                string caseContent = Request.Params["caseContent"].Trim();
                int officeId = int.Parse(Request.Params["officeId"].Trim());
                if (!caseBLO.CheckExistCaseCode(caseCode))
                {
                    string result = caseBLO.AddCase(caseCode, creatorId, receiptDate, caseContent, officeId);
                    if (result == "error")
                    {
                        return "error";
                    }
                    return result;
                }
                else return "existCode";
            }
            return "error";
        }

        // Case Detail ================================================================================

        [HttpPost]
        public JsonResult GetCaseDetail(int id)
        {
            var result = caseBLO.GetJsonCaseDetailById(id);
            return Json(new { html = RenderPartialViewToString("_CaseInfo", null), result }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public String UpdateCaseDetail()
        {
            int caseId = int.Parse(Request.Params["caseId"].Trim());
            string receiptDate = Request.Params["receiptDate"].Trim();
            int newOfficeId = int.Parse(Request.Params["officeId"].Trim());
            string status = Request.Params["status"].Trim();
            string caseContent = Request.Params["caseContent"].Trim();
            string disputeSubject = Request.Params["disputeSubject"].Trim();
            string disputeRelation = Request.Params["disputeRelation"].Trim();
            string limitationStatute = Request.Params["limitationStatute"].Trim();
            string legalEvent = Request.Params["legalEvent"].Trim();
            string errorFactor = Request.Params["errorFactor"].Trim();
            var listAuthorize = (List<String>)Session["UserAuthorize"];
            if (listAuthorize.Any())
            {
                if (listAuthorize.IndexOf(caseId.ToString()) != -1 || "All".Equals(listAuthorize[0]))
                {
                    if (caseBLO.CheckStaffInOffice(newOfficeId, caseId))
                    {
                  
                    bool result = caseBLO.UpdateCaseDetail(caseId, receiptDate, newOfficeId, status, caseContent,
                        disputeSubject, disputeRelation, limitationStatute, legalEvent, errorFactor);
                    if (result)
                    {
                        return "success";
                    }

                    } return "StaffNotInOffice";
                }
            }
            return "error";
        }

        [HttpPost]
        public JsonResult GetLawyerViewpoint(int id)
        {
            var result = caseBLO.GetCaseById(id);
            var outJson = UtilityClass.ConvertDynamicObjectWithFullAttr(result);
            return Json(outJson, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public String UpdateLawyerViewpoint()
        {
            int caseId = int.Parse(Request.Params["caseId"].Trim());
            string protectiveGoal = Request.Params["protectiveGoal"].Trim();
            string openingProcedure = Request.Params["openingProcedure"].Trim();
            string inquiryProcedure = Request.Params["inquiryProcedure"].Trim();
            string argumentProcedure = Request.Params["argumentProcedure"].Trim();

            bool result = caseBLO.UpdateLawyerViewpoint(caseId, protectiveGoal, openingProcedure, inquiryProcedure, argumentProcedure);
            if (result == false)
            {
                return "error";
            }
            return "success";
        }

        // Case Invoice ================================================================================

        [HttpPost]
        public JsonResult GetCost(int caseId)
        {
            var result = caseBLO.GetCost(caseId);
            return Json(new { html = RenderPartialViewToString("_CaseInvoice", null), result }, JsonRequestBehavior.AllowGet);
        }

        // Case Event ================================================================================

        [HttpPost]
        public JsonResult GetEvent(int caseId)
        {
            object result = caseBLO.GetEvent(caseId);
            var oEvent = result.GetType().GetProperty("OperationalEvents").GetValue(result, null);
            var casecode = result.GetType().GetProperty("CaseCode").GetValue(result, null);
            Session["caseCode"] = casecode;
            //oEvent = result.GetType().GetProperty("OperationalEvents").GetValue(result, null);

            var creatorList = new List<int>();
            foreach (var e in (IEnumerable) oEvent)
            {
                var creator = int.Parse(e.GetType().GetProperty("CreatorId").GetValue(e, null).ToString());
                creatorList.Add(creator);
            }
            var level1 = new List<string>();
            level1.Add("OperationalEvents");

            result = UtilityClass.ConvertDynamicObjectWithFullAttr(result, level1);
            var creatorUsername = caseBLO.GetListUsernameByStaffId(creatorList);
            return Json(new { html = RenderPartialViewToString("_CaseEvent", null), result, creatorUsername}, JsonRequestBehavior.AllowGet);
        }

        // Case Lawyer Related ================================================================================

        [HttpPost]
        public JsonResult GetLawyerRelated(int caseId)
        {
            var result = caseBLO.GetLawyerRelated(caseId);
            return Json(new { html = RenderPartialViewToString("_CaseLawyerRelated", null), result }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public String RemoveLawyerRelated()
        {
            int caseStaffId = int.Parse(Request.Params["caseStaffId"].Trim());

            int staffId = caseBLO.GetStaffIdByCaseStaffId(caseStaffId);
            int caseId = caseBLO.GetCaseIdByCaseStaffId(caseStaffId);
            var listAuthorize = (List<String>)Session["UserAuthorize"];
            if (listAuthorize.Any())
            {
                if (listAuthorize.IndexOf(caseId.ToString()) != -1 || "All".Equals(listAuthorize[0]))
                {
                    bool isCreator = caseBLO.IsCreator(staffId, caseId);
                    if (!isCreator)
                    {
                        bool result = caseBLO.RemoveLawyerRelated(caseStaffId);
                        if (result)
                        {
                            return "success";
                        }
                    }
                    else return "creator";
                }
            }
            return "error";
        }

        [HttpPost]
        public String AddLawyerRelated()
        {
            int staffId = int.Parse(Request.Params["staffId"].Trim());
            int caseId = int.Parse(Request.Params["caseId"].Trim());

            var listAuthorize = (List<String>)Session["UserAuthorize"];
            if (listAuthorize.Any())
            {
                if (listAuthorize.IndexOf(caseId.ToString()) != -1 || "All".Equals(listAuthorize[0]))
                {
                    if (!caseBLO.CheckExistCaseStaff(caseId, staffId))
                    {
                        bool result = caseBLO.AddCaseStaff(caseId, staffId);
                        if (result)
                        {
                            return "success";
                        }
                    }
                    else return "exist";
                }
            }
            return "error";
        }

        // Case Customer Related ================================================================================

        [HttpPost]
        public JsonResult GetCustomerRelated(int caseId)
        {
            var result = caseBLO.GetCustomerRelated(caseId);
            return Json(new { html = RenderPartialViewToString("_CaseCustomerRelated", null), result }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public String RemoveCustomerRelated()
        {
            int caseCusId = int.Parse(Request.Params["caseCustomerId"].Trim());

            var listAuthorize = (List<String>)Session["UserAuthorize"];
            var caseId = caseBLO.GetCaseIdByCaseCusId(caseCusId);
            if (listAuthorize.Any())
            {
                if (listAuthorize.IndexOf(caseId.ToString()) != -1 || "All".Equals(listAuthorize[0]))
                {
                    bool result = caseBLO.RemoveCustomerRelated(caseCusId);
                    if (result)
                    {
                        return "success";
                    }
                }
            }
            return "error";
        }

        [HttpPost]
        public String AddCaseCustomer(int customerId, int caseId)
        {
            var listAuthorize = (List<String>)Session["UserAuthorize"];
            if (listAuthorize.Any())
            {
                if (listAuthorize.IndexOf(caseId.ToString()) != -1 || "All".Equals(listAuthorize[0]))
                {
                    if (!caseBLO.CheckExistCaseCustomer(caseId, customerId))
                    {
                        bool result = caseBLO.AddCaseCustomer(caseId, customerId);
                        if (result)
                        {
                            return "success";
                        }
                    }
                    else return "exist";
                }
            }
            return "error";
        }

        [HttpPost]
        public String UpdateCustomer()
        {
            int id;
            Int32.TryParse(Request.Params["id"], out id);
            string taxCode = Request.Params["taxCode"].Trim();
            string name = Request.Params["name"].Trim();
            int selectCustomerGroup;
            Int32.TryParse(Request.Params["selectCustomerGroup"], out selectCustomerGroup);
            string represent = Request.Params["represent"].Trim();
            string sex = Request.Params["sex"];
            string birthDay = Request.Params["birthDay"].Trim();
            string identityNum = Request.Params["identityNum"].Trim();
            string identityDate = Request.Params["identityDate"].Trim();
            string identityPlace = Request.Params["identityPlace"].Trim();
            string bankAccount = Request.Params["bankAccount"].Trim();
            string bankBranch = Request.Params["bankBranch"].Trim();
            string address = Request.Params["address"].Trim();
            string mobile = Request.Params["mobile"].Trim();
            string telephone = Request.Params["telephone"].Trim();
            string email = Request.Params["email"].Trim();

            string result = customerBLO.UpdateCustomer(id, taxCode, name, selectCustomerGroup, represent, sex, birthDay, identityNum, identityDate, identityPlace, bankAccount, bankBranch, address, mobile, telephone, email);
            return result;
        }

        [HttpPost]
        public JsonResult GetCustomersAutoSearch(string cusName)
        {
            List<Customer> customer = customerBLO.GetCustomersAutoSearch(cusName);
            var list = new List<Object>();
            foreach (var cus in customer)
            {
                var result = UtilityClass.ConvertDynamicObjectWithFullAttr(cus);
                list.Add(result);
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetAllCustomerGroupJson()
        {
            var list = customerBLO.GetAllCustomerGroupJson();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        // Case Subject ================================================================================

        [HttpPost]
        public JsonResult GetSubjectRelated(int caseId)
        {
            List<Subject> subList = caseBLO.GetAllSubjects(caseId);
            var result = subList.Select(sub => new
            {
                subjectId = sub.SubjectId,
                subjectName = sub.SubjectName,
                litigationCapacity = sub.LitigationCapacity,
                address = sub.Address,
                phoneNumber = sub.PhoneNumber,
                email = sub.Email,
                caseId = sub.CaseId
            });
            return Json(new { html = RenderPartialViewToString("_CaseSubjectRelated", null), result }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public String AddSubject(string subjectName, string litigationCapacity, string address, string phoneNumber, string email, int caseId)
        {
            var listAuthorize = (List<String>)Session["UserAuthorize"];
            if (listAuthorize.Any())
            {
                if (listAuthorize.IndexOf(caseId.ToString()) != -1 || "All".Equals(listAuthorize[0]))
                {
                    bool result = caseBLO.AddSubject(subjectName, litigationCapacity, address, phoneNumber, email, caseId);
                    if (result)
                    {
                        return "success";
                    }
                }
            }
            return "error";
        }

        [HttpPost]
        public String UpdateSubject(int subjectId, string subjectName, string litigationCapacity, string address, string phoneNumber, string email)
        {
            var caseId = caseBLO.GetCaseIdBySubjectId(subjectId);
            var listAuthorize = (List<String>)Session["UserAuthorize"];
            if (listAuthorize.Any())
            {
                if (listAuthorize.IndexOf(caseId.ToString()) != -1 || "All".Equals(listAuthorize[0]))
                {
                    bool result = caseBLO.UpdateSubject(subjectId, subjectName, litigationCapacity, address, phoneNumber, email);
                    if (result)
                    {
                        return "success";
                    }
                }
            }
            return "error";
        }

        [HttpPost]
        public String DeleteSubject(int subjectId)
        {
            var caseId = caseBLO.GetCaseIdBySubjectId(subjectId);
            var listAuthorize = (List<String>)Session["UserAuthorize"];
            if (listAuthorize.Any())
            {
                if (listAuthorize.IndexOf(caseId.ToString()) != -1 || "All".Equals(listAuthorize[0]))
                {
                    bool result = caseBLO.DeleteSubject(subjectId);
                    if (result)
                    {
                        return "success";
                    }
                }
            }
            return "error";
        }

        // Case File ================================================================================

        public String GetFile(int caseId)
        {
            var caseBLO = new CaseBLO();
            var caseInfo = caseBLO.GetCaseById(caseId);
            Session["caseCode"] = caseInfo.CaseCode;
            Session["caseId"] = caseInfo.CaseId;
            return "success";
        }

        // Get data for viewer and creator with BaseController

        [HttpPost]
        public JsonResult GetAllOfficeJson()
        {
            List<Office> office = officeBLO.GetOfficeStaff();
            var list = new List<Object>();
            foreach (var off in office)
            {
                var result = UtilityClass.ConvertDynamicObjectWithFullAttr(off);
                list.Add(result);
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetOfficesByStaffId()
        {
            var result = officeBLO.GetOfficesByStaffId(int.Parse(Session["StaffId"].ToString()));
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetLawyerByOffice(int officeId)
        {
            var result = staffBLO.GetLawyerByOffice(officeId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        // Cost ================================================================================

        public JsonResult GetAllServices()
        {
            var serviceBlo = new ServiceBLO();

            List<ServiceType> listService = serviceBlo.GetAllServiceType();
            var level1 = new List<string>();
            level1.Add("Services");
            var list = new List<object>();

            foreach (var ser in listService)
            {
                var result = UtilityClass.ConvertDynamicObjectWithFullAttr(ser, level1);
                list.Add(result);
            }
            return Json(list, JsonRequestBehavior.DenyGet);
        }

        public JsonResult UpdateCost()
        {
            string code = Request.Params["update"].Trim();
            int id = int.Parse(Request.Params["caseId"].Trim());

            var serviceBlo = new ServiceBLO();
            var result = caseBLO.UpdateCost(id, code);

            return Json(result, JsonRequestBehavior.DenyGet);
        }

        public String AddInvoice()
        {
            int caseId = int.Parse(Request.Params["caseId"].Trim());
            int serviceCost = int.Parse(Request.Params["serviceCost"].Trim());
            string txtDescription = Request.Params["txtDescription"].Trim();
            string datePickerService = Request.Params["datePickerService"].Trim();
            string serviceName = Request.Params["serviceName"].Trim();

            var listAuthorize = (List<String>)Session["UserAuthorize"];
            if (listAuthorize.Any())
            {
                if (listAuthorize.IndexOf(caseId.ToString()) != -1 || "All".Equals(listAuthorize[0]))
                {
                    if (serviceCost >= 0 && !datePickerService.IsNullOrWhiteSpace() && !serviceName.IsNullOrWhiteSpace())
                    {
                        bool result = caseBLO.AddInvoice(caseId, serviceCost, txtDescription, datePickerService, serviceName);
                        if (result) return "Successful";
                    }
                }
            }
            return "Error";
        }

        public String AddPayment()
        {
            int caseId = int.Parse(Request.Params["caseId"].Trim());
            int paymentCost = int.Parse(Request.Params["paymentCost"].Trim());
            string txtDescription = Request.Params["txtDescription"].Trim();
            string datePickerPayment = Request.Params["datePickerPayment"].Trim();

            var listAuthorize = (List<String>)Session["UserAuthorize"];
            if (listAuthorize.Any())
            {
                if (listAuthorize.IndexOf(caseId.ToString()) != -1 || "All".Equals(listAuthorize[0]))
                {
                    if (paymentCost >= 0 && !txtDescription.IsNullOrWhiteSpace() && !datePickerPayment.IsNullOrWhiteSpace())
                    {
                        bool result = caseBLO.AddPayment(caseId, paymentCost, txtDescription, datePickerPayment);
                        if (result) return "Successful";
                    }
                }
            }
            return "Error";
        }

        public String DeleteInvoice()
        {
            int invoiceId = int.Parse(Request.Params["invoiceId"].Trim());
            int caseId = caseBLO.GetCaseIdByInvoiceId(invoiceId);

            var listAuthorize = (List<String>)Session["UserAuthorize"];
            if (listAuthorize.Any())
            {
                if (listAuthorize.IndexOf(caseId.ToString()) != -1 || "All".Equals(listAuthorize[0]))
                {
                    bool result = caseBLO.DeleteInvoice(invoiceId);
                    if (result) return "Successful";

                }
            }
            return "Error";
        }

        public String DeletePayment()
        {
            int paymentId = int.Parse(Request.Params["paymentId"].Trim());

            int caseId = caseBLO.GetCaseIdByPaymentId(paymentId);
            var listAuthorize = (List<String>)Session["UserAuthorize"];
            if (listAuthorize.Any())
            {
                if (listAuthorize.IndexOf(caseId.ToString()) != -1 || "All".Equals(listAuthorize[0]))
                {
                    bool result = caseBLO.DeletePayment(paymentId);
                    if (result) return "Successful";
                }
            }
            return "Error";
        }

        public String UpdateInvoice()
        {
            int serviceCost = int.Parse(Request.Params["serviceCost"].Trim());
            int invoiceId = int.Parse(Request.Params["invoiceId"].Trim());
            string txtDescription = Request.Params["txtDescription"].Trim();
            string datePickerService = Request.Params["datePickerService"].Trim();

            int caseId = caseBLO.GetCaseIdByInvoiceId(invoiceId);
            if (serviceCost > 0 && !datePickerService.IsNullOrWhiteSpace() && !txtDescription.IsNullOrWhiteSpace())
            {
                var listAuthorize = (List<String>)Session["UserAuthorize"];
                if (listAuthorize.Any())
                {
                    if (listAuthorize.IndexOf(caseId.ToString()) != -1 || "All".Equals(listAuthorize[0]))
                    {
                        bool result = caseBLO.UpdateInvoice(invoiceId, serviceCost, txtDescription, datePickerService);
                        if (result) return "Successful";
                    }
                }
            }
            return "Error";
        }

        public String UpdatePayment()
        {
            int paymentId = int.Parse(Request.Params["paymentId"].Trim());
            int paymentMoney = int.Parse(Request.Params["paymentCost"].Trim());
            string txtDescription = Request.Params["txtDescription"].Trim();
            string datePickerPayment = Request.Params["datePickerPayment"].Trim();

            int caseId = caseBLO.GetCaseIdByPaymentId(paymentId);
            var listAuthorize = (List<String>)Session["UserAuthorize"];
            if (listAuthorize.Any())
            {
                if (listAuthorize.IndexOf(caseId.ToString()) != -1 || "All".Equals(listAuthorize[0]))
                {
                    if (paymentId > 0 && paymentMoney > 0 && !txtDescription.IsNullOrWhiteSpace() &&
                        !datePickerPayment.IsNullOrWhiteSpace())
                    {
                        bool result = caseBLO.UpdatePayment(paymentId, paymentMoney, txtDescription,
                            datePickerPayment);
                        if (result) return "Successful";
                    }
                }
            }
            return "Error";
        }

        // operation event ==================================================================================

        public String EditOEvent()
        {
            string content = Request.Params["content"];
            content = System.Net.WebUtility.HtmlDecode(content);
            string edate = Request.Params["edate"];
            string bdate = Request.Params["bdate"];
            string title = Request.Params["title"];
            int oEventId = int.Parse(Request.Params["oEventId"]);

            int creatorId = caseBLO.GetStaffIdByEventId(oEventId);
            var staffId = int.Parse(Session["StaffId"].ToString());

            if (staffId == creatorId)
            {
                bool result = caseBLO.EditLegalEvent(bdate, content, oEventId,  edate, title);
                if (result)
                {
                    return "EditLegalEventSuccess";
                }
            } 
            return "EditLegalEventFail";
        }

        [ValidateInput(false)]
        public int NewOEvent()
        {
            string beginTime = Request.Params["bdate"];
            string endTime = Request.Params["edate"];
            string title = Request.Params["title"];
            string content = Request.Params["str"];
            content = System.Net.WebUtility.HtmlDecode(content);
            int caseId = int.Parse(Request.Params["caseId"]);
            var staffId = int.Parse(Session["StaffId"].ToString());
            var listAuthorize = (List<String>)Session["UserAuthorize"];
            if (listAuthorize.Any())
            {
                if (listAuthorize.IndexOf(caseId.ToString()) != -1 || "All".Equals(listAuthorize[0]))
                {
                    if (!title.IsNullOrWhiteSpace() && !beginTime.IsNullOrWhiteSpace())
                    {
                        int result = caseBLO.NewOperationalEvent(beginTime, content, caseId, endTime, staffId, title);
                        return result;
                    }
                }
            }
            return 0;
        }

        public String DeleteOEvent()
        {
            int oEventId = int.Parse(Request.Params["element"]);
            int creatorId = caseBLO.GetStaffIdByEventId(oEventId);
            var staffId = int.Parse(Session["StaffId"].ToString());
            if (staffId == creatorId)
            {
                bool result = caseBLO.DeleteLegalEvent(oEventId);
                if (result)
                {
                    return "deleteEventSuccess";
                }
            }
            return "deleteEventFail";
        }

        [HttpPost]
        public String CreateCusAndAddToCase()
        {
            int caseId = int.Parse(Request.Params["caseId"]);
            string taxCode = Request.Params["taxCode"].Trim();
            string name = Request.Params["cusname"].Trim();
            int selectCustomerGroup;
            Int32.TryParse(Request.Params["selectCustomerGroup"], out selectCustomerGroup);
            string represent = Request.Params["represent"].Trim();
            string sex = Request.Params["sex"];
            string birthDay = Request.Params["birthDay"].Trim();
            string identityNum = Request.Params["identityNum"].Trim();
            string identityDate = Request.Params["identityDate"].Trim();
            string identityPlace = Request.Params["identityPlace"].Trim();
            string bankAccount = Request.Params["bankAccount"].Trim();
            string bankBranch = Request.Params["bankBranch"].Trim();
            string address = Request.Params["address"].Trim();
            string mobile = Request.Params["mobile"].Trim();
            string telephone = Request.Params["telephone"].Trim();
            string email = Request.Params["email"].Trim();

            int cusId = customerBLO.AddCustomer(taxCode, name, selectCustomerGroup, represent, sex, birthDay, identityNum, identityDate, identityPlace, bankAccount, bankBranch, address, mobile, telephone, email);
            if (cusId != 0)
            {
               return AddCaseCustomer(cusId, caseId);
            }
            return "error";
        }

        // Utilities ================================================================================

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
    }
}
