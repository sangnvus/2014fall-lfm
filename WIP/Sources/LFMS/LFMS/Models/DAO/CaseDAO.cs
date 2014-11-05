using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Providers.Entities;
using System.Web.UI.WebControls.Expressions;

namespace LFMS.Models.DAO
{
    public class CaseDAO
    {
        LFMSEntities db;

        public CaseDAO()
        {
            db = new LFMSEntities();
        }

        public object GetCases(int displayNum, int orderKey, string orderType, int pageNum, string code,List<String> author)
        {
            var caseList = new List<Case>();
            
            if (orderKey == 0)
                caseList = "asc".Equals(orderType) ? db.Cases.Where(cs => cs.Active == true && (author.Contains(cs.CaseCode) || author.Contains("All") || cs.Status.Equals("Đã Thụ Lý"))).OrderBy(cs => cs.CaseCode).ToList() : db.Cases.Where(cs => cs.Active == true && (author.Contains(cs.CaseCode) || author.Contains("All") || cs.Status.Equals("Đã Thụ Lý"))).OrderByDescending(cs => cs.CaseCode).ToList();
            else if (orderKey == 1)
                caseList = "asc".Equals(orderType) ? db.Cases.Where(cs => cs.Active == true && (author.Contains(cs.CaseCode) || author.Contains("All") || cs.Status.Equals("Đã Thụ Lý"))).OrderBy(cs => cs.CaseContent).ToList() : db.Cases.Where(cs => cs.Active == true && (author.Contains(cs.CaseCode) || author.Contains("All") || cs.Status.Equals("Đã Thụ Lý"))).OrderByDescending(cs => cs.CaseContent).ToList();
            else if (orderKey == 2)
                caseList = "asc".Equals(orderType) ? db.Cases.Where(cs => cs.Active == true && (author.Contains(cs.CaseCode) || author.Contains("All") || cs.Status.Equals("Đã Thụ Lý"))).OrderBy(cs => cs.ReceiptDate).ToList() : db.Cases.Where(cs => cs.Active == true && (author.Contains(cs.CaseCode) || author.Contains("All") || cs.Status.Equals("Đã Thụ Lý"))).OrderByDescending(cs => cs.ReceiptDate).ToList();
            else if (orderKey == 3)
                caseList = "asc".Equals(orderType) ? db.Cases.Where(cs => cs.Active == true && (author.Contains(cs.CaseCode) || author.Contains("All") || cs.Status.Equals("Đã Thụ Lý"))).OrderBy(cs => cs.Office.OfficeName).ToList() : db.Cases.Where(cs => cs.Active == true && (author.Contains(cs.CaseCode) || author.Contains("All") || cs.Status.Equals("Đã Thụ Lý"))).OrderByDescending(cs => cs.Office.OfficeName).ToList();
            else if (orderKey == 4)
                caseList = "asc".Equals(orderType) ? db.Cases.Where(cs => cs.Active == true && (author.Contains(cs.CaseCode) || author.Contains("All") || cs.Status.Equals("Đã Thụ Lý"))).OrderBy(cs => cs.Status).ToList() : db.Cases.Where(cs => cs.Active == true && (author.Contains(cs.CaseCode) || author.Contains("All") || cs.Status.Equals("Đã Thụ Lý"))).OrderByDescending(cs => cs.Status).ToList();

            if (code != "")
            {
                caseList = caseList.Where(c => c.CaseCode.ToLower().Contains(code.ToLower()) || c.CaseContent.ToLower().Contains(code.ToLower())
                    || c.ReceiptDate.ToString("dd/MM/yyyy").Contains(code) || c.Office.OfficeName.ToLower().Contains(code.ToLower())).ToList();
            }

            var total = caseList.Count;
            var casePage = caseList.Skip(pageNum * displayNum).Take(displayNum).ToList();

            return new { casePage, total };
        }

        public Case GetCaseById(int id)
        {
            var cases = db.Cases.Where(cs => cs.CaseId == id).FirstOrDefault();
            return cases;
        }

        public List<Case> GetAssignedCases(int staffId)
        {
            var case_staff = db.Case_Staff.Where(c_s => c_s.StaffId == staffId).ToList();
            List<Int32> list = new List<int>();
            foreach (var i in case_staff)
            {
                list.Add(int.Parse(i.CaseId.ToString()));
            }

            var cs = db.Cases.Where(c => c.Status.Equals("Đang thụ lý") && list.Contains(c.CaseId)).ToList();
            return cs;
        }

