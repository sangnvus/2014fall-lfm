using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Ajax.Utilities;
using LFMS.Models.ConcerEntity;



namespace LFMS.Models.DAO
{
    public class SalaryDAO
    {
        LFMSEntities db;
        public SalaryDAO()
        {
            db = new LFMSEntities();
        }

        public List<Staff> GetAllStaff()
        {
            var staffList = db.Staffs.ToList();
            return staffList;
        }

        public List<MonthInYear> getMonthInYear()
        {
            List<MonthInYear> listResult = new List<MonthInYear>();
            var currentYear = DateTime.Now.Year;

            for (int i = 0; i <= 12; i++)
            {
                if (i == 0)
                {
                    MonthInYear monthInYear = new MonthInYear();
                    monthInYear.Month = "Tất cả";
                    monthInYear.Value = i;
                    listResult.Add(monthInYear);
                }
                if (i != 0)
                {
                    MonthInYear monthInYear = new MonthInYear();
                    monthInYear.Month = "Tháng " + i;
                    monthInYear.Value = i;
                    listResult.Add(monthInYear);
                }
            }
            return listResult;
        }

        public List<getYear> getYear()
        {
            List<getYear> listResult = new List<getYear>();
            var currentYear = DateTime.Now.Year;

            for (int i = currentYear; i >= 2000; i--)
            {
                getYear getYear = new getYear();
                getYear.Year = "Năm " + i;
                getYear.Value = i;
                listResult.Add(getYear);


            }
            return listResult;
        }

        public List<Salary> GetAllSalary()
        {

            List<Salary> listSalary = new List<Salary>();

            listSalary = (from salary in db.Salaries
                          where salary.Active == true
                              //&& salary.Date.Month == DateTime.Now.Month
                                && salary.Date.Year == DateTime.Now.Year
                          select salary).ToList();

            return listSalary;
        }

        //public List<Salary> getSalarybyMonthJson(int monthId)
        //{
        //    List<Salary> listSalary = new List<Salary>();
        //    if (monthId != 0)
        //    {
        //        listSalary = (from salary in db.Salaries
        //                      where salary.Active == true
        //                            && salary.Date.Month == monthId
        //                            && salary.Date.Year == DateTime.Now.Year
        //                      select salary).ToList();
        //    }
        //    else if (monthId == 0)
        //    {
        //        listSalary = (from salary in db.Salaries
        //                      where salary.Active == true
        //                       && salary.Date.Year == DateTime.Now.Year
        //                      select salary).ToList();
        //    }
        //    return listSalary;
        //}

        public List<Salary> getSalarybyMonthandYear(int monthId, int yearId)
        {
            List<Salary> listSalary = new List<Salary>();
            if (monthId != 0)
            {
                listSalary = (from salary in db.Salaries
                              where salary.Active == true
                                    && salary.Date.Month == monthId
                                     && salary.Date.Year == yearId
                              select salary).ToList();
            }
            else if (monthId == 0)
            {
                listSalary = (from salary in db.Salaries
                              where salary.Active == true
                                && salary.Date.Year == yearId
                              select salary).ToList();
            }
            return listSalary;
        }

        public List<Tax> GetAllTaxJson()
        {
            var Tax = db.Taxes.ToList();
            return Tax;
        }
        public List<Assurance> getAllAssuranceJson()
        {
            var staffAssuran = db.Assurances.ToList();
            return staffAssuran;
        }

        public List<Tax> GetAllTax()
        {
            var taxlist = db.Taxes.Where(c => c.Active == true).ToList();
            return taxlist;
        }
        public List<Benefit> GetSelectBenefit()
        {
            var benefit = db.Benefits.Where(c => c.Active == true).ToList();
            return benefit;
        }
        public List<Assurance> GetSelectAssurance()
        {
            var assu = db.Assurances.Where(c => c.Active == true).ToList();
            return assu;
        }

