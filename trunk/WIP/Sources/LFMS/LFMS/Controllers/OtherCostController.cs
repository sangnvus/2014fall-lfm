using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LFMS.Models.BLO;
using LFMS.Utilities;

namespace LFMS.Controllers
{
    public class OtherCostController : AdminController
    {
        //
        // GET: /OtherCost/
        OtherCostBLO otherCostBLO = new OtherCostBLO();

        public ActionResult OtherCost()
        {
            return View();
        }
        public JsonResult getAllOtherCost()
        {
            var list = otherCostBLO.getAllOtherCost();
            return Json(list, JsonRequestBehavior.AllowGet);

        }

        public string AddReward()
        {
            int selectOffice = int.Parse(Request.Params["selectOffice"]);
            string datePay = Request.Params["datePay"];


            double txtMoney;
            double.TryParse(Request.Params["txtMoney"], out txtMoney);
            string txtDescription = Request.Params["txtDescription"];
            if (!otherCostBLO.CheckDateImpress(datePay))
            {
                bool result = otherCostBLO.AddReward(selectOffice, datePay, txtMoney, txtDescription);
                if (result == false)
                {
                    return "Error";
                }
                return "Successful";
            }
            else return "dateinValide";
        }
        public String UpdateReward()
        {
            int txtRewardId;
            Int32.TryParse(Request.Params["txtRewardId"], out txtRewardId);

            int txtEditName;
            Int32.TryParse(Request.Params["txtEditName"], out txtEditName);

            string txtEditDate = Request.Params["txtEditDate"];

            double txtEditMoney;
            double.TryParse(Request.Params["txtEditMoney"], out txtEditMoney);

            string editDescription = Request.Params["editDescription"];

            if (!otherCostBLO.CheckDateImpress(txtEditDate))
            {
                bool result = otherCostBLO.UpdateReward(txtRewardId, txtEditName, txtEditDate, txtEditMoney, editDescription);
                if (result == false)
                {
                    return "Error";
                }
                return "Successful";

            }
            else return "dateinValide";
        }
        public String deleteReward()
        {
            int txtRewardId;
            Int32.TryParse(Request.Params["txtRewardId"], out txtRewardId);
            bool result = otherCostBLO.deleteReward(txtRewardId);
            if (result == true)
            {
                return "Successful";
            }
            return "Error";
        }

    }
}
