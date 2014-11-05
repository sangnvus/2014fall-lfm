using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Providers.Entities;
using LFMS.Models.DAO;
using LFMS.Utilities;
using Microsoft.Ajax.Utilities;
using WebGrease.Css.Extensions;

namespace LFMS.Models.BLO
{
    public class CaseBLO
    {
        private CaseDAO caseDAO;
        private UserDAO userDAO;

        public CaseBLO()
        {
            caseDAO = new CaseDAO();
            userDAO = new UserDAO();
        }

        public object GetCases(int displayNum, int orderKey, string orderType,int pageNum, string caseCode,List<String> author)
        {
            return caseDAO.GetCases(displayNum, orderKey, orderType ,pageNum, caseCode,author);
        }

        public Case GetCaseById(int id)
        {
            return caseDAO.GetCaseById(id);
        }

        public List<Object> GetListUsernameByStaffId(List<int> list)
        {
            var listAccount = caseDAO.GetListUsernameByStaffId(list);
            var level1 = new List<string>();
            level1.Add("Staff");
            var exceptList = new List<string>();
            exceptList.Add("Password");
           var result = new List<object>();
            foreach (var account in listAccount)
            {
                var acc = UtilityClass.ConvertDynamicObjectWithCustomAttr(account, exceptList, level1);
                result.Add(acc);
            }

            return result;
        }
        public List<Object> GetAssignedCases(int staffId)
        {
            var caseDAO = new CaseDAO();
            List<Case> listCase = caseDAO.GetAssignedCases(staffId);
            var list = new List<Object>();

            foreach (var staff in listCase)
            {
                var result = UtilityClass.ConvertDynamicObjectWithFullAttr(staff);
                list.Add(result);
            }
            return list;
        }

        public Object GetJsonCaseDetailById(int caseId)
        {
            var cs = caseDAO.GetCaseById(caseId);

            var level1 = new List<string>();
            level1.Add("Office");

            var result = UtilityClass.ConvertDynamicObjectWithFullAttr(cs, level1);
            return result;
        }

        public string AddCase(string caseCode, int creatorId, string receiptDate, string caseContent, int officeId)
        {
            if (!caseCode.IsNullOrWhiteSpace() && !receiptDate.IsNullOrWhiteSpace() && !caseContent.IsNullOrWhiteSpace() && officeId > 0)
            {
                return caseDAO.AddCase(caseCode, creatorId, receiptDate, caseContent, officeId);
            }
            return "error";
        }

        public bool UpdateCaseDetail(int caseId, string receiptDate, int officeId, string status, string caseContent,
            string disputeSubject, string disputeRelation, string limitationStatute, string legalEvent, string errorFactor)
        {
            if (!receiptDate.IsNullOrWhiteSpace() && officeId > 0 && !caseContent.IsNullOrWhiteSpace())
            {
                return caseDAO.UpdateCaseDetail(caseId, receiptDate, officeId, status, caseContent,
                disputeSubject, disputeRelation, limitationStatute, legalEvent, errorFactor);
            }
            return false;
        }

        public bool UpdateLawyerViewpoint(int caseId, string protectiveGoal, string openingProcedure,
            string inquiryProcedure, string argumentProcedure)
        {
            return caseDAO.UpdateLawyerViewpoint(caseId, protectiveGoal, openingProcedure, inquiryProcedure, argumentProcedure);
        }

        public bool CheckExistCaseCode(string caseCode)
        {
            return caseDAO.CheckExistCaseCode(caseCode);
        }

        public Object GetCost(int caseId)
        {
            var invoiceCase = caseDAO.GetCaseById(caseId);

            var level1 = new List<string>();
            level1.Add("UsedServices");
            level1.Add("Payments");
            level1.Add("Office");
            level1.Add("Case_Customer");
            var level2 = new List<string>();
            level2.Add("Customer");
        
            var result = UtilityClass.ConvertDynamicObjectWithFullAttr(invoiceCase, level1,level2);
            return result;
        }

        public Object GetEvent(int caseId)
        {
           return caseDAO.GetCaseById(caseId);

        }

        public Object GetCustomerRelated(int caseId)
        {
            var cs = caseDAO.GetCaseById(caseId);

            var level1 = new List<string>();
            level1.Add("Case_Customer");
            var level2 = new List<string>();
            level2.Add("Customer");
            var level3 = new List<string>();
            level3.Add("CustomerGroup");

            var result = UtilityClass.ConvertDynamicObjectWithFullAttr(cs, level1, level2, level3);
            return result;
        }

        public bool RemoveCustomerRelated(int id)
        {
            return caseDAO.RemoveCustomerRelated(id);
        }

        public int GetCaseIdByCaseCusId(int caseCusId)
        {
            return caseDAO.GetCaseIdByCaseCusId(caseCusId);

        }

        public bool CheckExistCaseCustomer(int caseId, int customerId)
        {
            return caseDAO.CheckExistCaseCustomer(caseId, customerId);
        }

        public bool AddCaseCustomer(int caseId, int customerId)
        {
            return caseDAO.AddCaseCustomer(caseId, customerId);
        }

        public Object GetLawyerRelated(int caseId)
        {
            var cs = caseDAO.GetCaseById(caseId);

            var level1 = new List<string>();
            level1.Add("Case_Staff");
            var level2 = new List<string>();
            level2.Add("Staff");
            var level3 = new List<string>();
            level3.Add("StaffGroup");
            level3.Add("StaffInformations");
            level3.Add("Role");
            level3.Add("Office_Staff");
            level3.Add("Accounts");

            var exceptList = new List<string>();
            exceptList.Add("Password");
            var result = UtilityClass.ConvertDynamicObjectWithCustomAttr(cs, exceptList, level1, level2, level3);
            return result;
        }