        /// <summary>
        /// Kiểm tra có tính lương hay chưa
        /// </summary>
        /// <param name="staffId"></param>
        /// <returns>True: đã tính lương - False: chưa tính lương</returns>
        public bool IsCountedSalary(int staffId, string txtDateCountSalary)
        {
            try
            {
                var dateValue = DateTime.ParseExact(txtDateCountSalary, "dd/MM/yyyy", null);
                var impressMonth = dateValue.Month;

                var isCounted = db.Salaries.Any(salary => salary.StaffId == staffId && impressMonth == salary.Date.Month);
                return isCounted;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Kiểm tra số giờ nghỉ trong ngày, nếu quá 8 tiếng thì báo message
        /// </summary>
        /// <param name="date"></param>
        /// <param name="staffId"></param>
        /// <param name="hours"></param>
        /// <returns>True: Quá 8 tiếng - False: chưa quá 8 tiếng</returns>
        public bool IsOverHoursInDay(string date, int staffId, double hours)
        {
            try
            {
                var dateValue = DateTime.ParseExact(date, "dd/MM/yyyy", null);
                var selectHours = from timeSheet in db.TimeSheets
                                  where timeSheet.StaffId == staffId
                                        && timeSheet.Date == dateValue
                                        && timeSheet.Active == true
                                  select timeSheet;

                double totalHoursAbsent = 0;
                foreach (var item in selectHours)
                {
                    totalHoursAbsent = totalHoursAbsent + Convert.ToInt32(item.Hours);
                }
                if (totalHoursAbsent + hours > 8)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public bool AddStaffSalary(int selectStaffId, string txtDateCountSalary, String[] staffBenefit, String[] staffAssurance)
        {
            try
            {
                //Total Salary table
                Salary staffSalary = new Salary();
                //Lấy staffId
                staffSalary.StaffId = selectStaffId;

                //Lấy lương cứng
                var selectBaseSalary = (from staff in db.Staffs
                                        from staffGroup in db.StaffGroups
                                        where staff.StaffId == selectStaffId
                                              && staff.Active == true
                                              && staff.StaffGroupId == staffGroup.StaffGroupId
                                        select staffGroup).FirstOrDefault();
                double baseSalary = Convert.ToDouble(selectBaseSalary.BaseSalary);
                staffSalary.BaseSalary = baseSalary;

                //Lấy só người giảm trừ gia cảnh
                var selectAppendantPeople = (from staff in db.Staffs
                                             where staff.StaffId == selectStaffId
                                                   && staff.Active == true
                                             select staff).FirstOrDefault();
                var appendantPeople = selectAppendantPeople.AppendantPeople;

                //Lấy ngày tính lương
                DateTime dateCountSalary = DateTime.ParseExact(txtDateCountSalary, "dd/MM/yyyy", null);
                staffSalary.Date = dateCountSalary;
                //Tháng tính lương -> dùng để tính ngày Absent trong tháng
                var monthCountSalary = dateCountSalary.Month;

                //Tính tổng số giờ nghỉ có phép trong năm => ngày nghỉ có phép trong năm
                var selectHoursAllowAbsentInYear = from timesheet in db.TimeSheets
                                                   where timesheet.StaffId == selectStaffId
                                                         && timesheet.Date.Year == DateTime.Now.Year
                                                         && timesheet.IsAllowed == true
                                                         && timesheet.Active == true
                                                   select timesheet;
                double totalHoursAllowAbsentInYear = 0;
                foreach (var item in selectHoursAllowAbsentInYear)
                {
                    totalHoursAllowAbsentInYear = totalHoursAllowAbsentInYear + item.Hours;
                }
                double totalDayAllowAbsentInYear = totalHoursAllowAbsentInYear / 8;


                //Tính số giờ nghỉ trong tháng => ngày nghỉ trong tháng
                var selectAbsentMonth = from timesheet in db.TimeSheets
                                        where timesheet.StaffId == selectStaffId
                                              && timesheet.Date.Month == monthCountSalary
                                              && timesheet.Active == true
                                        select timesheet;
                double totalHoursAbsentInMonth = 0;
                double totalHoursNotAllowAbsentInMonth = 0;
                foreach (var item in selectAbsentMonth)
                {
                    totalHoursAbsentInMonth = totalHoursAbsentInMonth + (item.Hours);
                    if (item.IsAllowed == false)
                    {
                        totalHoursNotAllowAbsentInMonth = totalHoursNotAllowAbsentInMonth + (item.Hours);
                    }
                }
                //tính tổng số ngày nghỉ có phép từ tháng 1 của năm đến tháng tính lương năm hiện tại để lấy ngày trừ lương
                var selectTimeAbsentToCurrent = from timecount in db.TimeSheets
                                                where timecount.StaffId == selectStaffId
                                                    && timecount.Date.Month >= 1
                                            && timecount.Date.Month <= monthCountSalary
                                            && timecount.IsAllowed == true
                                            && timecount.Date.Year == DateTime.Now.Year
                                                select timecount;
                double totalHoursAbsentToCurrentMonth = 0;
                foreach (var item in selectTimeAbsentToCurrent)
                {
                    totalHoursAbsentToCurrentMonth = totalHoursAbsentToCurrentMonth + (item.Hours);

                }
                double totalDayAbsentToCurrentMonth = totalHoursAbsentToCurrentMonth / 8;
                //tính tổng số ngày nghỉ đến tháng trước tháng tính lương
                //lây tháng tháng trước đó
                var monthpre = monthCountSalary - 1;
                var selectTimeAbsentToMothlast = from timecountpremonth in db.TimeSheets
                                                 where timecountpremonth.StaffId == selectStaffId
                                            && timecountpremonth.Date.Month >= 1
                                            && timecountpremonth.Date.Month <= monthpre
                                            && timecountpremonth.IsAllowed == true
                                            && timecountpremonth.Date.Year == DateTime.Now.Year
                                                 select timecountpremonth;
                double totalHoursAbsentToPreCurrentMonth = 0;
                foreach (var item in selectTimeAbsentToMothlast)
                {
                    totalHoursAbsentToPreCurrentMonth = totalHoursAbsentToPreCurrentMonth + (item.Hours);

                }
                double totalDayAbsentToPreCurrentMonth = totalHoursAbsentToPreCurrentMonth / 8;
                //Lấy số ngày bị trừ lương nếu nghỉ có phép > 12 ngày
                double totalDayDuraion = 0;

                if (totalDayAllowAbsentInYear < 12)
                {
                    totalDayDuraion = 0;
                    totalDayAbsentToPreCurrentMonth = 12;
                }
                else
                {
                    totalDayDuraion = ((totalDayAbsentToCurrentMonth - 12) - (totalDayAbsentToPreCurrentMonth - 12)) * 8;

                }


                double totalAbsentDayInMonth = totalHoursAbsentInMonth / 8;
                double totalNotAllowAbsentDayInMonth = totalHoursNotAllowAbsentInMonth / 8;
                staffSalary.TotalAbsent = totalAbsentDayInMonth;

                //Tính số ngày đi làm trong tháng
                var currentMonth = monthCountSalary;
                var currentYear = DateTime.Now.Year;
                var totalDay = System.DateTime.DaysInMonth(currentYear, currentMonth);
                List<DateTime> listDayPresent = new List<DateTime>();
                for (int i = 1; i < totalDay; i++)
                {
                    DateTime date = new DateTime(currentYear, currentMonth, i);
                    if (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday)
                    {
                        listDayPresent.Add(date);
                    }
                }
                var totalPresent = listDayPresent.Count - totalAbsentDayInMonth;
                staffSalary.TotalPresent = totalPresent;

                //Tính tổng số trợ cấp
                double totalBenefit = 0;
                List<Benefit> listStaffBenefit = new List<Benefit>();
                if (IsEmptyBenefit(staffBenefit) != true)
                {
                    for (int i = 0; i < staffBenefit.Length; i++)
                    {
                        var benefitId = Convert.ToInt32(staffBenefit[i]);
                        var selectBenefit = (from benefit in db.Benefits
                                             where benefit.BenefitID == benefitId
                                                   && benefit.Active == true
                                             select benefit).FirstOrDefault();
                        totalBenefit = totalBenefit + selectBenefit.Rate;
                        listStaffBenefit.Add(selectBenefit);
                    }
                }
                staffSalary.TotalBenefit = Math.Round(totalBenefit, 0);



                //Số tiền thưởng
                double totalReward = 0;
                var selectReward = from reward in db.Rewards
                                   where reward.StaffId == selectStaffId
                                         && reward.Date.Month == monthCountSalary
                                   select reward;
                foreach (var reward in selectReward)
                {
                    totalReward = totalReward + reward.Money;
                }
                staffSalary.TotalReward = totalReward;

                //Tổng lương sau khi cộng Trợ cấp, trừ tiền ngày nghỉ nếu như tổng số ngày nghỉ trong năm > 12 ngày
                //Tính lương sau khi cộng trợ cấp
                var totalSalary = baseSalary + totalBenefit + totalReward;

                //Lương theo ngày
                double salaryInDay = Math.Round(totalSalary / listDayPresent.Count, 0);
                //Lương theo giờ
                double salaryInHour = Math.Round(salaryInDay / 8, 0);


                //nếu như nghỉ không phép trong tháng thì trừ lương
                double totalDateDE = 0;
                double totakMoneyDE = 0;
                if (totalNotAllowAbsentDayInMonth != 0)
                {
                    totalSalary = totalSalary - (totalHoursNotAllowAbsentInMonth * salaryInHour);
                    if (totalDayAllowAbsentInYear > 12)
                    {
                        totalSalary = totalSalary - (totalDayDuraion * salaryInHour);
                        totakMoneyDE = totalDayDuraion * salaryInHour;
                        totalDateDE = totalDayDuraion / 8;
                    }
                    totakMoneyDE = totakMoneyDE + totalHoursNotAllowAbsentInMonth * salaryInHour;

                    totalDateDE = totalDateDE + totalHoursNotAllowAbsentInMonth / 8;
                    //lưu ngày bị trừ lương vào db
                    //Lưu tháng vào db
                    staffSalary.TotalMoneyDeduction = totakMoneyDE;
                    staffSalary.TotalDateDeduction = totalDateDE;
                }
                else if (totalNotAllowAbsentDayInMonth == 0)
                {
                    if (totalDayAllowAbsentInYear > 12)
                    {
                        //Lương trừ theo số giờ nghỉ trong tháng
                        totalSalary = totalSalary - (totalDayDuraion * salaryInHour);
                        totakMoneyDE = totalDayDuraion * salaryInHour;
                        //lưu ngày bị trừ lương vào db
                        totalDateDE = totalDayDuraion / 8;
                    }

                    staffSalary.TotalMoneyDeduction = totakMoneyDE;
                    staffSalary.TotalDateDeduction = totalDateDE;
                }
                //Tính tổng bảo hiểm
                List<Assurance> listAssuran = new List<Assurance>();
                double assuranceMoney = 0;
                if (IsEmptyBenefit(staffAssurance) != true)
                {
                    for (int i = 0; i < staffAssurance.Length; i++)
                    {
                        var assId = Convert.ToInt32(staffAssurance[i]);
                        var selectAss = (from ass in db.Assurances
                                         where ass.AssuranceID == assId
                                               && ass.Active == true
                                         select ass).FirstOrDefault();
                        assuranceMoney = assuranceMoney + ((totalSalary * selectAss.Rate) / 100);
                        listAssuran.Add(selectAss);
                    }
                }
                staffSalary.TotalAssurance = Math.Round(assuranceMoney, 0);


                //Tổng tiền giảm trừ người phụ thuộc
                var appendantPeopleMoney = appendantPeople * 3600000;
                //Tổng tiền giảm trừ bản thân
                var selfMoneyNotTax = 9000000;
                //Tính tổng số tiền không phải chịu thuế
                var moneyWithoutTax = appendantPeopleMoney + selfMoneyNotTax + assuranceMoney;

                //Số tiền phải chịu thuế
                var payTaxMoney = totalSalary - moneyWithoutTax;
                double taxOfMoney = 0;
                Tax taxOfStaff = new Tax();
                if (payTaxMoney > 0)
                {
                    var selectRateOfTax = (from tax in db.Taxes
                                           where tax.MoneyFrom <= payTaxMoney
                                                 && tax.MoneyTo >= payTaxMoney
                                                 && tax.Active == true
                                           select tax).FirstOrDefault();
                    if (selectRateOfTax != null)
                    {
                        var rateOfTax = selectRateOfTax.Rate;
                        var indexOfTax = selectRateOfTax.IndexTax;
                        taxOfMoney = ((Convert.ToDouble(payTaxMoney) * rateOfTax) / 100) - indexOfTax;

                        //Lưu thuế thu nhập cá nhân phải đóng lại để lưu xuống SalaryTaxDetail
                        taxOfStaff = selectRateOfTax;
                    }


                }
                staffSalary.TotalTax = Math.Round(taxOfMoney, 0);


                staffSalary.Active = true;
                //Tính tổng tạm ứng
                double totalImpress = 0;
                var selectImpress = from impress in db.Impresses
                                    where impress.StaffId == selectStaffId
                                          && impress.Date.Month == monthCountSalary
                                    select impress;
                foreach (var impress in selectImpress)
                {
                    totalImpress = totalImpress + impress.Money;
                }
                staffSalary.TotalImprest = totalImpress;

                //Số tiền thực nhận
                var actualSalary = totalSalary - taxOfMoney - assuranceMoney - totalImpress;
                //staffSalary.ActualSalary = actualSalary;
                staffSalary.ActualSalary = Math.Round(actualSalary, 0);
                ////Math.Round(actualSalary, 2);

                //Lưu vô bảng Salary
                db.Salaries.Add(staffSalary);

                //Lưu vô bảng TaxDetail thuế thu nhập các nhân
                if (taxOfStaff.TaxID != 0)
                {
                    SalaryTaxDetail newTax = new SalaryTaxDetail();
                    newTax.SalaryID = staffSalary.SalaryID;
                    newTax.TaxID = taxOfStaff.TaxID;
                    newTax.Description = taxOfStaff.Name;
                    newTax.Active = true;
                    db.SalaryTaxDetails.Add(newTax);
                }


                // Lưu vô bảng SalaryAssuranceDetail, các bảo hiểm phải nộp
                foreach (var assurance in listAssuran)
                {
                    SalaryAssuranceDetail newAssDetail = new SalaryAssuranceDetail();
                    newAssDetail.SalaryID = staffSalary.SalaryID;
                    newAssDetail.AssuranceID = assurance.AssuranceID;
                    newAssDetail.Description = assurance.Name;
                    newAssDetail.Active = true;
                    db.SalaryAssuranceDetails.Add(newAssDetail);
                }

                //Lưu vô bảng SalaryBenefitDetail, các trợ cấp được nhận
                if (listStaffBenefit.Count > 0)
                {
                    foreach (var benefit in listStaffBenefit)
                    {
                        SalaryBenefitDetail newBenefitDetail = new SalaryBenefitDetail();
                        newBenefitDetail.SalaryID = staffSalary.SalaryID;
                        newBenefitDetail.BenefitID = benefit.BenefitID;
                        newBenefitDetail.Description = benefit.Name;
                        newBenefitDetail.Active = true;
                        db.SalaryBenefitDetails.Add(newBenefitDetail);
                    }
                }
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public bool UpdateStaffSalary(int salaryId, int selectStaffId, string txtDateCountSalary, String[] cboOffice, String[] assList)
        {
            try
            {
                //update bảng Staff
                Salary staffSalary = GetSalaryEditByID(salaryId);
                if (staffSalary == null)
                {
                    return false;
                }
                else
                {
                    {
                        //Total Salary table
                        //Salary staffSalary = new Salary();
                        //Lấy staffId
                        staffSalary.SalaryID = salaryId;

                        //Lấy lương cứng
                        var selectBaseSalary = (from staff in db.Staffs
                                                from staffGroup in db.StaffGroups
                                                where staff.StaffId == selectStaffId
                                                      && staff.Active == true
                                                      && staff.StaffGroupId == staffGroup.StaffGroupId
                                                select staffGroup).FirstOrDefault();
                        double baseSalary = Convert.ToDouble(selectBaseSalary.BaseSalary);
                        staffSalary.BaseSalary = baseSalary;

                        //Lấy só người giảm trừ gia cảnh
                        var selectAppendantPeople = (from staff in db.Staffs
                                                     where staff.StaffId == selectStaffId
                                                           && staff.Active == true
                                                     select staff).FirstOrDefault();
                        var appendantPeople = selectAppendantPeople.AppendantPeople;

                        //Lấy ngày tính lương
                        DateTime dateCountSalary = DateTime.ParseExact(txtDateCountSalary, "dd/MM/yyyy", null);
                        staffSalary.Date = dateCountSalary;
                        //Tháng tính lương -> dùng để tính ngày Absent trong tháng
                        var monthCountSalary = dateCountSalary.Month;

                        //Tính tổng số giờ nghỉ có phép trong năm => ngày nghỉ có phép trong năm
                        var selectHoursAllowAbsentInYear = from timesheet in db.TimeSheets
                                                           where timesheet.StaffId == selectStaffId
                                                                 && timesheet.Date.Year == DateTime.Now.Year
                                                                 && timesheet.IsAllowed == true
                                                                 && timesheet.Active == true
                                                           select timesheet;
                        double totalHoursAllowAbsentInYear = 0;
                        foreach (var item in selectHoursAllowAbsentInYear)
                        {
                            totalHoursAllowAbsentInYear = totalHoursAllowAbsentInYear + (item.Hours);
                        }
                        double totalDayAllowAbsentInYear = totalHoursAllowAbsentInYear / 8;


                        //Tính số giờ nghỉ trong tháng => ngày nghỉ trong tháng
                        var selectAbsentMonth = from timesheet in db.TimeSheets
                                                where timesheet.StaffId == selectStaffId
                                                      && timesheet.Date.Month == monthCountSalary
                                                       && timesheet.Date.Year == DateTime.Now.Year
                                                      && timesheet.Active == true
                                                select timesheet;
                        double totalHoursAbsentInMonth = 0;
                        double totalHoursNotAllowAbsentInMonth = 0;
                        foreach (var item in selectAbsentMonth)
                        {
                            totalHoursAbsentInMonth = totalHoursAbsentInMonth + (item.Hours);
                            if (item.IsAllowed == false)
                            {
                                totalHoursNotAllowAbsentInMonth = totalHoursNotAllowAbsentInMonth + (item.Hours);
                            }
                        }

                        //tính tổng số ngày nghỉ có phép từ tháng 1 của năm đến tháng tính lương năm hiện tại để lấy ngày trừ lương
                        var selectTimeAbsentToCurrent = from timecount in db.TimeSheets
                                                        where timecount.StaffId == selectStaffId
                                                       && timecount.Date.Month >= 1
                                                    && timecount.Date.Month <= monthCountSalary
                                                    && timecount.IsAllowed == true
                                                    && timecount.Date.Year == DateTime.Now.Year
                                                        select timecount;
                        double totalHoursAbsentToCurrentMonth = 0;
                        foreach (var item in selectTimeAbsentToCurrent)
                        {
                            totalHoursAbsentToCurrentMonth = totalHoursAbsentToCurrentMonth + (item.Hours);

                        }
                        double totalDayAbsentToCurrentMonth = totalHoursAbsentToCurrentMonth / 8;
                        //tính tổng số ngày nghỉ đến tháng trước tháng tính lương
                        //lây tháng tháng trước đó
                        var monthpre = monthCountSalary - 1;
                        var selectTimeAbsentToMothlast = from timecountpremonth in db.TimeSheets
                                                         where timecountpremonth.StaffId == selectStaffId
                                                             && timecountpremonth.Date.Month >= 1
                                                    && timecountpremonth.Date.Month <= monthpre
                                                    && timecountpremonth.IsAllowed == true
                                                    && timecountpremonth.Date.Year == DateTime.Now.Year
                                                         select timecountpremonth;
                        double totalHoursAbsentToPreCurrentMonth = 0;
                        foreach (var item in selectTimeAbsentToMothlast)
                        {
                            totalHoursAbsentToPreCurrentMonth = totalHoursAbsentToPreCurrentMonth + (item.Hours);

                        }
                        double totalDayAbsentToPreCurrentMonth = totalHoursAbsentToPreCurrentMonth / 8;
                        //Lấy số ngày bị trừ lương nếu nghỉ có phép > 12 ngày
                        double totalDayDuraion = 0;
                        //var totalDayDuraionSalary = 0;
                        //if (totalHoursAbsentToPreCurrentMonth < 12)
                        //{
                        //    totalHoursAbsentToPreCurrentMonth = 12;
                        //}
                        if (totalDayAllowAbsentInYear < 12)
                        {
                            totalDayDuraion = 0;
                            totalDayAbsentToPreCurrentMonth = 12;
                        }
                        else
                        {
                            totalDayDuraion = ((totalDayAbsentToCurrentMonth - 12) - (totalDayAbsentToPreCurrentMonth - 12)) * 8;

                        }

                        double totalAbsentDayInMonth = totalHoursAbsentInMonth / 8;
                        double totalNotAllowAbsentDayInMonth = totalHoursNotAllowAbsentInMonth / 8;
                        staffSalary.TotalAbsent = totalAbsentDayInMonth;

                        //Tính số ngày đi làm trong tháng
                        var currentMonth = monthCountSalary;
                        var currentYear = DateTime.Now.Year;
                        var totalDay = System.DateTime.DaysInMonth(currentYear, currentMonth);
                        List<DateTime> listDayPresent = new List<DateTime>();
                        for (int i = 1; i < totalDay; i++)
                        {
                            DateTime date = new DateTime(currentYear, currentMonth, i);
                            if (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday)
                            {
                                listDayPresent.Add(date);
                            }
                        }
                        var totalPresent = listDayPresent.Count - totalAbsentDayInMonth;
                        staffSalary.TotalPresent = totalPresent;

                        //Tính tổng số trợ cấp
                        double totalBenefit = 0;
                        List<Benefit> listStaffBenefit = new List<Benefit>();
                        if (IsEmptyBenefit(cboOffice) != true)
                        {
                            for (int i = 0; i < cboOffice.Length; i++)
                            {
                                var benefitId = Convert.ToInt32(cboOffice[i]);
                                var selectBenefit = (from benefit in db.Benefits
                                                     where benefit.BenefitID == benefitId
                                                           && benefit.Active == true
                                                     select benefit).FirstOrDefault();
                                totalBenefit = totalBenefit + selectBenefit.Rate;
                                listStaffBenefit.Add(selectBenefit);
                            }
                        }
                        staffSalary.TotalBenefit = Math.Round(totalBenefit, 0);

                        //Tính tổng tạm ứng
                        double totalImpress = 0;
                        var selectImpress = from impress in db.Impresses
                                            where impress.StaffId == selectStaffId
                                                  && impress.Date.Month == monthCountSalary
                                            select impress;
                        foreach (var impress in selectImpress)
                        {
                            totalImpress = totalImpress + impress.Money;
                        }
                        staffSalary.TotalImprest = totalImpress;

                        //Tổng lương sau khi cộng Trợ cấp, trừ tiền ngày nghỉ nếu như tổng số ngày nghỉ trong năm > 12 ngày
                        //Số tiền thưởng
                        double totalReward = 0;
                        var selectReward = from reward in db.Rewards
                                           where reward.StaffId == selectStaffId
                                                 && reward.Date.Month == monthCountSalary
                                           select reward;
                        foreach (var reward in selectReward)
                        {
                            totalReward = totalReward + reward.Money;
                        }
                        staffSalary.TotalReward = totalReward;
                        //Tính lương sau khi cộng trợ cấp
                        var totalSalary = baseSalary + totalBenefit + totalReward;

                        //Lương theo ngày
                        var salaryInDay = Math.Round(totalSalary / listDayPresent.Count, 0);
                        //Lương theo giờ
                        var salaryInHour = Math.Round(salaryInDay / 8, 0);

                        //Nếu như tổng số ngày nghỉ phép trong năm > 12 ngày, trừ lương các ngày nghỉ trong tháng cho dù có phép hay ko phép
                        //if (totalDayAllowAbsentInYear > 12)
                        //{
                        //    //Lương trừ theo số giờ nghỉ trong tháng
                        //    totalSalary = totalSalary - (totalDayDuraion * salaryInHour);
                        //    staffSalary.TotalMoneyDeduction = totalDayDuraion * salaryInHour;
                        //    //lưu ngày bị trừ lương vào db
                        //    staffSalary.TotalDateDeduction = totalDayDuraion / 8;
                        //}
                        //nếu như nghỉ không phép trong tháng thì trừ lương
                        double totalDateDE = 0;
                        double totakMoneyDE = 0;
                        if (totalNotAllowAbsentDayInMonth != 0)
                        {
                            totalSalary = totalSalary - (totalHoursNotAllowAbsentInMonth * salaryInHour);
                            if (totalDayAllowAbsentInYear > 12)
                            {
                                //Lương trừ theo số giờ nghỉ trong tháng
                                totalSalary = totalSalary - (totalDayDuraion * salaryInHour);
                                totakMoneyDE = totalDayDuraion * salaryInHour;
                                //lưu ngày bị trừ lương vào db
                                totalDateDE = totalDayDuraion / 8;
                            }
                            totakMoneyDE = totakMoneyDE + totalHoursNotAllowAbsentInMonth * salaryInHour;
                            //lưu ngày bị trừ lương vào db
                            totalDateDE = totalDateDE + totalHoursNotAllowAbsentInMonth / 8;
                            staffSalary.TotalMoneyDeduction = totakMoneyDE;
                            staffSalary.TotalDateDeduction = totalDateDE;
                        }
                        else if (totalNotAllowAbsentDayInMonth == 0)
                        {
                            if (totalDayAllowAbsentInYear > 12)
                            {
                                //Lương trừ theo số giờ nghỉ trong tháng
                                totalSalary = totalSalary - (totalDayDuraion * salaryInHour);
                                totakMoneyDE = totalDayDuraion * salaryInHour;
                                //lưu ngày bị trừ lương vào db
                                totalDateDE = totalDayDuraion / 8;
                            }

                            staffSalary.TotalMoneyDeduction = totakMoneyDE;
                            staffSalary.TotalDateDeduction = totalDateDE;
                        }
                        //Tính tổng bảo hiểm
                        List<Assurance> listAssuran = new List<Assurance>();
                        double assuranceMoney = 0;
                        if (IsEmptyBenefit(assList) != true)
                        {
                            for (int i = 0; i < assList.Length; i++)
                            {
                                var assId = Convert.ToInt32(assList[i]);
                                var selectAss = (from ass in db.Assurances
                                                 where ass.AssuranceID == assId
                                                       && ass.Active == true
                                                 select ass).FirstOrDefault();
                                assuranceMoney = assuranceMoney + ((totalSalary * selectAss.Rate) / 100);
                                listAssuran.Add(selectAss);
                            }
                        }
                        staffSalary.TotalAssurance = Math.Round(assuranceMoney, 0);


                        //Tổng tiền giảm trừ người phụ thuộc
                        var appendantPeopleMoney = appendantPeople * 3600000;
                        //Tổng tiền giảm trừ bản thân
                        var selfMoneyNotTax = 9000000;
                        //Tính tổng số tiền không phải chịu thuế
                        var moneyWithoutTax = appendantPeopleMoney + selfMoneyNotTax + assuranceMoney;

                        //Số tiền phải chịu thuế
                        var payTaxMoney = totalSalary - moneyWithoutTax;
                        double taxOfMoney = 0;
                        Tax taxOfStaff = new Tax();
                        if (payTaxMoney > 0)
                        {
                            var selectRateOfTax = (from tax in db.Taxes
                                                   where tax.MoneyFrom <= payTaxMoney
                                                         && tax.MoneyTo >= payTaxMoney
                                                         && tax.Active == true
                                                   select tax).FirstOrDefault();
                            if (selectRateOfTax != null)
                            {
                                var rateOfTax = selectRateOfTax.Rate;
                                var indexOfTax = selectRateOfTax.IndexTax;
                                taxOfMoney = ((Convert.ToDouble(payTaxMoney) * rateOfTax) / 100) - indexOfTax;

                                //Lưu thuế thu nhập cá nhân phải đóng lại để lưu xuống SalaryTaxDetail
                                taxOfStaff = selectRateOfTax;
                                //Math.Round(taxOfMoney, 0);
                            }


                        }
                        staffSalary.TotalTax = Math.Round(taxOfMoney, 0);


                        staffSalary.Active = true;

                        //Số tiền thực nhận
                        //var actualSalary = totalSalary - taxOfMoney - assuranceMoney + totalReward;
                        var actualSalary = totalSalary - taxOfMoney - assuranceMoney - totalImpress;
                        //staffSalary.ActualSalary = actualSalary;
                        staffSalary.ActualSalary = Math.Round(actualSalary, 0);
                        ////Math.Round(actualSalary, 2);

                        //Lưu vô bảng Salary
                        //db.Salaries.Add(staffSalary);

                        //Lưu vô bảng TaxDetail thuế thu nhập các nhân
                        //remove thuế cũ của SalaryID đi và thêm vô cái mới
                        List<SalaryTaxDetail> salarytaxdetail = GetTaxBySalaryID(salaryId);
                        foreach (var offremove in salarytaxdetail)
                        {
                            db.SalaryTaxDetails.Remove(offremove);
                        }

                        if (taxOfStaff.TaxID != 0)
                        {
                            SalaryTaxDetail newTax = new SalaryTaxDetail();
                            newTax.SalaryID = staffSalary.SalaryID;
                            newTax.TaxID = taxOfStaff.TaxID;
                            newTax.Description = taxOfStaff.Name;
                            newTax.Active = true;
                            db.SalaryTaxDetails.Add(newTax);
                        }


                        // Lưu vô bảng SalaryAssuranceDetail, các bảo hiểm phải nộp

                        //remove  SalaryAssuranceDetail của SalaryID đi và thêm vô cái mới
                        List<SalaryAssuranceDetail> salaryAssurancedetail = GetAssuranceBySalaryID(salaryId);
                        foreach (var offremove in salaryAssurancedetail)
                        {
                            db.SalaryAssuranceDetails.Remove(offremove);
                        }

                        foreach (var assurance in listAssuran)
                        {
                            SalaryAssuranceDetail newAssDetail = new SalaryAssuranceDetail();
                            newAssDetail.SalaryID = staffSalary.SalaryID;
                            newAssDetail.AssuranceID = assurance.AssuranceID;
                            newAssDetail.Description = assurance.Name;
                            newAssDetail.Active = true;
                            db.SalaryAssuranceDetails.Add(newAssDetail);
                        }

                        //Lưu vô bảng SalaryBenefitDetail, các trợ cấp được nhận
                        //remove  SalaryBenefitDetail của SalaryID đi và thêm vô cái mới
                        List<SalaryBenefitDetail> salaryBenefitdetail = GetBenefitBySalaryID(salaryId);
                        foreach (var offremove in salaryBenefitdetail)
                        {
                            db.SalaryBenefitDetails.Remove(offremove);
                        }
                        if (listStaffBenefit.Count > 0)
                        {
                            foreach (var benefit in listStaffBenefit)
                            {
                                SalaryBenefitDetail newBenefitDetail = new SalaryBenefitDetail();
                                newBenefitDetail.SalaryID = staffSalary.SalaryID;
                                newBenefitDetail.BenefitID = benefit.BenefitID;
                                newBenefitDetail.Description = benefit.Name;
                                newBenefitDetail.Active = true;
                                db.SalaryBenefitDetails.Add(newBenefitDetail);
                            }
                        }
                        db.SaveChanges();
                        return true;
                    }

                }


            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }

        }

        public bool IsEmptyBenefit(String[] listBenefit)
        {
            bool result = true;
            foreach (var item in listBenefit)
            {
                if (!string.Empty.Equals(item.Trim()))
                    return false;
                break;
            }
            return result;

        }


        //public List<TimeSheetConcertEntity> getStaffTimesheetId(int id)
        //{
        //    List<TimeSheetConcertEntity> listTimeSheet = new List<TimeSheetConcertEntity>();
        //    var selectTimeSheet = from timesheet in db.TimeSheets
        //                          from staff in db.Staffs
        //                          where timesheet.StaffId == id
        //                                && staff.StaffId == timesheet.StaffId
        //                          select new
        //                          {
        //                              TimeSheet = timesheet,
        //                              Staff = staff
        //                          };
        //    foreach (var item in selectTimeSheet)
        //    {
        //        TimeSheetConcertEntity timeSheet = new TimeSheetConcertEntity();
        //        timeSheet.ID = item.TimeSheet.ID;
        //        timeSheet.StaffId = item.TimeSheet.StaffId;
        //        timeSheet.Date = item.TimeSheet.Date;
        //        //timeSheet.IsAbsent = item.TimeSheet.IsAbsent;
        //        //timeSheet.IsLeave = item.TimeSheet.IsLeave;
        //        timeSheet.Active = item.TimeSheet.Active;
        //        timeSheet.StaffName = item.Staff.StaffName;
        //        listTimeSheet.Add(timeSheet);
        //    }
        //    return listTimeSheet;
        //}

        //public List<ImpressConcertEntity> getAllImpress()
        //{
        //    var selectResult = from impress in db.Impresses
        //                       from staff in db.Staffs
        //                       where impress.StaffId == staff.StaffId
        //                             && staff.Active == true
        //                       select new
        //                       {
        //                           Staff = staff,
        //                           Impress = impress
        //                       };
        //    List<ImpressConcertEntity> listImpress = new List<ImpressConcertEntity>();
        //    foreach (var item in selectResult)
        //    {
        //        ImpressConcertEntity impressEnt = new ImpressConcertEntity();
        //        impressEnt.ID = item.Impress.ID;
        //        impressEnt.StaffId = item.Impress.StaffId;
        //        impressEnt.Date = item.Impress.Date;
        //        impressEnt.Money = item.Impress.Money;
        //        impressEnt.UserName = item.Staff.StaffName;
        //        listImpress.Add(impressEnt);
        //    }
        //    return listImpress;
        //}
        public List<Impress> getAllImpress()
        {
            var staff = db.Impresses.ToList();
            return staff;
        }
        public List<Impress> getImpressByStaffId(int staffId, int monthImpressId, int yearImpressId)
        {
            List<Impress> listImpress = new List<Impress>();
            if (staffId == 0 && monthImpressId == 0)
            {
                listImpress = (from reward in db.Impresses
                               where reward.Date.Year == yearImpressId
                               select reward).ToList();
            }
            else if (staffId == 0 && monthImpressId != 0)
            {
                listImpress = (from reward in db.Impresses
                               where reward.Date.Year == yearImpressId
                                  && reward.Date.Month == monthImpressId
                               select reward).ToList();
            }
            else if (staffId != 0 && monthImpressId != 0)
            {
                listImpress = (from reward in db.Impresses
                               where reward.StaffId == staffId
                               && reward.Date.Month == monthImpressId
                               && reward.Date.Year == yearImpressId
                               select reward).ToList();
            }
            else if (staffId != 0 && monthImpressId == 0)
            {
                listImpress = (from reward in db.Impresses
                               where reward.StaffId == staffId
                               && reward.Date.Year == yearImpressId
                               select reward).ToList();
            }
            return listImpress;
        }
        public List<Reward> getAllReward()
        {
            var reward = db.Rewards.ToList();
            return reward;
        }
        public List<Reward> getRewardbyStaffId(int staffId, int mothRewwardId, int yearRewardId)
        {
            List<Reward> listReward = new List<Reward>();
            if (staffId == 0 && mothRewwardId == 0)
            {
                listReward = (from reward in db.Rewards
                              where reward.Date.Year == yearRewardId
                              select reward).ToList();
            }
            else if (staffId == 0 && mothRewwardId != 0)
            {
                listReward = (from reward in db.Rewards
                              where reward.Date.Year == yearRewardId
                                 && reward.Date.Month == mothRewwardId
                              select reward).ToList();
            }
            else if (staffId != 0 && mothRewwardId != 0)
            {
                listReward = (from reward in db.Rewards
                              where reward.StaffId == staffId
                              && reward.Date.Month == mothRewwardId
                              && reward.Date.Year == yearRewardId
                              select reward).ToList();
            }
            else if (staffId != 0 && mothRewwardId == 0)
            {
                listReward = (from reward in db.Rewards
                              where reward.StaffId == staffId
                              && reward.Date.Year == yearRewardId
                              select reward).ToList();
            }
            return listReward;
        }

        public List<TimeSheet> GetTimesheet()
        {
            var staff = db.TimeSheets.ToList();
            return staff;
        }

        public List<TimeSheet> GetTimeSheetByStaffId(int staffId, int monthTimsheetId, int yearTimsheetId)
        {
            List<TimeSheet> listTimesheet = new List<TimeSheet>();
            if (staffId == 0 && monthTimsheetId == 0)
            {
                listTimesheet = (from reward in db.TimeSheets
                                 where reward.Date.Year == yearTimsheetId
                                 select reward).ToList();
            }
            else if (staffId == 0 && monthTimsheetId != 0)
            {
                listTimesheet = (from reward in db.TimeSheets
                                 where reward.Date.Year == yearTimsheetId
                                    && reward.Date.Month == monthTimsheetId
                                 select reward).ToList();
            }
            else if (staffId != 0 && monthTimsheetId != 0)
            {
                listTimesheet = (from reward in db.TimeSheets
                                 where reward.StaffId == staffId
                                 && reward.Date.Month == monthTimsheetId
                                 && reward.Date.Year == yearTimsheetId
                                 select reward).ToList();
            }
            else if (staffId != 0 && monthTimsheetId == 0)
            {
                listTimesheet = (from reward in db.TimeSheets
                                 where reward.StaffId == staffId
                                 && reward.Date.Year == yearTimsheetId
                                 select reward).ToList();
            }
            return listTimesheet;
        }

        public List<SalaryTaxDetail> GetTaxBySalaryID(int salaryId)
        {
            var listTax = db.SalaryTaxDetails.Where(c => c.SalaryID == salaryId).ToList();
            return listTax;
        }

        public List<SalaryBenefitDetail> GetBenefitBySalaryID(int salaryId)
        {
            var listbenefit = db.SalaryBenefitDetails.Where(c => c.SalaryID == salaryId).ToList();
            return listbenefit;
        }

        public List<SalaryAssuranceDetail> GetAssuranceBySalaryID(int salaryId)
        {
            var listAsurance = db.SalaryAssuranceDetails.Where(c => c.SalaryID == salaryId).ToList();
            return listAsurance;
        }


        public List<Benefit> getAllBenefit()
        {
            var staffBenefit = db.Benefits.ToList();
            return staffBenefit;
        }


        //public Staff GetStaffByID(int staffId)
        //{
        //    var staff = db.Staffs.Where(c => c.StaffId == staffId).FirstOrDefault();
        //    return staff;
        //}

        public Salary GetSalaryEditByID(int salaryId)
        {
            var salary = db.Salaries.Where(c => c.SalaryID == salaryId).FirstOrDefault();
            return salary;
        }

        public Tax GetTaxById(int taxId)
        {
            var tax = db.Taxes.Where(c => c.TaxID == taxId).FirstOrDefault();
            return tax;
        }

        public Assurance GetAssuranceById(int assuranceId)
        {
            var assurance = db.Assurances.Where(c => c.AssuranceID == assuranceId).FirstOrDefault();
            return assurance;
        }

        public Benefit GetBenefitById(int benefitId)
        {
            var benefit = db.Benefits.Where(c => c.BenefitID == benefitId).FirstOrDefault();
            return benefit;
        }

        public Impress GetImpressById(int impressId)
        {
            var impress = db.Impresses.Where(c => c.ID == impressId).FirstOrDefault();
            return impress;
        }

        public Reward GetRewardById(int rewardId)
        {
            var reward = db.Rewards.Where(c => c.RewardID == rewardId).FirstOrDefault();
            return reward;
        }
        public TimeSheet GetTimeSheetUpdateById(int timesheetId)
        {
            var timesheet = db.TimeSheets.Where(c => c.ID == timesheetId).FirstOrDefault();
            return timesheet;
        }

        public Role GetStaffRoleById(int staffGroupId)
        {
            var staffgroup = db.Roles.Where(c => c.RoleId == staffGroupId).FirstOrDefault();
            return staffgroup;
        }

        public bool AddNewTax(string taxName, double taxRate, double taxIndex, double taxFrom, double taxTo)
        {
            Tax tax = new Tax();
            tax.Name = taxName;
            tax.Rate = taxRate;
            tax.IndexTax = taxIndex;
            tax.MoneyFrom = taxFrom;
            tax.MoneyTo = taxTo;
            tax.Active = true;

            try
            {
                db.Taxes.Add(tax);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }


        public bool AddAssurance(string assuranceName, double assuranceRate)
        {
            Assurance assurance = new Assurance();
            assurance.Name = assuranceName;
            assurance.Rate = assuranceRate;
            assurance.Active = true;

            try
            {
                db.Assurances.Add(assurance);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        public bool AddImpress(int selectStaffImpress, string dateStaffImpress, double txtmoneyImpress)
        {
            Impress impress = new Impress();

            impress.StaffId = selectStaffImpress;

            DateTime dateStaffImpressPR = DateTime.ParseExact(dateStaffImpress, "dd/MM/yyyy", null);
            impress.Date = dateStaffImpressPR;
            impress.Money = txtmoneyImpress;
            try
            {
                db.Impresses.Add(impress);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        public bool AddReward(int selectStaffReward, string dateStaffReward, double txtmoneyReward, string txtDescription)
        {
            Reward reward = new Reward();

            reward.StaffId = selectStaffReward;

            DateTime dateStaffImpressPR = DateTime.ParseExact(dateStaffReward, "dd/MM/yyyy", null);
            reward.Date = dateStaffImpressPR;
            reward.Money = txtmoneyReward;
            reward.Description = txtDescription;
            try
            {
                db.Rewards.Add(reward);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public bool AddStaffTimesheet(int selectStaffTimesheet, string dateStaffTimesheet, string txtDetail, double txttimeLeave, bool staffAllow)
        {
            TimeSheet timesheet = new TimeSheet();

            timesheet.StaffId = selectStaffTimesheet;

            DateTime dateStaffImpressPR = DateTime.ParseExact(dateStaffTimesheet, "dd/MM/yyyy", null);
            timesheet.Date = dateStaffImpressPR;
            timesheet.Description = txtDetail;
            timesheet.Hours = txttimeLeave;
            timesheet.IsAllowed = staffAllow;
            timesheet.Active = true;
            try
            {
                db.TimeSheets.Add(timesheet);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }



        public bool UpdateTimeSheet(int txtTimeSheetId, int txtEditName, string txtEditDate, string txtEditDetail, int txtEdittimeLeave, bool editstaffAllow)
        {
            TimeSheet timesheet = GetTimeSheetUpdateById(txtTimeSheetId);
            if (timesheet == null)
            {
                return false;
            }
            else
            {
                try
                {
                    timesheet.StaffId = txtEditName;

                    DateTime editDate = DateTime.ParseExact(txtEditDate, "dd/MM/yyyy", null);
                    timesheet.Date = editDate;

                    timesheet.Description = txtEditDetail;
                    timesheet.Hours = txtEdittimeLeave;
                    timesheet.IsAllowed = editstaffAllow;
                    timesheet.Active = true;
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
        public bool addNewBenefit(string benefitName, double benefitRate)
        {
            Benefit benefit = new Benefit();
            benefit.Name = benefitName;
            benefit.Rate = benefitRate;
            benefit.Active = true;

            try
            {
                db.Benefits.Add(benefit);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public bool UpdateTax(int txTaxId, string taxName, double taxRate, double taxIndex, double taxFrom, double taxTo)
        {
            Tax tax = GetTaxById(txTaxId);
            if (tax == null)
            {
                return false;
            }
            else
            {
                try
                {
                    tax.Name = taxName;
                    tax.Rate = taxRate;
                    tax.IndexTax = taxIndex;
                    tax.MoneyFrom = taxFrom;
                    tax.MoneyTo = taxTo;
                    tax.Active = true;
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
        public bool UpdateAssurance(int assuranceId, string assuranceName, double assuranceRate)
        {
            Assurance assurance = GetAssuranceById(assuranceId);
            if (assurance == null)
            {
                return false;
            }
            else
            {
                try
                {
                    assurance.Name = assuranceName;
                    assurance.Rate = assuranceRate;
                    assurance.Active = true;
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

        public bool UpdateBenefit(int benefitId, string benefitName, double benefiteRate)
        {
            Benefit benefit = GetBenefitById(benefitId);
            if (benefit == null)
            {
                return false;
            }
            else
            {
                try
                {
                    benefit.Name = benefitName;
                    benefit.Rate = benefiteRate;
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

        public bool UpdateImpress(int txtImpressId, int txtEditName, string txtEditDate, double txtEditMoney)
        {
            Impress impress = GetImpressById(txtImpressId);
            if (impress == null)
            {
                return false;
            }
            else
            {
                try
                {
                    impress.StaffId = txtEditName;

                    DateTime editDate = DateTime.ParseExact(txtEditDate, "dd/MM/yyyy", null);
                    impress.Date = editDate;

                    impress.Money = txtEditMoney;
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

        public bool UpdateReward(int txtRewardId, int txtEditName, string txtEditDate, double txtEditMoney, string editDescription)
        {
            Reward reward = GetRewardById(txtRewardId);
            if (reward == null)
            {
                return false;
            }
            else
            {
                try
                {
                    reward.StaffId = txtEditName;

                    DateTime editDate = DateTime.ParseExact(txtEditDate, "dd/MM/yyyy", null);
                    reward.Date = editDate;

                    reward.Money = txtEditMoney;

                    reward.Description = editDescription;
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


        public string DeleteTax(int txTaxId)
        {
            try
            {
                Tax tax = GetTaxById(txTaxId);
                if ((bool)tax.Active)
                {
                    tax.Active = false;
                    db.SaveChanges();
                    return "inactive";

                }
                else tax.Active = true;
                db.SaveChanges();
                return "active";
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return "Error";
            }
        }
        public bool deleteSalary(int salaryId)
        {
            List<SalaryTaxDetail> salarytaxdetail = GetTaxBySalaryID(salaryId);
            foreach (var offremove in salarytaxdetail)
            {
                db.SalaryTaxDetails.Remove(offremove);
            }
            List<SalaryAssuranceDetail> salaryAssdetail = GetAssuranceBySalaryID(salaryId);
            foreach (var assdetail in salaryAssdetail)
            {
                db.SalaryAssuranceDetails.Remove(assdetail);
            }

            List<SalaryBenefitDetail> salaryBenefitdetail = GetBenefitBySalaryID(salaryId);
            foreach (var benedetail in salaryBenefitdetail)
            {
                db.SalaryBenefitDetails.Remove(benedetail);
            }

            Salary salary = GetSalaryEditByID(salaryId);
            if (salary == null)
            {
                return false;
            }
            else
            {
                try
                {
                    db.Salaries.Remove(salary);
                    salary.Active = false;
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


        public string DeleteAssurance(int assuranceId)
        {
            try
            {
                Assurance assurance = GetAssuranceById(assuranceId);
                if ((bool)assurance.Active)
                {
                    assurance.Active = false;
                    db.SaveChanges();
                    return "inactive";

                }
                else assurance.Active = true;
                db.SaveChanges();
                return "active";
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return "Error";
            }
        }
        public bool deleteImpress(int txtImpressId)
        {
            Impress impress = GetImpressById(txtImpressId);
            if (impress == null)
            {
                return false;
            }
            else
            {
                try
                {
                    db.Impresses.Remove(impress);
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
        public bool deleteReward(int txtRewardId)
        {
            Reward reward = GetRewardById(txtRewardId);
            if (reward == null)
            {
                return false;
            }
            else
            {
                try
                {
                    db.Rewards.Remove(reward);
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
        public bool deleteTimeSheet(int txtTimeSheetId)
        {
            TimeSheet timesheet = GetTimeSheetUpdateById(txtTimeSheetId);
            if (timesheet == null)
            {
                return false;
            }
            else
            {
                try
                {
                    db.TimeSheets.Remove(timesheet);
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
        public String DeleteBenefit(int benefitId)
        {
            try
            {
                Benefit benefit = GetBenefitById(benefitId);
                if ((bool)benefit.Active)
                {
                    benefit.Active = false;
                    db.SaveChanges();
                    return "inactive";

                }
                else benefit.Active = true;
                db.SaveChanges();
                return "active";
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return "Error";
            }
        }


        //Validate input
        public bool CheckExistTaxName(string txtTaxName)
        {
            var tax = db.Taxes.Where(cs => cs.Name == txtTaxName).FirstOrDefault();
            if (tax == null)
            {
                return false;
            }
            return true;
        }

        public bool CheckExistAssuranName(string txtAssuranName)
        {
            var tax = db.Assurances.Where(cs => cs.Name == txtAssuranName).FirstOrDefault();
            if (tax == null)
            {
                return false;
            }
            return true;
        }

        public bool CheckExistBenefitName(string txtBenefitName)
        {
            var tax = db.Benefits.Where(cs => cs.Name == txtBenefitName).FirstOrDefault();
            if (tax == null)
            {
                return false;
            }
            return true;
        }

        public bool CheckDateImpress(string strDateTime)
        {
            //var dateValue = DateTime.Parse(strDateTime);

            var dateValue = DateTime.ParseExact(strDateTime, "dd/MM/yyyy", null);
            var impressMonth = dateValue.Month;
            var year = dateValue.Year;

            var currentYear = DateTime.Now.Year;

            var currentMonth = DateTime.Now.Month;

            if (impressMonth == currentMonth && year == currentYear)
            {
                return false;
            }
            return true;
        }
        public bool CheckDateIsSunday(string strDateTime)
        {
            //var dateValue = DateTime.Parse(strDateTime);

            var dateValue = DateTime.ParseExact(strDateTime, "dd/MM/yyyy", null);


            if (dateValue.DayOfWeek != DayOfWeek.Saturday && dateValue.DayOfWeek != DayOfWeek.Sunday)
            {
                return false;
            }
            return true;
        }

        public bool CheckDateEditSalary(string strDateTime)
        {
            //var dateValue = DateTime.Parse(strDateTime);

            var dateValue = DateTime.ParseExact(strDateTime, "dd/MM/yyyy", null);
            var dateEdit = dateValue.Month;
            

           
            var currentMonth = DateTime.Now.Month;

            if (dateEdit < currentMonth )
            {
               
                    return true;
             
              
            }
            return false;
        }


        public List<int> GetOfficeHasCaseInWork(List<int> listCase)
        {
            try
            {
                var officeInwork = db.Cases.Where(cs => listCase.Contains(cs.CaseId) && cs.Status == "Đang thụ lý").DistinctBy(cs => cs.OfficeId).Select(cs => cs.OfficeId).ToList();
                return officeInwork;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        }

        /// <summary>
        /// Get List Salary for export excel
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        public SalaryExcelEntity GetSalaryByMonthExcel(int month, int year)
        {
            SalaryExcelEntity salaryResult = new SalaryExcelEntity();
            List<SalaryConcertEntity> listSalary = new List<SalaryConcertEntity>();
            if (month != 0)
            {
                salaryResult.TimeValue = "Tháng : " + month + "       Năm : " + year;
                var result = (from salary in db.Salaries
                              from staff in db.Staffs
                              where salary.Active == true
                                    && salary.Date.Month == month
                                    && salary.Date.Year == year
                                    && salary.StaffId == staff.StaffId
                                    && salary.Active == true
                              select new
                              {
                                  Salary = salary,
                                  Staff = staff
                              });
                foreach (var item in result)
                {
                    SalaryConcertEntity salary = new SalaryConcertEntity();
                    salary.SalaryID = item.Salary.SalaryID;
                    salary.Date = item.Salary.Date;
                    salary.StaffId = item.Salary.StaffId;
                    salary.TotalAbsent = item.Salary.TotalAbsent;
                    salary.TotalPresent = item.Salary.TotalPresent;
                    salary.TotalBenefit = item.Salary.TotalBenefit;
                    salary.TotalImprest = item.Salary.TotalImprest;
                    salary.TotalTax = item.Salary.TotalTax;
                    salary.TotalAssurance = item.Salary.TotalAssurance;
                    salary.TotalReward = item.Salary.TotalReward;
                    salary.BaseSalary = item.Salary.BaseSalary;
                    salary.TotalDateDeduction = item.Salary.TotalDateDeduction;
                    salary.TotalMoneyDeduction = item.Salary.TotalMoneyDeduction;
                    salary.ActualSalary = item.Salary.ActualSalary;
                    salary.Active = item.Salary.Active;
                    salary.AppendantPeople = (int)item.Staff.AppendantPeople;
                    salary.StaffName = item.Staff.StaffName;
                    listSalary.Add(salary);
                }
                salaryResult.ListSalary = listSalary;
            }
            else if (month == 0)
            {
                salaryResult.TimeValue = "Năm: " + year;
                var result = (from salary in db.Salaries
                              from staff in db.Staffs
                              where salary.Active == true
                                    && salary.Date.Year == year
                                    && salary.StaffId == staff.StaffId
                                    && salary.Active == true
                              select new
                              {
                                  Salary = salary,
                                  Staff = staff
                              });
                foreach (var item in result)
                {
                    SalaryConcertEntity salary = new SalaryConcertEntity();
                    salary.SalaryID = item.Salary.SalaryID;
                    salary.Date = item.Salary.Date;
                    salary.StaffId = item.Salary.StaffId;
                    salary.TotalAbsent = item.Salary.TotalAbsent;
                    salary.TotalPresent = item.Salary.TotalPresent;
                    salary.TotalBenefit = item.Salary.TotalBenefit;
                    salary.TotalImprest = item.Salary.TotalImprest;
                    salary.TotalTax = item.Salary.TotalTax;
                    salary.TotalAssurance = item.Salary.TotalAssurance;
                    salary.TotalReward = item.Salary.TotalReward;
                    salary.BaseSalary = item.Salary.BaseSalary;
                    salary.ActualSalary = item.Salary.ActualSalary;
                    salary.TotalDateDeduction = item.Salary.TotalDateDeduction;
                    salary.TotalMoneyDeduction = item.Salary.TotalMoneyDeduction;
                    salary.Active = item.Salary.Active;
                    salary.AppendantPeople = (int)item.Staff.AppendantPeople;
                    salary.StaffName = item.Staff.StaffName;
                    listSalary.Add(salary);
                }
                salaryResult.ListSalary = listSalary;
            }
            return salaryResult;
        }
    }
}