        public string AddCase(string caseCode, int creatorId, string receiptDate, string caseContent, int officeId)
        {
            Case cs = new Case();
            cs.CaseCode = caseCode;
            cs.CreatorId = creatorId;
            DateTime date = DateTime.ParseExact(receiptDate, "dd/MM/yyyy", null);
            cs.ReceiptDate = date;
            cs.CaseContent = caseContent;
            cs.OfficeId = officeId;
            cs.Status = "Đang thụ lý";
            cs.Active = true;
            try
            {
                db.Cases.Add(cs);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return "error";
            }

            Case_Staff case_staff = new Case_Staff();
            case_staff.CaseId = cs.CaseId;
            case_staff.StaffId = creatorId;
            try
            {
                db.Case_Staff.Add(case_staff);
                db.SaveChanges();
                return cs.CaseId.ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return "error";
            }
        }

        public bool UpdateCaseDetail(int caseId, string receiptDate, int officeId, string status, string caseContent,
            string disputeSubject, string disputeRelation, string limitationStatute, string legalEvent, string errorFactor)
        {
            Case cs = GetCaseById(caseId);
            if (cs == null)
            {
                return false;
            }
            else
            {
                try
                {
                    DateTime date = DateTime.ParseExact(receiptDate, "dd/MM/yyyy", null);
                    cs.ReceiptDate = date;
                    cs.OfficeId = officeId;
                    cs.Status = status;
                    cs.CaseContent = caseContent;
                    cs.DisputeSubject = disputeSubject;
                    cs.DisputeRelation = disputeRelation;
                    cs.LimitationStatute = limitationStatute;
                    cs.LegalEvent = legalEvent;
                    cs.ErrorFactor = errorFactor;

                    db.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return false;
                }
            }
        }

        public bool UpdateLawyerViewpoint(int caseId, string protectiveGoal, string openingProcedure, 
            string inquiryProcedure, string argumentProcedure)
        {
            Case cs = GetCaseById(caseId);
            if (cs == null)
            {
                return false;
            }
            else
            {
                try
                {
                    cs.ProtectiveGoal = protectiveGoal;
                    cs.OpeningProcedure = openingProcedure;
                    cs.InquiryProcedure = inquiryProcedure;
                    cs.ArgumentProcedure = argumentProcedure;

                    db.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return false;
                }
            }
        }

