using System;
using LFMS.Models.DAO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LFMS.Utilities;
using Microsoft.Ajax.Utilities;
using System.Text.RegularExpressions;
using LFMS.Models.ConcerEntity;

namespace LFMS.Models.BLO
{
    public class SalaryBLO
    {
       private SalaryDAO salaryDAO;

       public SalaryBLO()
        {
            salaryDAO = new SalaryDAO();
         
        }   

        public List<Staff> GetAllStaff()
        {
            return salaryDAO.GetAllStaff();
        }

        public List<MonthInYear> getMonthInYear()
        {
            return salaryDAO.getMonthInYear();
        }
        public List<getYear> getYear()
        {
            return salaryDAO.getYear();
        }

        public List<Object> GetAllSalaryJson()
        {
            var staff = salaryDAO.GetAllSalary();
            var level1 = new List<string>();
            level1.Add("Staff");
            level1.Add("SalaryAssuranceDetails");
            level1.Add("SalaryBenefitDetails");
            level1.Add("SalaryTaxDetails");
             var level2 = new List<string>();
             level2.Add("TimeSheets");
    
            var list = new List<object>();
            foreach (var cus in staff)
            {
                var result = UtilityClass.ConvertDynamicObjectWithFullAttr(cus, level1,level2);
                list.Add(result);
            }

            return list;
        }

        //public List<Object> getSalarybyMonthJson(int monthId)
        //{
        //    var staff = salaryDAO.getSalarybyMonthJson(monthId);
        //    var level1 = new List<string>();
        //    level1.Add("Staff");
        //    level1.Add("SalaryAssuranceDetails");
        //    level1.Add("SalaryBenefitDetails");
        //    level1.Add("SalaryTaxDetails");
        //    var level2 = new List<string>();
        //    level2.Add("TimeSheets");

        //    var list = new List<object>();
        //    foreach (var cus in staff)
        //    {
        //        var result = UtilityClass.ConvertDynamicObjectWithFullAttr(cus, level1,level2);
        //        list.Add(result);
        //    }

        //    return list;
        //}
        public List<Object> getSalarybyMonthandYear(int monthId, int yearId)
        {
            var staff = salaryDAO.getSalarybyMonthandYear(monthId, yearId);
            var level1 = new List<string>();
            level1.Add("Staff");
            level1.Add("SalaryAssuranceDetails");
            level1.Add("SalaryBenefitDetails");
            level1.Add("SalaryTaxDetails");
            var level2 = new List<string>();
            level2.Add("TimeSheets");

            var list = new List<object>();
            foreach (var cus in staff)
            {
                var result = UtilityClass.ConvertDynamicObjectWithFullAttr(cus, level1, level2);
                list.Add(result);
            }

            return list;
        }

        //public List<Object> getCurrentImpressJson(int staffId)
        //{
        //    var staff = salaryDAO.getCurrentImpressJson(staffId);
        //     var list = new List<object>();
        //    foreach (var cus in staff)
        //    {
        //        var result = UtilityClass.ConvertDynamicObjectWithFullAttr(cus);
        //        list.Add(result);
        //    }

        //    return list;
        //}
               
        public Object GetAllTaxJson()
        {

            var staffTax = salaryDAO.GetAllTaxJson();
            var list = new List<object>();
            foreach (var tax in staffTax)
            {
                var result = UtilityClass.ConvertDynamicObjectWithFullAttr(tax);
                list.Add(result);
            }
            return list;
        }



        //public List<Object> getStaffTimesheetId(int StaffId)
        //{
        //    var staffImpresses = salaryDAO.getStaffTimesheetId(StaffId);
        //    var list = new List<Object>();
        //    foreach (var cus in staffImpresses)
        //    {
        //        var result = UtilityClass.ConvertDynamicObjectWithFullAttr(cus);
        //        list.Add(result);
        //    }

        //    return list;
        //}

        public List<Tax> GetAllTax()
        {
            return salaryDAO.GetAllTax();
        }

        public List<Benefit> GetSelectBenefit()
        {
            return salaryDAO.GetSelectBenefit();
        }

        public List<Assurance> GetSelectAssurance()
        {
            return salaryDAO.GetSelectAssurance();
        }

        public bool AddStaffSalary(int selectStaffId, string txtDateCountSalary, String[] cboOffice, String[] assList)
        {
            return salaryDAO.AddStaffSalary(selectStaffId, txtDateCountSalary, cboOffice, assList);
               
            }

        public bool UpdateStaffSalary(int salaryId, int selectStaffId, string txtDateCountSalary, String[] cboOffice, String[] assList)
        {
            return salaryDAO.UpdateStaffSalary(salaryId, selectStaffId, txtDateCountSalary, cboOffice, assList);

        }
        
