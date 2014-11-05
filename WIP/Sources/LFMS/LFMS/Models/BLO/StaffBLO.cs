using System;
using LFMS.Models.DAO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LFMS.Utilities;
using Microsoft.Ajax.Utilities;
using System.Text.RegularExpressions;

namespace LFMS.Models.BLO
{
    public class StaffBLO
    {
        private StaffDAO staffDAO;
        private OfficeDAO officeDAO;

        public StaffBLO()
        {
            staffDAO = new StaffDAO();
            officeDAO = new OfficeDAO();
        }

        public List<Staff> GetAllStaff()
        {
            return staffDAO.GetAllStaff();
        }

        public List<Object> GetAllStaffJson()
        {
            var staff = staffDAO.GetAllStaff();

            var level1 = new List<string>();
            ////level1.Add("StaffInformations");
            level1.Add("StaffGroup");
            level1.Add("Office_Staff");
            level1.Add("Role");
            level1.Add("Accounts");
            var level2 = new List<string>();
            level2.Add("Office");
            var except = new List<string>();
            except.Add("Password");
            var list = new List<object>();
            foreach (var cus in staff)
            {
                var result = UtilityClass.ConvertDynamicObjectWithCustomAttr(cus, except, level1, level2);
                list.Add(result);
            }

            return list;
        }

        public List<Object> GetLawyerByOffice(int officeId)
        {
            var staffDAO = new StaffDAO();
            List<Staff> listStaff = staffDAO.GetLawyerByOffice(officeId);
            var list = new List<Object>();

            var level1 = new List<string>();
            level1.Add("Accounts");
            var except = new List<string>();
            except.Add("Password");

            foreach (var staff in listStaff)
            {
                var result = UtilityClass.ConvertDynamicObjectWithCustomAttr(staff, except, level1);
                list.Add(result);
            }
            return list;
        }

        public Object GetAllRoleJson()
        {
            var staffRole = staffDAO.GetAllRoleJson();
            var list = new List<object>();
            foreach (var role in staffRole)
            {
                var result = UtilityClass.ConvertDynamicObjectWithFullAttr(role);
                list.Add(result);
            }
            return list;
        }

        public Object GetAllGroupJson()
        {

            var staffGroup = staffDAO.GetAllGroupJson();
            var list = new List<object>();
            foreach (var group in staffGroup)
            {
                var result = UtilityClass.ConvertDynamicObjectWithFullAttr(group);
                list.Add(result);
            }
            return list;
        }
        public Object ListAllStaff()
        {

            var staffGroup = staffDAO.ListAllStaff();
            var list = new List<object>();
            foreach (var group in staffGroup)
            {
                var result = UtilityClass.ConvertDynamicObjectWithFullAttr(group);
                list.Add(result);
            }
            return list;
        }

        public Staff GetStaffByID(int staffId)
        {
            return staffDAO.GetStaffByID(staffId);
        }

        public bool AddStaff(string staffName, string staffPos, int roleId, int staffGroupId, string txtStaffHome, string txtStaffIdenNum, string txtStaffEmail, string txtStaffBankBranch,
            string txtStaffPhone, string txtStaffImage, string txtStaffSex, string txtStaffAdd, string txtStaffDayofbith, string txtStaffPlacebirth,
            string txtStaffIdenDay, string txtStaffIdenPlace, string txtStaffTax, string txtStaffBankNum, string txtUsername,int txtAppendantPeople, String[] cboOffice)
        {
            if (!staffName.IsNullOrWhiteSpace() && !txtUsername.IsNullOrWhiteSpace() && !txtStaffPhone.IsNullOrWhiteSpace() && !txtStaffEmail.IsNullOrWhiteSpace() && !txtStaffAdd.IsNullOrWhiteSpace() && !txtStaffIdenNum.IsNullOrWhiteSpace() && staffGroupId > 0 && roleId > 0)
            {

                string s = "^[a-zA-Z0-9]*$";
                Regex rg = new Regex(s);
                if (rg.IsMatch(txtUsername))
                {
                    string txtPass = md5Encode.Md5Encode("123456");
                    return staffDAO.AddStaff(staffName, staffPos, roleId, staffGroupId, txtStaffHome, txtStaffIdenNum, txtStaffEmail, txtStaffBankBranch, txtStaffPhone,
                    txtStaffImage, txtStaffSex, txtStaffAdd, txtStaffDayofbith, txtStaffPlacebirth, txtStaffIdenDay, txtStaffIdenPlace, txtStaffTax, txtStaffBankNum, txtUsername,txtAppendantPeople, txtPass, cboOffice);
                }
            }
            return false;

        }

