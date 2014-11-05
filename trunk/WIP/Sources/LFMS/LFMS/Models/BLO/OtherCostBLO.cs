using System;
using LFMS.Models.DAO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Ajax.Utilities;
using LFMS.Utilities;


namespace LFMS.Models.BLO
{
    public class OtherCostBLO 
    {
        //
        // GET: /OtherCostBLO/
        private OtherCostDAO othercostDAO;
        public OtherCostBLO()
        {
            othercostDAO = new OtherCostDAO();
           
        }
 
        public List<Object> getAllOtherCost()
        {
            var otherCost = othercostDAO.getAllOtherCost();
            var level1 = new List<string>();
            level1.Add("Office");

            var list = new List<Object>();
            foreach (var cus in otherCost)
            {
                var result = UtilityClass.ConvertDynamicObjectWithFullAttr(cus, level1);
                list.Add(result);
            }

            return list;
        }
        public bool CheckDateImpress(string txtBenefitName)
        {
            return othercostDAO.CheckDateImpress(txtBenefitName);
        }
        public bool AddReward(int selectStaffReward, string dateStaffReward, double txtmoneyReward, string txtDescription)
        {
            return othercostDAO.AddReward(selectStaffReward, dateStaffReward, txtmoneyReward, txtDescription);
        }
        public bool UpdateReward(int txtRewardId, int txtEditName, string txtEditDate, double txtEditMoney, string editDescription)
        {
            return othercostDAO.UpdateReward(txtRewardId, txtEditName, txtEditDate, txtEditMoney, editDescription);
        }
        public bool deleteReward(int txtRewardId)
        {
            return othercostDAO.deleteReward(txtRewardId);
        }

    }
}