        public bool CheckExistCaseCode(string caseCode)
        {
            var cases = db.Cases.Where(cs => cs.CaseCode == caseCode).FirstOrDefault();
            if (cases == null)
            {
                return false;
            }
            return true;
        }
        public Case GetCaseByCaseCode(string caseCode)
        {
            try
            {
                var cases = db.Cases.Where(cs => cs.CaseCode == caseCode).FirstOrDefault();
                return cases;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        }

        public Case_Customer GetCaseCustomerById(int id)
        {
            var caseCus = db.Case_Customer.Where(cs => cs.CaseCustomerId == id).FirstOrDefault();
            return caseCus;
        }

        public bool RemoveCustomerRelated(int id)
        {
            Case_Customer caseCustomer = GetCaseCustomerById(id);
            if (caseCustomer == null)
            {
                return false;
            }
            else
            {
                try
                {
                    db.Case_Customer.Remove(caseCustomer);
                    db.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return false;
                }
            }
        }

        public bool CheckExistCaseCustomer(int caseId, int customerId)
        {
            var caseCus = db.Case_Customer.Where(cs => cs.CaseId == caseId && cs.CustomerId == customerId).FirstOrDefault();
            if (caseCus == null)
            {
                return false;
            }
            return true;
        }

        public bool AddCaseCustomer(int caseId, int customerId)
        {
            Case_Customer cs = new Case_Customer();
            cs.CaseId = caseId;
            cs.CustomerId = customerId;
            try
            {
                db.Case_Customer.Add(cs);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public Case_Staff GetCaseStaffById(int id)
        {
            var caseStaff = db.Case_Staff.Where(cs => cs.CaseStaffId == id).FirstOrDefault();
            return caseStaff;
        }

        public bool RemoveLawyerRelated(int id)
        {
            Case_Staff caseStaff = GetCaseStaffById(id);
            if (caseStaff == null)
            {
                return false;
            }
            else
            {
                try
                {
                    db.Case_Staff.Remove(caseStaff);
                    db.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return false;
                }
            }
        }

        public bool CheckExistCaseStaff(int caseId, int staffId)
        {
            var caseStaff = db.Case_Staff.Where(cs => cs.CaseId == caseId && cs.StaffId == staffId).FirstOrDefault();
            if (caseStaff == null)
            {
                return false;
            }
            return true;
        }

        public bool AddCaseStaff(int caseId, int staffId)
        {
            Case_Staff cs = new Case_Staff();
            cs.CaseId = caseId;
            cs.StaffId = staffId;
            try
            {
                db.Case_Staff.Add(cs);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public List<Subject> GetAllSubjects(int caseId)
        {
            var subList = db.Subjects.Where(sub => sub.CaseId == caseId).ToList();
            return subList;
        }

        public bool AddSubject(string subjectName, string litigationCapacity, string address, string phoneNumber, string email, int caseId)
        {
            Subject sub = new Subject();
            sub.SubjectName = subjectName;
            sub.LitigationCapacity = litigationCapacity;
            sub.Address = address;
            sub.PhoneNumber = phoneNumber;
            sub.Email = email;
            sub.CaseId = caseId;
            try
            {
                db.Subjects.Add(sub);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public Subject GetSubjectById(int id)
        {
            var sub = db.Subjects.Where(s => s.SubjectId == id).FirstOrDefault();
            return sub;
        }

        public bool DeleteSubject(int id)
        {
            Subject sub = GetSubjectById(id);
            if (sub == null)
            {
                return false;
            }
            else
            {
                try
                {
                    db.Subjects.Remove(sub);
                    db.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return false;
                }
            }
        }

        public bool UpdateSubject(int subjectId, string subjectName, string litigationCapacity, string address, string phoneNumber, string email)
        {
            Subject sub = GetSubjectById(subjectId);
            if (sub == null)
            {
                return false;
            }
            else
            {
                try
                {
                    sub.SubjectName = subjectName;
                    sub.LitigationCapacity = litigationCapacity;
                    sub.Address = address;
                    sub.PhoneNumber = phoneNumber;
                    sub.Email = email;

                    db.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return false;
                }
            }
        }

        public string GetCreatorId(int caseId)
        {
            var cases = db.Cases.Where(cs => cs.CaseId == caseId).FirstOrDefault();
            return cases.CreatorId.ToString();
        }

        public int GetCaseIdByCaseCusId(int caseCusId)
        {
            try
            {
               var caseCus= new Case_Customer();
                caseCus = db.Case_Customer.Where(cc => cc.CaseCustomerId == caseCusId).FirstOrDefault();
                return caseCus.CaseId;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return 0;
            }
        }

        public int GetCaseIdByCaseStaffId(int caseStaffId)
        {
            try
            {
                var caseStaff = new Case_Staff();
                caseStaff = db.Case_Staff.Where(cs => cs.CaseStaffId == caseStaffId).FirstOrDefault();
                return caseStaff.CaseId;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return 0;
            }
        }

        public int GetStaffIdByCaseStaffId(int caseStaffId)
        {
            try
            {
                var caseStaff = new Case_Staff();
                caseStaff = db.Case_Staff.Where(cs => cs.CaseStaffId == caseStaffId).FirstOrDefault();
                return caseStaff.StaffId;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return 0;
            }
        }


        public int GetCaseIdBySubjectId(int subjectId)
        {
            try
            {
                var sub = new Subject();
                sub = db.Subjects.Where(s => s.SubjectId == subjectId).FirstOrDefault();
                return sub.CaseId;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return 0;
            }
        }
        public bool EditOperationalEvent(string bdate, string content, int oEventId,  string edate, string title)
        {
            try
            {
                OperationalEvent oEvent = db.OperationalEvents.Where(i => i.OperationalEventId == oEventId).FirstOrDefault();

                if (oEvent != null )
                {
                    oEvent.Description = content;
                    oEvent.BeginTime = DateTime.ParseExact(bdate, "d/M/yyyy H:mm", null);
                    oEvent.EndTime = DateTime.ParseExact(edate, "d/M/yyyy H:mm", null);
                    oEvent.Title = title;
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public int NewOperationalEvent(string beginTime, string content, int caseId, string endTime, int staffId, string title)
        {
            var oEvent = new OperationalEvent();
            oEvent.CaseId = caseId;
            DateTime bTime = DateTime.ParseExact(beginTime, "d/M/yyyy H:mm", null);
            oEvent.BeginTime = bTime;

            DateTime eTime = DateTime.ParseExact(endTime, "d/M/yyyy H:mm", null);
            oEvent.EndTime = eTime;
            oEvent.Title = title;
            oEvent.Description = content;
            if (staffId != 0)
            {
                oEvent.CreatorId = staffId;
            }

            try
            {
                db.OperationalEvents.Add(oEvent);
                db.SaveChanges();
                return oEvent.OperationalEventId;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return 0;
            }
        }

        public bool DeleteOperationalEvent(int operationalEventId)
        {
            try
            {
                OperationalEvent oEvent = db.OperationalEvents.Where(i => i.OperationalEventId == operationalEventId).FirstOrDefault();
                db.OperationalEvents.Remove(oEvent);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public int GetCreatorIdByEventId(int oEventId)
        {
            try
            {
                var oEvent = new OperationalEvent();
                oEvent = db.OperationalEvents.Where(s => s.OperationalEventId== oEventId).FirstOrDefault();
                return oEvent.CreatorId;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return 0;
            }
        }
        public object UpdateCost(int caseId)
        {
            try
            {
                var list = db.Cases.FirstOrDefault(i => i.CaseId == caseId);

                return list;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        public bool AddInvoice(int caseId, int serviceCost, string txtDescription, string datePickerService, string serviceName)
        {

            UsedService usedService = new UsedService();
            usedService.CaseId = caseId;
            usedService.Description = txtDescription;
            usedService.ServiceCost = serviceCost;
            usedService.ServiceName = serviceName;
            usedService.RegisteredDate = DateTime.ParseExact(datePickerService, "dd/MM/yyyy", null);

            try
            {
                db.UsedServices.Add(usedService);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public bool AddPayment(int caseId, int paymentCost, string txtDescription, string datePickerPayment)
        {

            Payment payment = new Payment();
            payment.CaseId = caseId;
            payment.Description = txtDescription;
            payment.PaymentMoney = paymentCost;
            payment.PaymentTime = DateTime.ParseExact(datePickerPayment, "dd/MM/yyyy", null);

            try
            {
                db.Payments.Add(payment);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public bool DeleteInvoice(int invoiceId)
        {

            UsedService usedService = db.UsedServices.Where(i => i.UsedServiceId == invoiceId).FirstOrDefault();

            try
            {
                db.UsedServices.Remove(usedService);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public bool DeletePayment(int paymentId)
        {

            Payment payment = db.Payments.Where(i => i.PaymentId == paymentId).FirstOrDefault();

            try
            {
                db.Payments.Remove(payment);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public bool UpdateInvoice(int invoiceId, int serviceCost, string txtDescription, string datePickerService)
        {
            try
            {

                UsedService usedService = db.UsedServices.Where(i => i.UsedServiceId == invoiceId).FirstOrDefault();
                usedService.ServiceCost = serviceCost;
                usedService.Description = txtDescription;
                usedService.RegisteredDate = DateTime.ParseExact(datePickerService, "dd/MM/yyyy", null);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }


        }

        public bool UpdatePayment(int paymentId, int paymentMoney, string txtDescription, string datePickerPayment)
        {
            try
            {

                Payment payment = db.Payments.Where(i => i.PaymentId == paymentId).FirstOrDefault();
                payment.PaymentMoney = paymentMoney;
                payment.Description = txtDescription;
                payment.PaymentTime = DateTime.ParseExact(datePickerPayment, "dd/MM/yyyy", null);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }


        }
        public int GetCaseIdByInvoiceId(int invoiceId)
        {
            try
            {
                var usedService = new UsedService();
                usedService = db.UsedServices.Where(s => s.UsedServiceId == invoiceId).FirstOrDefault();
                return usedService.CaseId;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return 0;
            }
        }

        public int GetCaseIdByPaymentId(int paymentId)
        {
            try
            {
                var payment = new Payment();
                payment = db.Payments.Where(s => s.PaymentId == paymentId).FirstOrDefault();
                return payment.CaseId;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return 0;
            }
        }

        public List<Account> GetListUsernameByStaffId(List<int> slist)
        {
            try
            {
                List<Account> acc = db.Accounts.Where(s => slist.Contains(s.StaffId)).ToList();
                return acc;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}