        public bool UpdateStaff(int staffId, string staffName, string staffPos, int roleId, int staffGroupId, string txtStaffHome, string txtStaffIdenNum, string txtStaffEmail, string txtStaffBankBranch,
            string txtStaffPhone, string txtStaffImage, string txtStaffSex, string txtStaffAdd, string txtStaffDayofbith, string txtStaffPlacebirth,
            string txtStaffIdenDay, string txtStaffIdenPlace, string txtStaffTax, string txtStaffBankNum,int detailAppendantPeople, String[] newOffList)
        {
            if (!staffName.IsNullOrWhiteSpace() && !txtStaffPhone.IsNullOrWhiteSpace() && !txtStaffEmail.IsNullOrWhiteSpace() && !txtStaffAdd.IsNullOrWhiteSpace() && !txtStaffIdenNum.IsNullOrWhiteSpace() && staffGroupId > 0 && roleId > 0)
            {
                if (("".Equals(newOffList[0])) && roleId != 1)
                {
                    return false;
                }
                 return staffDAO.UpdateStaff(staffId, staffName, staffPos, roleId, staffGroupId, txtStaffHome, txtStaffIdenNum, txtStaffEmail, txtStaffBankBranch, txtStaffPhone,
                   txtStaffImage, txtStaffSex, txtStaffAdd, txtStaffDayofbith, txtStaffPlacebirth, txtStaffIdenDay, txtStaffIdenPlace, txtStaffTax, txtStaffBankNum, detailAppendantPeople, newOffList);
            }
            return false;
        }

        public bool UpdateStaffPass(int staffId, string password2)
        {
            if (!password2.IsNullOrWhiteSpace())
            {
                string s = "^[a-zA-Z0-9]*$";
                Regex rg = new Regex(s);
                if (rg.IsMatch(password2))
                {
                    return staffDAO.UpdateStaffPass(staffId, password2);
                }
            }
            return false;
        }
        public bool ResetStaffPass(int staffId)
        {
            string pass = md5Encode.Md5Encode("123456");
            return staffDAO.ResetStaffPass(staffId, pass);
        }
        //public bool DeleteStaffInfo(int staffId)
        //{
        //    return staffDAO.DeleteStaffInfo(staffId);
        //}
        //public bool ActiveStaff(int staffId)
        //{
        //    return staffDAO.ActiveStaff(staffId);
        //}
        public string SetStatusStaff(int staffId)
        {
            return staffDAO.SetStatusStaff(staffId);
        }

        public bool AddStaffGroup(string staffGroupName, string staffGroupDetail, double txtBaseSalary)
        {
            return staffDAO.AddStaffGroup(staffGroupName, staffGroupDetail,txtBaseSalary);
        }
        public bool UpdateStaffGroup(int StaffGrId, string staffGrName, string staffGrDetail, double txtEditMoney)
        {
            return staffDAO.UpdateStaffGroup(StaffGrId, staffGrName, staffGrDetail, txtEditMoney);
        }
        public bool DeleteStaffGroup(int staffGroupId)
        {
            return staffDAO.DeleteStaffGroup(staffGroupId);
        }
        public bool CheckExistUserName(string txtUsername)
        {
            return staffDAO.CheckExistUserName(txtUsername);
        }
        public bool CheckExistGroupName(string txtCheckGroupName)
        {
            return staffDAO.CheckExistGroupName(txtCheckGroupName);
        }
        public bool CheckPassword(int staffId, string password)
        {
            return staffDAO.CheckPassword(staffId, password);
        }



        public bool CheckOfficeInWork(string[] newOfficeList, int newRoleId, int staffId)
        {
            if (newRoleId != 1)
            {
                var changeStaff = GetStaffByID(staffId);
                var caseList = changeStaff.Case_Staff.Select(cs => cs.CaseId).ToList();
                var caseInWork = GetOfficeHasCaseInWork(caseList);
                List<int> newOff = new List<int>();
                foreach (var off in newOfficeList)
                {
                    newOff.Add(int.Parse(off));
                }
                foreach (var workCase in caseInWork)
                {
                    if (!newOff.Contains(workCase))
                    {
                        return false;
                    }
                }

            }
            return true;
        }

        public List<int> GetOfficeHasCaseInWork(List<int> listCase)
        {
            return staffDAO.GetOfficeHasCaseInWork(listCase);
        }
        public bool CheckEditableStaff(int sessionStaffId, int newRole, int changeStaffId)
        {
            var changeStaff = GetStaffByID(changeStaffId);
            if ((changeStaff.RoleId == 1 || newRole == 1) && sessionStaffId != 1)
            {
                return false;
            }
            if (changeStaffId == 1 && changeStaff.RoleId!=1)
            {
                return false;
            }
            return true;
        }
    }
}