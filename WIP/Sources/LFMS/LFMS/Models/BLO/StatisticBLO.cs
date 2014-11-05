using LFMS.Models.DAO;
using LFMS.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LFMS.Utilities;

namespace LFMS.Models.BLO
{
    public class StatisticBLO
    {
        private StatisticDAO statisticDAO;

        public StatisticBLO()
        {
            statisticDAO = new StatisticDAO();
        }

        public StatisticData Get12MonthRevenue(int officeId)
        {
            if (officeId == 0)
            {
                return statisticDAO.Get12MonthRevenue();
            }
            return statisticDAO.Get12MonthRevenueByOfficeId(officeId);
        }

        public StatisticData GetLastYearRevenue(int officeId)
        {
            if (officeId == 0)
            {
                return statisticDAO.GetLastYearRevenue();
            }
            return statisticDAO.GetLastYearRevenueByOfficeId(officeId);
        }
        public Dictionary<string, double> GetYearsRevenue(int officeId, int from, int to)
        {
            if(officeId == 0)
            {
                return statisticDAO.GetYearsRevenue(from, to);
            }
            return statisticDAO.GetYearsRevenueByOfficeId(from, to, officeId);
        }
        public Dictionary<string, int> GetFromYearRevenue()
        {
            return statisticDAO.GetFromYearRevenue();
        }
        public Dictionary<string, int> GetToYearRevenue(int year)
        {
            return statisticDAO.GetToYearRevenue(year);
        }

        //==============================================================================
        public StatisticData Get12MonthCase(int officeId)
        {
            if (officeId == 0)
            {
                return statisticDAO.Get12MonthCase();
            }
            return statisticDAO.Get12MonthCaseByOfficeId(officeId);
        }
        public StatisticData GetLastYearCase(int officeId)
        {
            if (officeId == 0)
            {
                return statisticDAO.GetLastYearCase();
            }
            return statisticDAO.GetLastYearCaseByOfficeId(officeId);
        }

        public Dictionary<string, double> GetYearsCase(int officeId, int from, int to)
        {
            if (officeId == 0)
            {
                return statisticDAO.GetYearsCase(from, to);
            }
            return statisticDAO.GetYearsCaseByOfficeId(from, to, officeId);
        }
        public Dictionary<string, int> GetFromYearCase()
        {
            return statisticDAO.GetFromYearCase();
        }
        public Dictionary<string, int> GetToYearCase(int year)
        {
            return statisticDAO.GetToYearCase(year);
        }
        //==========================================================================
        public StatisticData Get12MonthStaffRevenue(int staffId)
        {
            return statisticDAO.Get12MonthStaffRevenue(staffId);
        }

        public StatisticData GetLastYearStaffRevenue(int staffId)
        {
            return statisticDAO.GetLastYearStaffRevenue(staffId);
        }
        public Dictionary<string, double> GetYearsStaffRevenue(int staffId, int from, int to)
        {
            return statisticDAO.GetYearsRevenueByStaffId(from, to, staffId);
        }
        public Dictionary<string, int> GetFromYearStaffRevenue()
        {
            return statisticDAO.GetFromYearStaffRevenue();
        }
        public Dictionary<string, int> GetToYearStaffRevenue(int year)
        {
            return statisticDAO.GetToYearStaffRevenue(year);
        }

        public List<Object> GetAllSelectStaff()
        {
            return statisticDAO.GetAllSelectStaff();
        }
    }
}