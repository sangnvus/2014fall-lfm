using LFMS.Models.Entity;
using LFMS.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LFMS.Models.DAO
{
    public class StatisticDAO
    {
        LFMSEntities db;
        public StatisticDAO()
        {
            db = new LFMSEntities();
        }

        public StatisticData Get12MonthRevenue()
        {
            StatisticData statisticData = new StatisticData();

            int month1 = DateTime.Now.Month - 1;
            int year1 = DateTime.Now.Year;
            if (month1 <= 0)
            {
                month1 += 12;
                year1 -= 1;
            }
            statisticData.feild1 = GetRevenueInMonth(month1, year1);

            int month2 = DateTime.Now.Month - 2;
            int year2 = DateTime.Now.Year;
            if (month2 <= 0)
            {
                month2 += 12;
                year2 -= 1;
            }
            statisticData.feild2 = GetRevenueInMonth(month2, year2);

            int month3 = DateTime.Now.Month - 3;
            int year3 = DateTime.Now.Year;
            if (month3 <= 0)
            {
                month3 += 12;
                year3 -= 1;
            }
            statisticData.feild3 = GetRevenueInMonth(month3, year3);

            int month4 = DateTime.Now.Month - 4;
            int year4 = DateTime.Now.Year;
            if (month4 <= 0)
            {
                month4 += 12;
                year4 -= 1;
            }
            statisticData.feild4 = GetRevenueInMonth(month4, year4);

            int month5 = DateTime.Now.Month - 5;
            int year5 = DateTime.Now.Year;
            if (month5 <= 0)
            {
                month5 += 12;
                year5 -= 1;
            }
            statisticData.feild5 = GetRevenueInMonth(month5, year5);

            int month6 = DateTime.Now.Month - 6;
            int year6 = DateTime.Now.Year;
            if (month6 <= 0)
            {
                month6 += 12;
                year6 -= 1;
            }
            statisticData.feild6 = GetRevenueInMonth(month6, year6);

            int month7 = DateTime.Now.Month - 7;
            int year7 = DateTime.Now.Year;
            if (month7 <= 0)
            {
                month7 += 12;
                year7 -= 1;
            }
            statisticData.feild7 = GetRevenueInMonth(month7, year7);

            int month8 = DateTime.Now.Month - 8;
            int year8 = DateTime.Now.Year;
            if (month8 <= 0)
            {
                month8 += 12;
                year8 -= 1;
            }
            statisticData.feild8 = GetRevenueInMonth(month8, year8);

            int month9 = DateTime.Now.Month - 9;
            int year9 = DateTime.Now.Year;
            if (month9 <= 0)
            {
                month9 += 12;
                year9 -= 1;
            }
            double totalMoney9 = GetRevenueInMonth(month9, year9);

            statisticData.feild9 = totalMoney9;

            int month10 = DateTime.Now.Month - 10;
            int year10 = DateTime.Now.Year;
            if (month1 <= 0)
            {
                month10 += 12;
                year10 -= 1;
            }
            statisticData.feild10 = GetRevenueInMonth(month10, year10);

            int month11 = DateTime.Now.Month - 11;
            int year11 = DateTime.Now.Year;
            if (month11 <= 0)
            {
                month11 += 12;
                year11 -= 1;
            }

            statisticData.feild11 = GetRevenueInMonth(month11, year11);

            int month12 = DateTime.Now.Month - 12;
            int year12 = DateTime.Now.Year;
            if (month12 <= 0)
            {
                month12 += 12;
                year12 -= 1;
            }
            statisticData.feild12 = GetRevenueInMonth(month12, year12);

            return statisticData;
        }
        public StatisticData Get12MonthRevenueByOfficeId(int officeId)
        {
            StatisticData statisticData = new StatisticData();

            int month1 = DateTime.Now.Month - 1;
            int year1 = DateTime.Now.Year;
            if (month1 <= 0)
            {
                month1 += 12;
                year1 -= 1;
            }
            statisticData.feild1 = GetRevenueInMonthByOfficeId(month1, year1, officeId);

            int month2 = DateTime.Now.Month - 2;
            int year2 = DateTime.Now.Year;
            if (month2 <= 0)
            {
                month2 += 12;
                year2 -= 1;
            }
            statisticData.feild2 = GetRevenueInMonthByOfficeId(month2, year2, officeId);

            int month3 = DateTime.Now.Month - 3;
            int year3 = DateTime.Now.Year;
            if (month3 <= 0)
            {
                month3 += 12;
                year3 -= 1;
            }
            statisticData.feild3 = GetRevenueInMonthByOfficeId(month3, year3, officeId);

            int month4 = DateTime.Now.Month - 4;
            int year4 = DateTime.Now.Year;
            if (month4 <= 0)
            {
                month4 += 12;
                year4 -= 1;
            }
            statisticData.feild4 = GetRevenueInMonthByOfficeId(month4, year4, officeId);

            int month5 = DateTime.Now.Month - 5;
            int year5 = DateTime.Now.Year;
            if (month5 <= 0)
            {
                month5 += 12;
                year5 -= 1;
            }
            statisticData.feild5 = GetRevenueInMonthByOfficeId(month5, year5, officeId);

            int month6 = DateTime.Now.Month - 6;
            int year6 = DateTime.Now.Year;
            if (month6 <= 0)
            {
                month6 += 12;
                year6 -= 1;
            }
            statisticData.feild6 = GetRevenueInMonthByOfficeId(month6, year6, officeId);

            int month7 = DateTime.Now.Month - 7;
            int year7 = DateTime.Now.Year;
            if (month7 <= 0)
            {
                month7 += 12;
                year7 -= 1;
            }
            statisticData.feild7 = GetRevenueInMonthByOfficeId(month7, year7, officeId);

            int month8 = DateTime.Now.Month - 8;
            int year8 = DateTime.Now.Year;
            if (month8 <= 0)
            {
                month8 += 12;
                year8 -= 1;
            }
            statisticData.feild8 = GetRevenueInMonthByOfficeId(month8, year8, officeId);

            int month9 = DateTime.Now.Month - 9;
            int year9 = DateTime.Now.Year;
            if (month9 <= 0)
            {
                month9 += 12;
                year9 -= 1;
            }
            statisticData.feild9 = GetRevenueInMonthByOfficeId(month9, year9, officeId);

            int month10 = DateTime.Now.Month - 10;
            int year10 = DateTime.Now.Year;
            if (month1 <= 0)
            {
                month10 += 12;
                year10 -= 1;
            }
            statisticData.feild10 = GetRevenueInMonthByOfficeId(month10, year10, officeId);

            int month11 = DateTime.Now.Month - 11;
            int year11 = DateTime.Now.Year;
            if (month11 <= 0)
            {
                month11 += 12;
                year11 -= 1;
            }
            statisticData.feild11 = GetRevenueInMonthByOfficeId(month11, year11, officeId);

            int month12 = DateTime.Now.Month - 12;
            int year12 = DateTime.Now.Year;
            if (month12 <= 0)
            {
                month12 += 12;
                year12 -= 1;
            }
            statisticData.feild12 = GetRevenueInMonthByOfficeId(month12, year12, officeId);

            return statisticData;
        }
        public StatisticData GetLastYearRevenue()
        {
            StatisticData statisticData = new StatisticData();
            statisticData.feild1 = GetRevenueInMonth(1, DateTime.Now.Year - 1);
            statisticData.feild2 = GetRevenueInMonth(2, DateTime.Now.Year - 1);
            statisticData.feild3 = GetRevenueInMonth(3, DateTime.Now.Year - 1);
            statisticData.feild4 = GetRevenueInMonth(4, DateTime.Now.Year - 1);
            statisticData.feild5 = GetRevenueInMonth(5, DateTime.Now.Year - 1);
            statisticData.feild6 = GetRevenueInMonth(6, DateTime.Now.Year - 1);
            statisticData.feild7 = GetRevenueInMonth(7, DateTime.Now.Year - 1);
            statisticData.feild8 = GetRevenueInMonth(8, DateTime.Now.Year - 1);
            statisticData.feild9 = GetRevenueInMonth(9, DateTime.Now.Year - 1);
            statisticData.feild10 = GetRevenueInMonth(10, DateTime.Now.Year - 1);
            statisticData.feild11 = GetRevenueInMonth(11, DateTime.Now.Year - 1);
            statisticData.feild12 = GetRevenueInMonth(12, DateTime.Now.Year - 1);
            return statisticData;
        }
        public StatisticData GetLastYearRevenueByOfficeId(int officeId)
        {
            StatisticData statisticData = new StatisticData();
            statisticData.feild1 = GetRevenueInMonthByOfficeId(1, DateTime.Now.Year - 1, officeId);
            statisticData.feild2 = GetRevenueInMonthByOfficeId(2, DateTime.Now.Year - 1, officeId);
            statisticData.feild3 = GetRevenueInMonthByOfficeId(3, DateTime.Now.Year - 1, officeId);
            statisticData.feild4 = GetRevenueInMonthByOfficeId(4, DateTime.Now.Year - 1, officeId);
            statisticData.feild5 = GetRevenueInMonthByOfficeId(5, DateTime.Now.Year - 1, officeId);
            statisticData.feild6 = GetRevenueInMonthByOfficeId(6, DateTime.Now.Year - 1, officeId);
            statisticData.feild7 = GetRevenueInMonthByOfficeId(7, DateTime.Now.Year - 1, officeId);
            statisticData.feild8 = GetRevenueInMonthByOfficeId(8, DateTime.Now.Year - 1, officeId);
            statisticData.feild9 = GetRevenueInMonthByOfficeId(9, DateTime.Now.Year - 1, officeId);
            statisticData.feild10 = GetRevenueInMonthByOfficeId(10, DateTime.Now.Year - 1, officeId);
            statisticData.feild11 = GetRevenueInMonthByOfficeId(11, DateTime.Now.Year - 1, officeId);
            statisticData.feild12 = GetRevenueInMonthByOfficeId(12, DateTime.Now.Year - 1, officeId);
            return statisticData;
        }

        public Dictionary<string, double> GetYearsRevenue(int from, int to)
        {
            Dictionary<string, double> list = new Dictionary<string,double>();
            
            for(int i = from; i <= to; i++)
            {
                var listPayment = db.Payments.Where(p => p.PaymentTime.Year == i).ToList();
                //var listSalary = db.Salaries.Where(s => s.Date.Year == i).ToList();
                //var listOtherCost = db.OtherCosts.Where(o => o.Date.Year == i).ToList();
                double result = 0;
                foreach (var payment in listPayment)
                {
                    result += payment.PaymentMoney;
                }
                /*foreach (var salary in listSalary)
                {
                    result -= salary.TotalBenefit;
                    result -= salary.TotalImprest;
                    result -= salary.TotalTax;
                    result -= salary.TotalAssurance;
                    result -= salary.TotalReward;
                    result += salary.TotalMoneyDeduction;
                }
                foreach (var otherCost in listOtherCost)
                {
                    result -= otherCost.Cost;
                }*/
                list.Add(i.ToString(), result);
            }
            return list;
        }
        public Dictionary<string, double> GetYearsRevenueByOfficeId(int from, int to, int officeId)
        {
            Dictionary<string, double> list = new Dictionary<string, double>();

            for (int i = from; i <= to; i++)
            {
                var listPayment = db.Payments.Where(p => p.PaymentTime.Year == i && p.Case.Office.OfficeId == officeId).ToList();
                //var listSalary = db.Salaries.Where(s => s.Date.Year == i && s.Staff.Office_Staff.Any(os => os.OfficeId == officeId)).ToList();
                //var listOtherCost = db.OtherCosts.Where(o => o.Date.Year == i && o.OfficeId == officeId).ToList();
                double result = 0;
                foreach (var payment in listPayment)
                {
                    result += payment.PaymentMoney;
                }
                /*foreach (var salary in listSalary)
                {
                    result -= salary.TotalBenefit;
                    result -= salary.TotalImprest;
                    result -= salary.TotalTax;
                    result -= salary.TotalAssurance;
                    result -= salary.TotalReward;
                    result += salary.TotalMoneyDeduction;
                }
                foreach (var otherCost in listOtherCost)
                {
                    result -= otherCost.Cost;
                }*/
                list.Add(i.ToString(), result);
            }
            return list;
        }

        public Dictionary<string, int> GetFromYearRevenue()
        {
            Dictionary<string, int> list = new Dictionary<string, int>();
            var payment = db.Payments.OrderBy(p => p.PaymentTime).FirstOrDefault();
            int from = payment.PaymentTime.Year;
            int to = DateTime.Now.Year - 2;
            for (int i = from; i <= to; i++)
            {
                list.Add(i.ToString(), i);
            }
            return list;
        }
        public Dictionary<string, int> GetToYearRevenue(int year)
        {
            Dictionary<string, int> list = new Dictionary<string, int>();
            int from = year + 1;
            int to = DateTime.Now.Year - 1;
            int to2 = from + 10;
            if (to > to2)
            {
                to = to2;
            }
            for (int i = from; i <= to; i++)
            {
                list.Add(i.ToString(), i);
            }
            return list;
        }
        //================================================================================
        private double GetRevenueInMonth(int month, int year)
        {
            var listPayment = db.Payments.Where(p => p.PaymentTime.Month == month && p.PaymentTime.Year == year).ToList();
            //var listSalary = db.Salaries.Where(s => s.Date.Month == month && s.Date.Year == year).ToList();
            //var listOtherCost = db.OtherCosts.Where(o => o.Date.Month == month && o.Date.Year == year).ToList();
            double result = 0;
            foreach (var payment in listPayment)
            {
                result += payment.PaymentMoney;
            }
            /*foreach (var salary in listSalary)
            {
                result -= salary.TotalBenefit;
                result -= salary.TotalImprest;
                result -= salary.TotalTax;
                result -= salary.TotalAssurance;
                result -= salary.TotalReward;
                //result += salary.TotalMoneyDeduction;
            }
            foreach (var otherCost in listOtherCost)
            {
                result -= otherCost.Cost;
            }*/
            return result;
        }

        private double GetRevenueInMonthByOfficeId(int month, int year, int officeId)
        {
            var listPayment = db.Payments.Where(p => p.PaymentTime.Month == month && p.PaymentTime.Year == year && p.Case.OfficeId == officeId).ToList();
            //var listSalary = db.Salaries.Where(s => s.Date.Month == month && s.Date.Year == year && s.Staff.Office_Staff.Any(os => os.OfficeId == officeId)).ToList();
            //var listOtherCost = db.OtherCosts.Where(o => o.Date.Month == month && o.Date.Year == year && o.OfficeId == officeId).ToList();
            double result = 0;
            foreach (var payment in listPayment)
            {
                result += payment.PaymentMoney;
            }
            /*foreach (var salary in listSalary)
            {
                result -= salary.TotalBenefit;
                result -= salary.TotalImprest;
                result -= salary.TotalTax;
                result -= salary.TotalAssurance;
                result -= salary.TotalReward;
                //result += salary.TotalMoneyDeduction;
            }
            foreach (var otherCost in listOtherCost)
            {
                result -= otherCost.Cost;
            }*/
            return result;
        }

        private double GetRevenueInMonthByStaffId(int month, int year, int staffId)
        {

            var listPayment = db.Payments.Where(p => p.Case.Case_Staff.Any(cs => cs.StaffId == staffId) && p.PaymentTime.Month == month && p.PaymentTime.Year == year).ToList();
            //var listSalary = db.Salaries.Where(s => s.Date.Month == month && s.Date.Year == year && s.StaffId == staffId).ToList();
            double result = 0;
            foreach (var payment in listPayment)
            {
                result += payment.PaymentMoney;
            }
            /*foreach (var salary in listSalary)
            {
                result -= salary.TotalBenefit;
                result -= salary.TotalImprest;
                result -= salary.TotalTax;
                result -= salary.TotalAssurance;
                result -= salary.TotalReward;
                //result += salary.TotalMoneyDeduction;
            }*/

            return result;
        }

        private int GetCaseNumInMonthByStaffId(int month, int year, int staffId)
        {
            var count = db.Case_Staff.Where(cs => cs.StaffId == 1 && cs.Case.ReceiptDate.Month == month && cs.Case.ReceiptDate.Year == year).ToList().Count();
            return count;
        }
        //================================================================================

        public StatisticData Get12MonthCase()
        {
            StatisticData statisticData = new StatisticData();

            int month1 = DateTime.Now.Month - 1;
            int year1 = DateTime.Now.Year;
            if (month1 <= 0)
            {
                month1 += 12;
                year1 -= 1;
            }
            statisticData.feild1 = db.Cases.Where(c => c.ReceiptDate.Month == month1 && c.ReceiptDate.Year == year1).ToList().Count();

            int month2 = DateTime.Now.Month - 2;
            int year2 = DateTime.Now.Year;
            if (month2 <= 0)
            {
                month2 += 12;
                year2 -= 1;
            }
            statisticData.feild2 = db.Cases.Where(c => c.ReceiptDate.Month == month2 && c.ReceiptDate.Year == year2).ToList().Count();

            int month3 = DateTime.Now.Month - 3;
            int year3 = DateTime.Now.Year;
            if (month3 <= 0)
            {
                month3 += 12;
                year3 -= 1;
            }
            statisticData.feild3 = db.Cases.Where(c => c.ReceiptDate.Month == month3 && c.ReceiptDate.Year == year3).ToList().Count();

            int month4 = DateTime.Now.Month - 4;
            int year4 = DateTime.Now.Year;
            if (month4 <= 0)
            {
                month4 += 12;
                year4 -= 1;
            }
            statisticData.feild4 = db.Cases.Where(c => c.ReceiptDate.Month == month4 && c.ReceiptDate.Year == year4).ToList().Count();

            int month5 = DateTime.Now.Month - 5;
            int year5 = DateTime.Now.Year;
            if (month5 <= 0)
            {
                month5 += 12;
                year5 -= 1;
            }
            statisticData.feild5 = db.Cases.Where(c => c.ReceiptDate.Month == month5 && c.ReceiptDate.Year == year5).ToList().Count();

            int month6 = DateTime.Now.Month - 6;
            int year6 = DateTime.Now.Year;
            if (month6 <= 0)
            {
                month6 += 12;
                year6 -= 1;
            }
            statisticData.feild6 = db.Cases.Where(c => c.ReceiptDate.Month == month6 && c.ReceiptDate.Year == year6).ToList().Count();

            int month7 = DateTime.Now.Month - 7;
            int year7 = DateTime.Now.Year;
            if (month7 <= 0)
            {
                month7 += 12;
                year7 -= 1;
            }
            statisticData.feild7 = db.Cases.Where(c => c.ReceiptDate.Month == month7 && c.ReceiptDate.Year == year7).ToList().Count();

            int month8 = DateTime.Now.Month - 8;
            int year8 = DateTime.Now.Year;
            if (month8 <= 0)
            {
                month8 += 12;
                year8 -= 1;
            }
            statisticData.feild8 = db.Cases.Where(c => c.ReceiptDate.Month == month8 && c.ReceiptDate.Year == year8).ToList().Count();

            int month9 = DateTime.Now.Month - 9;
            int year9 = DateTime.Now.Year;
            if (month9 <= 0)
            {
                month9 += 12;
                year9 -= 1;
            }
            double totalMoney9 = db.Cases.Where(c => c.ReceiptDate.Month == month9 && c.ReceiptDate.Year == year9).ToList().Count();

            statisticData.feild9 = totalMoney9;

            int month10 = DateTime.Now.Month - 10;
            int year10 = DateTime.Now.Year;
            if (month1 <= 0)
            {
                month10 += 12;
                year10 -= 1;
            }
            statisticData.feild10 = db.Cases.Where(c => c.ReceiptDate.Month == month10 && c.ReceiptDate.Year == year10).ToList().Count();

            int month11 = DateTime.Now.Month - 11;
            int year11 = DateTime.Now.Year;
            if (month11 <= 0)
            {
                month11 += 12;
                year11 -= 1;
            }

            statisticData.feild11 = db.Cases.Where(c => c.ReceiptDate.Month == month11 && c.ReceiptDate.Year == year11).ToList().Count();

            int month12 = DateTime.Now.Month - 12;
            int year12 = DateTime.Now.Year;
            if (month12 <= 0)
            {
                month12 += 12;
                year12 -= 1;
            }
            statisticData.feild12 = db.Cases.Where(c => c.ReceiptDate.Month == month12 && c.ReceiptDate.Year == year12).ToList().Count();

            return statisticData;
        }
        public StatisticData Get12MonthCaseByOfficeId(int officeId)
        {
            StatisticData statisticData = new StatisticData();

            int month1 = DateTime.Now.Month - 1;
            int year1 = DateTime.Now.Year;
            if (month1 <= 0)
            {
                month1 += 12;
                year1 -= 1;
            }
            statisticData.feild1 = db.Cases.Where(c => c.ReceiptDate.Month == month1 && c.ReceiptDate.Year == year1 && c.OfficeId == officeId).ToList().Count();

            int month2 = DateTime.Now.Month - 2;
            int year2 = DateTime.Now.Year;
            if (month2 <= 0)
            {
                month2 += 12;
                year2 -= 1;
            }
            statisticData.feild2 = db.Cases.Where(c => c.ReceiptDate.Month == month2 && c.ReceiptDate.Year == year2 && c.OfficeId == officeId).ToList().Count();

            int month3 = DateTime.Now.Month - 3;
            int year3 = DateTime.Now.Year;
            if (month3 <= 0)
            {
                month3 += 12;
                year3 -= 1;
            }
            statisticData.feild3 = db.Cases.Where(c => c.ReceiptDate.Month == month3 && c.ReceiptDate.Year == year3 && c.OfficeId == officeId).ToList().Count();

            int month4 = DateTime.Now.Month - 4;
            int year4 = DateTime.Now.Year;
            if (month4 <= 0)
            {
                month4 += 12;
                year4 -= 1;
            }
            statisticData.feild4 = db.Cases.Where(c => c.ReceiptDate.Month == month4 && c.ReceiptDate.Year == year4 && c.OfficeId == officeId).ToList().Count();

            int month5 = DateTime.Now.Month - 5;
            int year5 = DateTime.Now.Year;
            if (month5 <= 0)
            {
                month5 += 12;
                year5 -= 1;
            }
            statisticData.feild5 = db.Cases.Where(c => c.ReceiptDate.Month == month5 && c.ReceiptDate.Year == year5 && c.OfficeId == officeId).ToList().Count();

            int month6 = DateTime.Now.Month - 6;
            int year6 = DateTime.Now.Year;
            if (month6 <= 0)
            {
                month6 += 12;
                year6 -= 1;
            }
            statisticData.feild6 = db.Cases.Where(c => c.ReceiptDate.Month == month6 && c.ReceiptDate.Year == year6 && c.OfficeId == officeId).ToList().Count();

            int month7 = DateTime.Now.Month - 7;
            int year7 = DateTime.Now.Year;
            if (month7 <= 0)
            {
                month7 += 12;
                year7 -= 1;
            }
            statisticData.feild7 = db.Cases.Where(c => c.ReceiptDate.Month == month7 && c.ReceiptDate.Year == year7 && c.OfficeId == officeId).ToList().Count();

            int month8 = DateTime.Now.Month - 8;
            int year8 = DateTime.Now.Year;
            if (month8 <= 0)
            {
                month8 += 12;
                year8 -= 1;
            }
            double totalMoney8 = db.Cases.Where(c => c.ReceiptDate.Month == month8 && c.ReceiptDate.Year == year8 && c.OfficeId == officeId).ToList().Count();

            statisticData.feild8 = totalMoney8;

            int month9 = DateTime.Now.Month - 1;
            int year9 = DateTime.Now.Year;
            if (month9 <= 0)
            {
                month9 += 12;
                year9 -= 1;
            }
            double totalMoney9 = db.Cases.Where(c => c.ReceiptDate.Month == month9 && c.ReceiptDate.Year == year9 && c.OfficeId == officeId).ToList().Count();

            statisticData.feild9 = totalMoney9;

            int month10 = DateTime.Now.Month - 10;
            int year10 = DateTime.Now.Year;
            if (month1 <= 0)
            {
                month10 += 12;
                year10 -= 1;
            }
            statisticData.feild10 = db.Cases.Where(c => c.ReceiptDate.Month == month10 && c.ReceiptDate.Year == year10 && c.OfficeId == officeId).ToList().Count();

            int month11 = DateTime.Now.Month - 11;
            int year11 = DateTime.Now.Year;
            if (month11 <= 0)
            {
                month11 += 12;
                year11 -= 1;
            }

            statisticData.feild11 = db.Cases.Where(c => c.ReceiptDate.Month == month11 && c.ReceiptDate.Year == year11 && c.OfficeId == officeId).ToList().Count();

            int month12 = DateTime.Now.Month - 12;
            int year12 = DateTime.Now.Year;
            if (month12 <= 0)
            {
                month12 += 12;
                year12 -= 1;
            }
            statisticData.feild12 = db.Cases.Where(c => c.ReceiptDate.Month == month12 && c.ReceiptDate.Year == year12 && c.OfficeId == officeId).ToList().Count();

            return statisticData;
        }
        public StatisticData GetLastYearCase()
        {
            StatisticData statisticData = new StatisticData();
            int lastYear = DateTime.Now.Year - 1;
            statisticData.feild1 = db.Cases.Where(c => c.ReceiptDate.Month == 1 && c.ReceiptDate.Year == lastYear).ToList().Count();
            statisticData.feild2 = db.Cases.Where(c => c.ReceiptDate.Month == 2 && c.ReceiptDate.Year == lastYear).ToList().Count();
            statisticData.feild3 = db.Cases.Where(c => c.ReceiptDate.Month == 3 && c.ReceiptDate.Year == lastYear).ToList().Count();
            statisticData.feild4 = db.Cases.Where(c => c.ReceiptDate.Month == 4 && c.ReceiptDate.Year == lastYear).ToList().Count();
            statisticData.feild5 = db.Cases.Where(c => c.ReceiptDate.Month == 5 && c.ReceiptDate.Year == lastYear).ToList().Count();
            statisticData.feild6 = db.Cases.Where(c => c.ReceiptDate.Month == 6 && c.ReceiptDate.Year == lastYear).ToList().Count();
            statisticData.feild7 = db.Cases.Where(c => c.ReceiptDate.Month == 7 && c.ReceiptDate.Year == lastYear).ToList().Count();
            statisticData.feild8 = db.Cases.Where(c => c.ReceiptDate.Month == 8 && c.ReceiptDate.Year == lastYear).ToList().Count();
            statisticData.feild9 = db.Cases.Where(c => c.ReceiptDate.Month == 9 && c.ReceiptDate.Year == lastYear).ToList().Count();
            statisticData.feild10 = db.Cases.Where(c => c.ReceiptDate.Month == 10 && c.ReceiptDate.Year == lastYear).ToList().Count();
            statisticData.feild11 = db.Cases.Where(c => c.ReceiptDate.Month == 11 && c.ReceiptDate.Year == lastYear).ToList().Count();
            statisticData.feild12 = db.Cases.Where(c => c.ReceiptDate.Month == 12 && c.ReceiptDate.Year == lastYear).ToList().Count();
            return statisticData;
        }
        public StatisticData GetLastYearCaseByOfficeId(int officeId)
        {
            StatisticData statisticData = new StatisticData();
            int lastYear = DateTime.Now.Year - 1;
            statisticData.feild1 = db.Cases.Where(c => c.ReceiptDate.Month == 1 && c.ReceiptDate.Year == lastYear && c.OfficeId == officeId).ToList().Count();
            statisticData.feild2 = db.Cases.Where(c => c.ReceiptDate.Month == 2 && c.ReceiptDate.Year == lastYear && c.OfficeId == officeId).ToList().Count();
            statisticData.feild3 = db.Cases.Where(c => c.ReceiptDate.Month == 3 && c.ReceiptDate.Year == lastYear && c.OfficeId == officeId).ToList().Count();
            statisticData.feild4 = db.Cases.Where(c => c.ReceiptDate.Month == 4 && c.ReceiptDate.Year == lastYear && c.OfficeId == officeId).ToList().Count();
            statisticData.feild5 = db.Cases.Where(c => c.ReceiptDate.Month == 5 && c.ReceiptDate.Year == lastYear && c.OfficeId == officeId).ToList().Count();
            statisticData.feild6 = db.Cases.Where(c => c.ReceiptDate.Month == 6 && c.ReceiptDate.Year == lastYear && c.OfficeId == officeId).ToList().Count();
            statisticData.feild7 = db.Cases.Where(c => c.ReceiptDate.Month == 7 && c.ReceiptDate.Year == lastYear && c.OfficeId == officeId).ToList().Count();
            statisticData.feild8 = db.Cases.Where(c => c.ReceiptDate.Month == 8 && c.ReceiptDate.Year == lastYear && c.OfficeId == officeId).ToList().Count();
            statisticData.feild9 = db.Cases.Where(c => c.ReceiptDate.Month == 9 && c.ReceiptDate.Year == lastYear && c.OfficeId == officeId).ToList().Count();
            statisticData.feild10 = db.Cases.Where(c => c.ReceiptDate.Month == 10 && c.ReceiptDate.Year == lastYear && c.OfficeId == officeId).ToList().Count();
            statisticData.feild11 = db.Cases.Where(c => c.ReceiptDate.Month == 11 && c.ReceiptDate.Year == lastYear && c.OfficeId == officeId).ToList().Count();
            statisticData.feild12 = db.Cases.Where(c => c.ReceiptDate.Month == 12 && c.ReceiptDate.Year == lastYear && c.OfficeId == officeId).ToList().Count();
            return statisticData;
        }
        public Dictionary<string, double> GetYearsCase(int from, int to)
        {
            Dictionary<string, double> list = new Dictionary<string, double>();

            for (int i = from; i <= to; i++)
            {
                var sum = db.Cases.Where(c => c.ReceiptDate.Year == i).ToList().Count();
                list.Add(i.ToString(), sum);
            }
            return list;
        }
        public Dictionary<string, double> GetYearsCaseByOfficeId(int from, int to, int officeId)
        {
            Dictionary<string, double> list = new Dictionary<string, double>();

            for (int i = from; i <= to; i++)
            {
                var sum = db.Cases.Where(c => c.ReceiptDate.Year == i && c.OfficeId == officeId).ToList().Count();
                list.Add(i.ToString(), sum);
            }
            return list;
        }
        public Dictionary<string, int> GetFromYearCase()
        {
            Dictionary<string, int> list = new Dictionary<string, int>();
            var cas = db.Cases.OrderBy(c => c.ReceiptDate).FirstOrDefault();
            int from = cas.ReceiptDate.Year;
            int to = DateTime.Now.Year - 2;
            for (int i = from; i <= to; i++)
            {
                list.Add(i.ToString(), i);
            }
            return list;
        }
        public Dictionary<string, int> GetToYearCase(int year)
        {
            Dictionary<string, int> list = new Dictionary<string, int>();
            int from = year + 1;
            int to = DateTime.Now.Year - 1;
            int to2 = from + 10;
            if (to > to2)
            {
                to = to2;
            }
            for (int i = from; i <= to; i++)
            {
                list.Add(i.ToString(), i);
            }
            return list;
        }
        //================================================================================
        public StatisticData Get12MonthStaffRevenue(int staffId)
        {
            StatisticData statisticData = new StatisticData();

            int month1 = DateTime.Now.Month - 1;
            int year1 = DateTime.Now.Year;
            if (month1 <= 0)
            {
                month1 += 12;
                year1 -= 1;
            }
            statisticData.feild1 = GetRevenueInMonthByStaffId(month1, year1, staffId);

            int month2 = DateTime.Now.Month - 2;
            int year2 = DateTime.Now.Year;
            if (month2 <= 0)
            {
                month2 += 12;
                year2 -= 1;
            }
            statisticData.feild2 = GetRevenueInMonthByStaffId(month2, year2, staffId);

            int month3 = DateTime.Now.Month - 3;
            int year3 = DateTime.Now.Year;
            if (month3 <= 0)
            {
                month3 += 12;
                year3 -= 1;
            }
            statisticData.feild3 = GetRevenueInMonthByStaffId(month3, year3, staffId);

            int month4 = DateTime.Now.Month - 4;
            int year4 = DateTime.Now.Year;
            if (month4 <= 0)
            {
                month4 += 12;
                year4 -= 1;
            }
            statisticData.feild4 = GetRevenueInMonthByStaffId(month4, year4, staffId);

            int month5 = DateTime.Now.Month - 5;
            int year5 = DateTime.Now.Year;
            if (month5 <= 0)
            {
                month5 += 12;
                year5 -= 1;
            }
            statisticData.feild5 = GetRevenueInMonthByStaffId(month5, year5, staffId);

            int month6 = DateTime.Now.Month - 6;
            int year6 = DateTime.Now.Year;
            if (month6 <= 0)
            {
                month6 += 12;
                year6 -= 1;
            }
            statisticData.feild6 = GetRevenueInMonthByStaffId(month6, year6, staffId);

            int month7 = DateTime.Now.Month - 7;
            int year7 = DateTime.Now.Year;
            if (month7 <= 0)
            {
                month7 += 12;
                year7 -= 1;
            }
            statisticData.feild7 = GetRevenueInMonthByStaffId(month7, year7, staffId);

            int month8 = DateTime.Now.Month - 8;
            int year8 = DateTime.Now.Year;
            if (month8 <= 0)
            {
                month8 += 12;
                year8 -= 1;
            }
            statisticData.feild8 = GetRevenueInMonthByStaffId(month8, year8, staffId);

            int month9 = DateTime.Now.Month - 9;
            int year9 = DateTime.Now.Year;
            if (month9 <= 0)
            {
                month9 += 12;
                year9 -= 1;
            }
            statisticData.feild9 = GetRevenueInMonthByStaffId(month9, year9, staffId);

            int month10 = DateTime.Now.Month - 10;
            int year10 = DateTime.Now.Year;
            if (month1 <= 0)
            {
                month10 += 12;
                year10 -= 1;
            }
            statisticData.feild10 = GetRevenueInMonthByStaffId(month10, year10, staffId);

            int month11 = DateTime.Now.Month - 11;
            int year11 = DateTime.Now.Year;
            if (month11 <= 0)
            {
                month11 += 12;
                year11 -= 1;
            }

            statisticData.feild11 = GetRevenueInMonthByStaffId(month11, year11, staffId);

            int month12 = DateTime.Now.Month - 12;
            int year12 = DateTime.Now.Year;
            if (month12 <= 0)
            {
                month12 += 12;
                year12 -= 1;
            }
            statisticData.feild12 = GetRevenueInMonthByStaffId(month12, year12, staffId);

            return statisticData;
        }

        public StatisticData GetLastYearStaffRevenue(int staffId)
        {
            int lastYear = DateTime.Now.Year - 1;
            StatisticData statisticData = new StatisticData();
            statisticData.feild1 = GetRevenueInMonthByStaffId(1, lastYear, staffId);
            statisticData.feild2 = GetRevenueInMonthByStaffId(2, lastYear, staffId);
            statisticData.feild3 = GetRevenueInMonthByStaffId(3, lastYear, staffId);
            statisticData.feild4 = GetRevenueInMonthByStaffId(4, lastYear, staffId);
            statisticData.feild5 = GetRevenueInMonthByStaffId(5, lastYear, staffId);
            statisticData.feild6 = GetRevenueInMonthByStaffId(6, lastYear, staffId);
            statisticData.feild7 = GetRevenueInMonthByStaffId(7, lastYear, staffId);
            statisticData.feild8 = GetRevenueInMonthByStaffId(8, lastYear, staffId);
            statisticData.feild9 = GetRevenueInMonthByStaffId(9, lastYear, staffId);
            statisticData.feild10 = GetRevenueInMonthByStaffId(10, lastYear, staffId);
            statisticData.feild11 = GetRevenueInMonthByStaffId(11, lastYear, staffId);
            statisticData.feild12 = GetRevenueInMonthByStaffId(12, lastYear, staffId);
            return statisticData;
        }
        public Dictionary<string, double> GetYearsRevenueByStaffId(int from, int to, int staffId)
        {
            Dictionary<string, double> list = new Dictionary<string, double>();

            for (int i = from; i <= to; i++)
            {
                var li = db.Payments.Where(p => p.PaymentTime.Year == i && p.Case.Case_Staff.Any(cs => cs.StaffId == staffId)).ToList();
                double result = 0;
                foreach (var item in li)
                {
                    result += item.PaymentMoney;
                }
                list.Add(i.ToString(), result);
            }
            return list;
        }

        public Dictionary<string, int> GetFromYearStaffRevenue()
        {
            Dictionary<string, int> list = new Dictionary<string, int>();
            var payment = db.Payments.OrderBy(p => p.PaymentTime).FirstOrDefault();
            int from = payment.PaymentTime.Year;
            int to = DateTime.Now.Year - 2;
            for (int i = from; i <= to; i++)
            {
                list.Add(i.ToString(), i);
            }
            return list;
        }
        public Dictionary<string, int> GetToYearStaffRevenue(int year)
        {
            Dictionary<string, int> list = new Dictionary<string, int>();
            int from = year + 1;
            int to = DateTime.Now.Year - 1;
            int to2 = from + 10;
            if (to > to2)
            {
                to = to2;
            }
            for (int i = from; i <= to; i++)
            {
                list.Add(i.ToString(), i);
            }
            return list;
        }

        public List<Object> GetAllSelectStaff()
        {
            var staffList = db.Staffs.ToList();
            var list = new List<object>();
            var level1 = new List<string>();
            level1.Add("Accounts");
            var except = new List<string>();
            except.Add("Password");
            foreach (var staff in staffList)
            {
                var result = UtilityClass.ConvertDynamicObjectWithCustomAttr(staff, except, level1);
                list.Add(result);
            }
            return list;
        }
    }
}