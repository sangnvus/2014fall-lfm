using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LFMS.Models.DAO
{
    public class UserDAO
    {
        LFMSEntities db;

        public UserDAO()
        {
            db = new LFMSEntities();
        }

        public Account GetUser(string username, string password)
        {
            try
            {
                var acc = db.Accounts.FirstOrDefault(s => s.Password == password && s.Username == username);
                if (acc != null)
                {
                    return acc;
                }
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public int GetRole(int staffId)
        {
            try
            {
                var staff = db.Staffs.FirstOrDefault(s => s.StaffId == staffId);
                if (staff != null) return staff.RoleId;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return 0;
        }

        public List<Case_Staff> GetAuthorize(int staffId)
        {
            try
            {
                var caseStaff = db.Case_Staff.Where(cs => cs.StaffId == staffId && cs.Case.Status == "Đang thụ lý" && cs.Case.Office.Active==true);
                return caseStaff.ToList() ;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new List<Case_Staff>(0);
            }
        }




    }
}