        public bool RemoveLawyerRelated(int id)
        {
            return caseDAO.RemoveLawyerRelated(id);
        }

        public int GetStaffIdByCaseStaffId(int caseStaffId)
        {
            return caseDAO.GetStaffIdByCaseStaffId(caseStaffId);
        }
        public int GetCaseIdByCaseStaffId(int caseStaffId)
        {
            return caseDAO.GetCaseIdByCaseStaffId(caseStaffId);
        }

        public bool CheckExistCaseStaff(int caseId, int staffId)
        {
            return caseDAO.CheckExistCaseStaff(caseId, staffId);
        }

        public bool AddCaseStaff(int caseId, int staffId)
        {
            return caseDAO.AddCaseStaff(caseId, staffId);
        }

        public List<Subject> GetAllSubjects(int caseId)
        {
            return caseDAO.GetAllSubjects(caseId);
        }

        public bool AddSubject(string subjectName, string litigationCapacity, string address, string phoneNumber, string email, int caseId)
        {
            if (!subjectName.IsNullOrWhiteSpace() && !litigationCapacity.IsNullOrWhiteSpace())
            {
                return caseDAO.AddSubject(subjectName, litigationCapacity, address, phoneNumber, email, caseId);
            }
            return false;
        }

        public bool DeleteSubject(int subjectId)
        {
            return caseDAO.DeleteSubject(subjectId);
        }

        public int GetCaseIdBySubjectId(int subjectId)
        {
            return caseDAO.GetCaseIdBySubjectId(subjectId);
        }

        public bool UpdateSubject(int subjectId, string subjectName, string litigationCapacity, string address, string phoneNumber, string email)
        {
            if (!subjectName.IsNullOrWhiteSpace() && !litigationCapacity.IsNullOrWhiteSpace())
            {
                return caseDAO.UpdateSubject(subjectId, subjectName, litigationCapacity, address, phoneNumber, email);
            }
            return false;
        }

        public bool CanEditLawyer(int staffId, int caseId)
        {
            if (userDAO.GetRole(staffId) == 1 || caseDAO.GetCreatorId(caseId) == staffId.ToString())
            {
                return true;
            }
            return false;
        }

        public bool IsCreator(int staffId, int caseId)
        {
            if (caseDAO.GetCreatorId(caseId) == staffId.ToString())
            {
                return true;
            }
            return false;
        }

        
        public bool EditLegalEvent(string bdate, string content, int oEventId, string edate, string title)
        {
            return caseDAO.EditOperationalEvent(bdate, content, oEventId, edate, title);
        }

        public int NewOperationalEvent(string beginTime, string content, int caseId, string endTime, int staffId, string title)
        {
            return caseDAO.NewOperationalEvent(beginTime, content, caseId, endTime, staffId, title);
        }

        public bool DeleteLegalEvent(int oEventId)
        {
            return caseDAO.DeleteOperationalEvent(oEventId);
        }

        public int GetStaffIdByEventId(int oEventId)
        {
            return caseDAO.GetCreatorIdByEventId(oEventId);
        }

        public bool AddInvoice(int caseId, int serviceCost, string txtDescription, string datePickerService, string serviceName)
        {
            return caseDAO.AddInvoice(caseId, serviceCost, txtDescription, datePickerService, serviceName);
        }
        internal bool AddPayment(int caseId, int paymentCost, string txtDescription, string datePickerPayment)
        {
            return caseDAO.AddPayment(caseId, paymentCost, txtDescription, datePickerPayment);
        }


        internal bool DeleteInvoice(int invoiceId)
        {
            return caseDAO.DeleteInvoice(invoiceId);
        }

        public bool DeletePayment(int paymentId)
        {
            return caseDAO.DeletePayment(paymentId);
        }

        internal bool UpdateInvoice(int invoiceId, int serviceCost, string txtDescription, string datePickerService)
        {
            return caseDAO.UpdateInvoice(invoiceId, serviceCost, txtDescription, datePickerService);
        }

        public bool UpdatePayment(int paymentId, int paymentCost, string txtDescription, string datePickerPayment)
        {
            return caseDAO.UpdatePayment(paymentId, paymentCost, txtDescription, datePickerPayment);
        }
        public object UpdateCost(int caseId, string code)
        {

            var list = caseDAO.UpdateCost(caseId);
            var level1 = new List<string>();
            level1.Add(code);
            return UtilityClass.ConvertDynamicObjectWithFullAttr(list, level1);

        }

        public int GetCaseIdByInvoiceId(int invoiceId)
        {
            return caseDAO.GetCaseIdByInvoiceId(invoiceId);
        }
        public int GetCaseIdByPaymentId(int paymentId)
        {
            return caseDAO.GetCaseIdByPaymentId(paymentId);
        }

        public bool CheckStaffInOffice(int newOfficeId, int caseId)
        {
            
           var caseStaff = caseDAO.GetCaseById(caseId).Case_Staff.ToList();
            foreach (var staff in caseStaff)
            {
                if (staff.Staff.RoleId != 1)
                {
                    var listOffice = staff.Staff.Office_Staff.Select(off => off.OfficeId).ToList();
                    if (listOffice.IndexOf(newOfficeId)==-1)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public Case GetCaseByCaseCode(string caseCode)
        {
            return caseDAO.GetCaseByCaseCode(caseCode);
        }
    }
}