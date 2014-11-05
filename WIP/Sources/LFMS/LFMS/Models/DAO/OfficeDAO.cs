using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace LFMS.Models.DAO
{
    public class OfficeDAO
    {
        LFMSEntities db;
        public OfficeDAO()
        {
            db = new LFMSEntities();
        }
        public List<Office> GetActiveOffice()
        {
            var officeList = db.Offices.Where(of => of.Active == true).ToList();
            return officeList;
        }

        public List<Office> GetAllOffice()
        {
            var officeList = db.Offices.ToList();
            return officeList;
        }
        public List<Office> GetOfficeStaff()
        {
            var officeList = db.Offices.Where(c => c.Active == true).ToList();
            return officeList;
        }

        public Office GetOfficeByID(int offId)
        {
            var office = db.Offices.Where(c => c.OfficeId == offId).FirstOrDefault();
            return office;
        }
        public bool AddOffice(string offName, string offManager, string offTaxcode, string offAdd,
            string offPhone, string offFax, string offEmail, string offWebsite, string offbankAccount, string offbankName)
        {
            Office office = new Office();
            office.OfficeName = offName;
            office.Manager = offManager;
            office.TaxCode = offTaxcode;
            office.Address = offAdd;
            office.PhoneNumber = offPhone;
            office.Fax = offFax;
            office.Email = offEmail;
            office.Website = offWebsite;
            office.BankAccount = offbankAccount;
            office.BankBranch = offbankName;
            office.Active = true;
            try
            {
                db.Offices.Add(office);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }


        public bool UpdateOffice(int offId, string offName, string offManager, string offTaxcode, string offAdd,
            string offPhone, string offFax, string offEmail, string offWebsite, string offbankAccount, string offbankName)
        {
            Office office = GetOfficeByID(offId);
            if (office == null)
            {
                return false;
            }
            else
            {
                try
                {
                    office.OfficeName = offName;
                    office.Manager = offManager;
                    office.TaxCode = offTaxcode;
                    office.Address = offAdd;
                    office.PhoneNumber = offPhone;
                    office.Fax = offFax;
                    office.Email = offEmail;
                    office.Website = offWebsite;
                    office.BankAccount = offbankAccount;
                    office.BankBranch = offbankName;
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

        public String SetStatusOffice(int id)
        {
            try
            {
                Office office = GetOfficeByID(id);
                if ((bool)office.Active)
                {
                    office.Active = false;
                    db.SaveChanges();
                    return "inactive";

                }
                else office.Active = true;
                db.SaveChanges();
                return "active";
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        public bool CheckExistOfficeName(string txtOfficename)
        {
            var staff = db.Offices.Where(cs => cs.OfficeName == txtOfficename).FirstOrDefault();
            if (staff == null)
            {
                return false;
            }
            return true;
        }
    }
}