        public Object getAllAssuranceJson()
        {

            var staffAssuran = salaryDAO.getAllAssuranceJson();
            var list = new List<object>();
            foreach (var assuran in staffAssuran)
            {
                var result = UtilityClass.ConvertDynamicObjectWithFullAttr(assuran);
                list.Add(result);
            }
            return list;
        }


        public List<Object> getAllImpressJson()
        {
            var staffImpresses = salaryDAO.getAllImpress();
            var level1 = new List<string>();
            level1.Add("Staff");

             var list = new List<Object>();
                foreach (var cus in staffImpresses)
            {
                var result = UtilityClass.ConvertDynamicObjectWithFullAttr(cus, level1);
                list.Add(result);
            }

            return list;
        }
        public List<Object> getImpressByStaffId(int staffId,int monthImpressId, int yearImpressId)
        {
            var staffImpresses = salaryDAO.getImpressByStaffId(staffId, monthImpressId, yearImpressId);
            var level1 = new List<string>();
            level1.Add("Staff");

            var list = new List<Object>();
            foreach (var cus in staffImpresses)
            {
                var result = UtilityClass.ConvertDynamicObjectWithFullAttr(cus, level1);
                list.Add(result);
            }

            return list;
        }

        public List<Object> getAllRewardJson()
        {
            var staffReward = salaryDAO.getAllReward();
            var level1 = new List<string>();
            level1.Add("Staff");

            var list = new List<Object>();
            foreach (var cus in staffReward)
            {
                var result = UtilityClass.ConvertDynamicObjectWithFullAttr(cus, level1);
                list.Add(result);
            }

            return list;
        }
        public List<Object> getRewardbyStaffId(int staffId,int monthRewwardId,int yearRewardId)
        {
            var staffReward = salaryDAO.getRewardbyStaffId(staffId, monthRewwardId, yearRewardId);
            var level1 = new List<string>();
            level1.Add("Staff");

            var list = new List<Object>();
            foreach (var cus in staffReward)
            {
                var result = UtilityClass.ConvertDynamicObjectWithFullAttr(cus, level1);
                list.Add(result);
            }

            return list;
        }
        

        //public List<Object> GetTimesheet()
        //{
        //    var staffImpresses = salaryDAO.GetTimesheet();
        //    var list = new List<Object>();
        //    foreach (var cus in staffImpresses)
        //    {
        //        var result = UtilityClass.ConvertDynamicObjectWithFullAttr(cus);
        //        list.Add(result);
        //    }

        //    return list;
        //}
        public List<Object> GetTimeSheetByStaffId(int staffId, int monthTimsheetId, int yearTimsheetId)
        {
            var staff = salaryDAO.GetTimeSheetByStaffId(staffId,monthTimsheetId ,yearTimsheetId);
            var level1 = new List<string>();
            level1.Add("Staff");

            var list = new List<object>();
            foreach (var cus in staff)
            {
                var result = UtilityClass.ConvertDynamicObjectWithFullAttr(cus, level1);
                list.Add(result);
            }

            return list;
        }
        public List<Object> GetTimesheet()
        {
            var staff = salaryDAO.GetTimesheet();
            var level1 = new List<string>();
            level1.Add("Staff");

            var list = new List<object>();
            foreach (var cus in staff)
            {
                var result = UtilityClass.ConvertDynamicObjectWithFullAttr(cus, level1);
                list.Add(result);
            }

            return list;
        }

        public List<Object> getAllBenefit()
        {
            var staffBenefit = salaryDAO.getAllBenefit();
             var list = new List<Object>();
             foreach (var cus in staffBenefit)
            {
                var result = UtilityClass.ConvertDynamicObjectWithFullAttr(cus);
                list.Add(result);
            }

            return list;
        } 

        
       

        //public Staff GetStaffByID(int staffId)
        //{
        //    return salaryDAO.GetStaffByID(staffId);
        //}

        public bool AddNewTax(string taxName, double taxRate, double taxIndex,double taxFrom, double taxTo)
        {
            return salaryDAO.AddNewTax(taxName, taxRate, taxIndex, taxFrom, taxTo);
        }

        //public bool addnewTimeSheet()
        //{
        //    return salaryDAO.addnewTimeSheet();
        //}
        public bool AddAssurance(string benefitName, double benefitRate)
        {
            return salaryDAO.AddAssurance(benefitName, benefitRate);
        }
        public bool AddImpress(int selectStaffImpress, string dateStaffImpress, double txtmoneyImpress)
        {
            return salaryDAO.AddImpress(selectStaffImpress, dateStaffImpress, txtmoneyImpress);
        }

