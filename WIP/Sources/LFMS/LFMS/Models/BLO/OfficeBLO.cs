using System;
using LFMS.Models.DAO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Ajax.Utilities;
using LFMS.Utilities;

namespace LFMS.Models.BLO
{
    public class OfficeBLO
    {
        private OfficeDAO officeDAO;
        private StaffDAO staffDAO;

        public OfficeBLO()
        {
            officeDAO = new OfficeDAO();
            staffDAO = new StaffDAO();
        }

        public List<Office> GetActiveOffice()
        {
            return officeDAO.GetActiveOffice();
        }

        public List<Office> GetAllOffice()
        {
            return officeDAO.GetAllOffice();
        }
        public List<Office> GetOfficeStaff()
        {
            return officeDAO.GetOfficeStaff();
        }

        public Office GetOfficeByID(int offId)
        {
            return officeDAO.GetOfficeByID(offId);
        }

        public Object GetOfficesByStaffId(int staffId)
        {
            var st = staffDAO.GetStaffByID(staffId);

            var level1 = new List<string>();
            level1.Add("Office_Staff");
            var level2 = new List<string>();
            level2.Add("Office");

            var result = UtilityClass.ConvertDynamicObjectWithFullAttr(st, level1, level2);
            return result;
        }

        public bool AddOffice(string offName, string offManager, string offTaxcode, string offAdd,
                 string offPhone, string offFax, string offEmail, string offWebsite, string offbankAccount, string offbankName)
        {
            if (!offName.IsNullOrWhiteSpace() && !offAdd.IsNullOrWhiteSpace() && !offManager.IsNullOrWhiteSpace() && !offPhone.IsNullOrWhiteSpace() && !offEmail.IsNullOrWhiteSpace())
            {
                return officeDAO.AddOffice(offName, offManager, offTaxcode, offAdd, offPhone, offFax, offEmail, offWebsite, offbankAccount, offbankName);
            }
            return false;
        }


        public bool UpdateOffice(int offId, string offName, string offManager, string offTaxcode, string offAdd,
                 string offPhone, string offFax, string offEmail, string offWebsite, string offbankAccount, string offbankName)
        {
            if (!offName.IsNullOrWhiteSpace() && !offAdd.IsNullOrWhiteSpace() && !offManager.IsNullOrWhiteSpace() && !offPhone.IsNullOrWhiteSpace() && !offEmail.IsNullOrWhiteSpace())
            {
                return officeDAO.UpdateOffice(offId, offName, offManager, offTaxcode, offAdd, offPhone, offFax, offEmail, offWebsite, offbankAccount, offbankName);
            }
            return false;
        }

        public string SetStatusOffice(int offId)
        {
            return officeDAO.SetStatusOffice(offId);
        }

        public bool CheckExistOfficeName(string txtOfficename)
        {
            return officeDAO.CheckExistOfficeName(txtOfficename);
        }
    }
}