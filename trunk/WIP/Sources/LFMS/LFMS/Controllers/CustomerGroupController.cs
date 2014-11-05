using LFMS.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LFMS.Controllers
{
    public class CustomerGroupController : Controller
    {
        //
        // GET: /CustomerGroup/

        public JsonResult GetAllCustomerGroup()
        {

            var db = new LFMSEntities();

            var customerGroup = db.CustomerGroups.ToList();
            var level1 = new List<string>();
            level1.Add("Customer");

            var list = new List<object>();
            foreach (var cus in customerGroup)
            {
                var result = UtilityClass.ConvertDynamicObjectWithFullAttr(cus, level1);
                list.Add(result);
            }

            return Json(list, JsonRequestBehavior.AllowGet);
        }

    }
}
