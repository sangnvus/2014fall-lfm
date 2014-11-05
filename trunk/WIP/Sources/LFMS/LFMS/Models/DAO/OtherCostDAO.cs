using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LFMS.Models.DAO
{
    public class OtherCostDAO
    {
        //
        // GET: /OtherCostDAO/
        LFMSEntities db;
        public OtherCostDAO()
        {
            db = new LFMSEntities();
        }
        public List<OtherCost> getAllOtherCost()
        {
            var cost = db.OtherCosts.ToList();
            return cost;
        }
        public bool CheckDateImpress(string strDateTime)
        {
            //var dateValue = DateTime.Parse(strDateTime);

            var dateValue = DateTime.ParseExact(strDateTime, "dd/MM/yyyy", null);
            var impressMonth = dateValue.Month;

            var currentMonth = DateTime.Now.Month;

            if (impressMonth == currentMonth)
            {
                return false;
            }
            return true;
        }
        public bool AddReward(int selectStaffReward, string dateStaffReward, double txtmoneyReward, string txtDescription)
        {
            OtherCost reward = new OtherCost();

            reward.OfficeId = selectStaffReward;

            DateTime dateStaffImpressPR = DateTime.ParseExact(dateStaffReward, "dd/MM/yyyy", null);
            reward.Date = dateStaffImpressPR;
            reward.Cost = txtmoneyReward;
            reward.Description = txtDescription;
            try
            {
                db.OtherCosts.Add(reward);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        public bool UpdateReward(int txtRewardId, int txtEditName, string txtEditDate, double txtEditMoney, string editDescription)
        {
            OtherCost reward = GetOtherCostById(txtRewardId);
            if (reward == null)
            {
                return false;
            }
            else
            {
                try
                {
                    reward.OfficeId = txtEditName;

                    DateTime editDate = DateTime.ParseExact(txtEditDate, "dd/MM/yyyy", null);
                    reward.Date = editDate;

                    reward.Cost = txtEditMoney;

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
        public OtherCost GetOtherCostById(int otherCostId)
        {
            var reward = db.OtherCosts.Where(c => c.Id == otherCostId).FirstOrDefault();
            return reward;
        }
        public bool deleteReward(int txtRewardId)
        {
            OtherCost reward = GetOtherCostById(txtRewardId);
            if (reward == null)
            {
                return false;
            }
            else
            {
                try
                {
                    db.OtherCosts.Remove(reward);
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
    }
}
