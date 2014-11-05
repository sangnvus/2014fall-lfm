using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Ajax.Utilities;


namespace LFMS.Models.DAO
{
    public class StaffDAO
    {
        LFMSEntities db;
        public StaffDAO()
        {
            db = new LFMSEntities();
        }

        public List<Staff> GetAllStaff()
        {
            var staffList = db.Staffs.ToList();
            return staffList;
        }
        public List<Role> GetAllRoleJson()
        {
            var staffRole = db.Roles.ToList();
            return staffRole;
        }
        public List<StaffGroup> GetAllGroupJson()
        {
            var StaffGroup = db.StaffGroups.ToList();
            return StaffGroup;
        }

        public List<Staff> ListAllStaff()
        {
            var StaffGroup = db.Staffs.ToList();
            return StaffGroup;
        }
        public Staff GetStaffByID(int staffId)
        {
            var staff = db.Staffs.Where(c => c.StaffId == staffId).FirstOrDefault();
            return staff;
        }
        public List<Staff> GetLawyerByOffice(int offId)
        {
            var off_staff = db.Office_Staff.Where(os => os.OfficeId == offId).ToList();
            List<Int32> list = new List<int>();
            foreach (var i in off_staff)
            {
                list.Add(int.Parse(i.StaffId.ToString()));
            }

            var staff = db.Staffs.Where(c => c.RoleId == 1 || list.Contains(c.StaffId)).ToList();
            return staff;
        }
        public Staff GetStaffInfoByID(int staffInfoId)
        {
            var staffInfo = db.Staffs.Where(c => c.StaffId == staffInfoId).FirstOrDefault();
            return staffInfo;
        }
        public List<Office_Staff> GetStaffOfficeByID(int staffOffId)
        {
            var staffOffice = db.Office_Staff.Where(c => c.StaffId == staffOffId).ToList();
            return staffOffice;
        }
        public StaffGroup GetStaffGroupById(int staffGroupId)
        {
            var staffgroup = db.StaffGroups.Where(c => c.StaffGroupId == staffGroupId).FirstOrDefault();
            return staffgroup;
        }
        public Role GetStaffRoleById(int staffGroupId)
        {
            var staffgroup = db.Roles.Where(c => c.RoleId == staffGroupId).FirstOrDefault();
            return staffgroup;
        }
        public bool AddStaff(string staffName, string staffPos, int roleId, int staffGroupId, string txtStaffHome, string txtStaffIdenNum, string txtStaffEmail, string txtStaffBankBranch,
            string txtStaffPhone, string txtStaffImage, string txtStaffSex, string txtStaffAdd, string txtStaffDayofbith, string txtStaffPlacebirth,
            string txtStaffIdenDay, string txtStaffIdenPlace, string txtStaffTax, string txtStaffBankNum, string txtUsername, int txtAppendantPeople, string txtPass, String[] cboOffice)
        {
            try
            {
                Staff staff = new Staff();
                staff.StaffName = staffName;
                staff.Position = staffPos;
                staff.RoleId = roleId;
                staff.StaffGroupId = staffGroupId;
                staff.Telephone = txtStaffHome;
                staff.IdentityNumber = txtStaffIdenNum;
                staff.Email = txtStaffEmail;
                staff.BankBranch = txtStaffBankBranch;
                staff.Mobile = txtStaffPhone;
                staff.Avatar = txtStaffImage;
                staff.Sex = txtStaffSex;
                staff.Address = txtStaffAdd;
                DateTime txtStaffDayofbithDt = DateTime.ParseExact(txtStaffDayofbith, "dd/MM/yyyy", null);
                staff.DateOfBirth = txtStaffDayofbithDt;
                staff.PlaceOfBirth = txtStaffPlacebirth;
                DateTime txtStaffIdenDayDt = DateTime.ParseExact(txtStaffIdenDay, "dd/MM/yyyy", null);
                staff.IdentityDate = txtStaffIdenDayDt;
                staff.IdentityPlace = txtStaffIdenPlace;
                staff.TaxCode = txtStaffTax;
                staff.BankAccount = txtStaffBankNum;
                staff.AppendantPeople = txtAppendantPeople;
                staff.Active = true;

                db.Staffs.Add(staff);

                var acc = new Account();
                acc.StaffId =  staff.StaffId;
                acc.Password = txtPass;
                acc.Username = txtUsername;

                db.Accounts.Add(acc);

   
                if (roleId != 1)
                {
                   
                    foreach (var off in cboOffice)
                    {
                        Office_Staff newOff = new Office_Staff();
                        newOff.StaffId = staff.StaffId;
                        int offInt= int.Parse(off);
                        newOff.OfficeId = offInt;
                        db.Office_Staff.Add(newOff);
                    }
                }

                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        public bool CheckPassword(int staffId, string password)
        {
            var staff = db.Accounts.Where(cs => cs.Password == password && cs.StaffId == staffId).FirstOrDefault();
            if (staff == null)
            {
                return false;
            }
            return true;
        }

        public bool UpdateStaffPass(int staffId, string passMd5)
        {
            var acc = db.Accounts.Where(a => a.StaffId == staffId).FirstOrDefault();
            if (acc != null)
            {
                try
                {
                    acc.Password = passMd5;
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

        public bool ResetStaffPass(int staffId, string passMd5)
        {
            Staff staff = GetStaffByID(staffId);
            try
            {
                staff.Accounts.FirstOrDefault().Password = passMd5;
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public bool UpdateStaff(int staffId, string staffName, string staffPos, int roleId, int staffGroupId, string txtStaffHome, string txtStaffIdenNum, string txtStaffEmail, string txtStaffBankBranch,
             string txtStaffPhone, string txtStaffImage, string txtStaffSex, string txtStaffAdd, string txtStaffDayofbith, string txtStaffPlacebirth,
            string txtStaffIdenDay, string txtStaffIdenPlace, string txtStaffTax, string txtStaffBankNum,int detailAppendantPeople, String[] newOffList)
        {
            try
            {
                //update bảng Staff
                Staff staff = GetStaffByID(staffId);
                if (staff == null)
                {
                    return false;
                }
                else
                {
                    staff.StaffId = staffId;
                    staff.StaffName = staffName;
                    staff.Position = staffPos;
                    staff.RoleId = roleId;
                    staff.StaffGroupId = staffGroupId;
                    staff.Telephone = txtStaffHome;
                    staff.IdentityNumber = txtStaffIdenNum;
                    staff.Email = txtStaffEmail;
                    staff.BankBranch = txtStaffBankBranch;
                    staff.Mobile = txtStaffPhone;
                    staff.Sex = txtStaffSex;
                    staff.Address = txtStaffAdd;

                    DateTime txtStaffDayofbithDt = DateTime.ParseExact(txtStaffDayofbith, "dd/MM/yyyy", null);
                    staff.DateOfBirth = txtStaffDayofbithDt;

                    staff.PlaceOfBirth = txtStaffPlacebirth;

                    DateTime txtStaffIdenDayDt = DateTime.ParseExact(txtStaffIdenDay, "dd/MM/yyyy", null);
                    staff.IdentityDate = txtStaffIdenDayDt;

                    staff.IdentityPlace = txtStaffIdenPlace;
                    staff.TaxCode = txtStaffTax;
                    staff.BankAccount = txtStaffBankNum;
                    staff.Avatar = txtStaffImage;
                    staff.AppendantPeople = detailAppendantPeople;

                }

                //update bảng Office_Staff
                List<Office_Staff> officeStaff = GetStaffOfficeByID(staffId);
                    foreach (var offremove in officeStaff)
                    {
                        db.Office_Staff.Remove(offremove);
                    }
                
          
                if (roleId != 1)
                {
                    foreach (var off in newOffList)
                    {
                        var newOff = new Office_Staff();
                        newOff.StaffId = staff.StaffId;
                        int offInt= int.Parse(off);
                        newOff.OfficeId = offInt;
                        db.Office_Staff.Add(newOff);
                    }
                }
                db.SaveChanges();
            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            return true;
        }

        public bool CheckExistUserName(string txtUsername)
        {
            var staff = db.Accounts.Where(cs => cs.Username == txtUsername).FirstOrDefault();
            if (staff == null)
            {
                return false;
            }
            return true;
        }
        public bool CheckExistGroupName(string txtCheckGroupName)
        {
            var checkgroup = db.StaffGroups.Where(cs => cs.StaffGroupName == txtCheckGroupName).FirstOrDefault();
            if (checkgroup == null)
            {
                return false;
            }
            return true;
        }
   
        public String SetStatusStaff(int staffId)
        {
            try
            {
                Staff staff = GetStaffByID(staffId);
                if ((bool)staff.Active)
                {
                    staff.Active = false;
                    db.SaveChanges();
                    return "inactive";

                }
                else staff.Active = true;
                db.SaveChanges();
                return "active";
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return "Error";
        }
        public bool AddStaffGroup(string staffGroupName, string staffGroupDetail, double txtBaseSalary)
        {
            StaffGroup staffGroup = new StaffGroup();
            staffGroup.StaffGroupName = staffGroupName;
            staffGroup.Description = staffGroupDetail;
            staffGroup.BaseSalary = txtBaseSalary;

            try
            {
                db.StaffGroups.Add(staffGroup);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        public bool UpdateStaffGroup(int StaffGrId, string staffGrName, string staffGrDetail, double txtEditMoney)
        {
            StaffGroup staffGroup = GetStaffGroupById(StaffGrId);
            if (staffGroup == null)
            {
                return false;
            }
            else
            {
                try
                {
                    staffGroup.StaffGroupName = staffGrName;
                    staffGroup.Description = staffGrDetail;
                    staffGroup.BaseSalary = txtEditMoney;
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
        public bool DeleteStaffGroup(int staffGroupId)
        {
            StaffGroup staffgroup = GetStaffGroupById(staffGroupId);
            if (staffgroup == null)
            {
                return false;
            }
            else
            {
                try
                {
                    db.StaffGroups.Remove(staffgroup);
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

        public List<int> GetOfficeHasCaseInWork(List<int> listCase)
        {
            try
            {
                var officeInwork = db.Cases.Where(cs => listCase.Contains(cs.CaseId) && cs.Status == "Đang thụ lý").DistinctBy(cs=>cs.OfficeId).Select(cs=>cs.OfficeId).ToList();
                return officeInwork;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        }
    }
}