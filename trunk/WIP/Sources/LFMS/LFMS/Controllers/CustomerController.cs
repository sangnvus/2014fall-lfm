using System.Collections;
using LFMS.Models.BLO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LFMS.Utilities;
using System.IO;

namespace LFMS.Controllers
{
    public class CustomerController : AdminController
    {
        CustomerBLO customerBLO = new CustomerBLO();
        //
        // GET: /Customer/
        public ActionResult Customer()
        {
            return View();
        }

        public JsonResult GetAllCustomerJson()
        {
            int displayNum = int.Parse(Request.Params["displayNum"]);
            int orderKey = int.Parse(Request.Params["orderKey"]);
            string orderType = Request.Params["orderType"];
            string code = Request.Params["code"];
            int pageNum = int.Parse(Request.Params["pageNum"]);
            var list = customerBLO.GetPagingCustomerJson(displayNum, orderKey, orderType, code, pageNum);
            return Json(new { html = RenderPartialViewToString("_Customer", null), list }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPagingCustomerJson()
        {
            int displayNum = int.Parse(Request.Params["displayNum"]);
            int orderKey = int.Parse(Request.Params["orderKey"]);
            string orderType = Request.Params["orderType"];
            string code = Request.Params["code"];
            int pageNum = int.Parse(Request.Params["pageNum"]);


            return Json(customerBLO.GetPagingCustomerJson(displayNum, orderKey, orderType, code, pageNum));

        }

        [HttpPost]
        public String AddCustomer()
        {
            string taxCode = Request.Params["taxCode"].Trim();
            string name = Request.Params["cusname"].Trim();
            int selectCustomerGroup;
            Int32.TryParse(Request.Params["selectCustomerGroup"], out selectCustomerGroup);
            string represent = Request.Params["represent"].Trim();
            string sex = Request.Params["sex"];
            string birthDay = Request.Params["birthDay"].Trim();
            string identityNum = Request.Params["identityNum"].Trim();
            string identityDate = Request.Params["identityDate"].Trim();
            string identityPlace = Request.Params["identityPlace"].Trim();
            string bankAccount = Request.Params["bankAccount"].Trim();
            string bankBranch = Request.Params["bankBranch"].Trim();
            string address = Request.Params["address"].Trim();
            string mobile = Request.Params["mobile"].Trim();
            string telephone = Request.Params["telephone"].Trim();
            string email = Request.Params["email"].Trim();

            int cusId = customerBLO.AddCustomer(taxCode, name, selectCustomerGroup, represent, sex, birthDay, identityNum, identityDate, identityPlace, bankAccount, bankBranch, address, mobile, telephone, email);
            if (cusId != 0)
            {
                return "success";
            }
            return "fail";
            
        }

        [HttpPost]
        public String UpdateCustomer()
        {
            int id;
            Int32.TryParse(Request.Params["id"], out id);
            string taxCode = Request.Params["taxCode"].Trim();
            string name = Request.Params["name"].Trim();
            int selectCustomerGroup;
            Int32.TryParse(Request.Params["selectCustomerGroup"], out selectCustomerGroup);
            string represent = Request.Params["represent"].Trim();
            string sex = Request.Params["sex"];
            string birthDay = Request.Params["birthDay"].Trim();
            string identityNum = Request.Params["identityNum"].Trim();
            string identityDate = Request.Params["identityDate"].Trim();
            string identityPlace = Request.Params["identityPlace"].Trim();
            string bankAccount = Request.Params["bankAccount"].Trim();
            string bankBranch = Request.Params["bankBranch"].Trim();
            string address = Request.Params["address"].Trim();
            string mobile = Request.Params["mobile"].Trim();
            string telephone = Request.Params["telephone"].Trim();
            string email = Request.Params["email"].Trim();

            string result = customerBLO.UpdateCustomer(id, taxCode, name, selectCustomerGroup, represent, sex, birthDay, identityNum, identityDate, identityPlace, bankAccount, bankBranch, address, mobile, telephone, email);
            return result;
        }

        [HttpPost]
        public String DeleteCustomer()
        {
            int customerId;
            Int32.TryParse(Request.Params["customerId"], out customerId);
            string result = customerBLO.DeleteCustomer(customerId);
            return result;
        }

        [HttpPost]
        public JsonResult GetAllCustomerGroupJson()
        {
            var list = customerBLO.GetAllCustomerGroupJson();
            return Json(new { html = RenderPartialViewToString("_CustomerGroup", null), list }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public String AddCustomerGroup()
        {
            string name = Request.Params["groupName"];
            string description = Request.Params["description"];

            string result = customerBLO.AddCustomerGroup(name, description);
            return result;
        }

        [HttpPost]
        public String UpdateCustomerGroup()
        {
            int id;
            Int32.TryParse(Request.Params["groupId"], out id);
            string name = Request.Params["groupName"];
            string description = Request.Params["description"];

            string result = customerBLO.UpdateCustomerGroup(id, name, description);
            return result;
        }

        [HttpPost]
        public String DeleteCustomerGroup()
        {
            int groupId;
            Int32.TryParse(Request.Params["groupId"], out groupId);
            string result = customerBLO.DeleteCustomerGroup(groupId);
            return result;
        }

        private string RenderPartialViewToString(string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
            {
                viewName = this.ControllerContext.RouteData.GetRequiredString("action");
            }

            this.ViewData.Model = model;
            var sw = new StringWriter();
            ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(this.ControllerContext, viewName);
            var viewContext = new ViewContext(this.ControllerContext, viewResult.View, this.ViewData, this.TempData, sw);
            viewResult.View.Render(viewContext, sw);
            viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
            return sw.GetStringBuilder().ToString();
        }
    }
}