        public bool AddReward(int selectStaffReward, string dateStaffReward, double txtmoneyReward, string txtDescription)
        {
            return salaryDAO.AddReward(selectStaffReward, dateStaffReward, txtmoneyReward, txtDescription);
        }
        public bool AddStaffTimesheet(int selectStaffTimesheet, string dateStaffTimesheet, string txtDetail, double txttimeLeave, bool  staffAllow)
        {
            return salaryDAO.AddStaffTimesheet(selectStaffTimesheet, dateStaffTimesheet, txtDetail, txttimeLeave, staffAllow);
        }
        public bool UpdateTimeSheet(int txtTimeSheetId, int txtEditName, string txtEditDate, string txtEditDetail, int txtEdittimeLeave, bool editstaffAllow)
        {
            return salaryDAO.UpdateTimeSheet(txtTimeSheetId, txtEditName, txtEditDate, txtEditDetail, txtEdittimeLeave, editstaffAllow);
        }
        //Check validate

        public bool IsOverHoursInDay(string date,int staffId,double hours)
        {
            return salaryDAO.IsOverHoursInDay(date, staffId, hours);
        }

        public bool CheckExistAssuranName(string txtAssuranName)
        {
            return salaryDAO.CheckExistAssuranName(txtAssuranName);
        }

        public bool IsCountedSalary(int staffId, string txtDateCountSalary)
        {
            return salaryDAO.IsCountedSalary(staffId, txtDateCountSalary);
        }

        public bool CheckExistTaxName(string txtTaxname)
        {
            return salaryDAO.CheckExistTaxName(txtTaxname);
        }
        public bool CheckExistBenefitName(string txtBenefitName)
        {
            return salaryDAO.CheckExistBenefitName(txtBenefitName);
        }

        public bool CheckDateImpress(string txtBenefitName)
        {
            return salaryDAO.CheckDateImpress(txtBenefitName);
        }

        public bool CheckDateEditSalary(string txtEditDay)
        {
            return salaryDAO.CheckDateEditSalary(txtEditDay);
        }
        public bool CheckDateIsSunday(string txtTimesheetDay)
        {
            return salaryDAO.CheckDateIsSunday(txtTimesheetDay);
        }
               

        public bool addNewBenefit(string assuranceName, double assuranceRate)
        {
            return salaryDAO.addNewBenefit(assuranceName, assuranceRate);
        }

        public bool UpdateTax(int txTaxId, string taxName, double taxRate, double taxIndex,double taxFrom,double taxTo)
        {
            return salaryDAO.UpdateTax(txTaxId, taxName, taxRate, taxIndex, taxFrom, taxTo);
        }
        public bool UpdateAssurance(int assuranceId, string assuranceName, double assuranceRate)
        {
            return salaryDAO.UpdateAssurance(assuranceId, assuranceName, assuranceRate);
        }

        public bool UpdateBenefit(int benefitId, string benefitName, double benefiteRate)
        {
            return salaryDAO.UpdateBenefit(benefitId, benefitName, benefiteRate);
        }
        public bool UpdateImpress(int txtImpressId, int txtEditName,  string txtEditDate, double txtEditMoney)
        {
            return salaryDAO.UpdateImpress(txtImpressId, txtEditName, txtEditDate, txtEditMoney);
        }
        public bool UpdateReward(int txtRewardId, int txtEditName, string txtEditDate, double txtEditMoney, string editDescription)
        {
            return salaryDAO.UpdateReward(txtRewardId, txtEditName, txtEditDate, txtEditMoney, editDescription);
        }
        
        
        public string DeleteTax(int txTaxId)
        {
            return salaryDAO.DeleteTax(txTaxId);
        }
           public bool deleteSalary(int salaryId)
        {
            return salaryDAO.deleteSalary(salaryId);
        }
        
        public string DeleteAssurance(int assuranceId)
        {
            return salaryDAO.DeleteAssurance(assuranceId);
        }

        public bool deleteImpress(int txtImpressId)
        {
            return salaryDAO.deleteImpress(txtImpressId);
        }
        public bool deleteReward(int txtRewardId)
        {
            return salaryDAO.deleteReward(txtRewardId);
        }

        public String DeleteBenefit(int benefitId)
        {
            return salaryDAO.DeleteBenefit(benefitId);
        }
        public bool deleteTimeSheet(int txtTimeSheetId)
        {
            return salaryDAO.deleteTimeSheet(txtTimeSheetId);
        }

        /// <summary>
        /// Get List Salary for export excel
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        public SalaryExcelEntity GetSalaryByMonthExcel(int month, int year)
        {
            return salaryDAO.GetSalaryByMonthExcel(month, year);
        }
    }
}