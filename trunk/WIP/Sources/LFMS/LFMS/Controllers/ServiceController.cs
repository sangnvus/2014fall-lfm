using LFMS.Models.BLO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LFMS.Utilities;
using Microsoft.Ajax.Utilities;
using System.Web.Providers.Entities;
using System.IO;

namespace LFMS.Controllers
{
    public class ServiceController : AdminController
    {
        private ServiceBLO serviceBLO = new ServiceBLO();
        //
        // GET: /Service/

        public ActionResult Service()
        {
            return View();
        }

        public JsonResult GetAllServiceJson()
        {

            List<Object> list = serviceBLO.GetAllServiceJson();
            //return Json(list, JsonRequestBehavior.DenyGet);
            return Json(new { html = RenderPartialViewToString("_Service", null), list }, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public String AddService()
        {
            string name = Request.Params["serviceName"];
            string description = Request.Params["description"];
            int typeId;
            Int32.TryParse(Request.Params["selectServiceType"], out typeId);

            string result = serviceBLO.AddService(name, description, typeId);
            return result;
        }

        [HttpPost]
        public String UpdateService()
        {
            int id;
            Int32.TryParse(Request.Params["serviceId"], out id);
            string name = Request.Params["serviceName"];
            string description = Request.Params["description"];
            int typeId;
            Int32.TryParse(Request.Params["selectServiceType"], out typeId);

            string result = serviceBLO.UpdateService(id, name, description, typeId);
            return result;
        }

        [HttpPost]
        public String DeleteService()
        {
            int id;
            Int32.TryParse(Request.Params["serviceId"], out id);
            string result = serviceBLO.DeleteService(id);
            return result;
        }


        public JsonResult GetAllServiceTypeJson()
        {

            List<Object> list = serviceBLO.GetAllServiceTypeJson();
            //return Json(list, JsonRequestBehavior.DenyGet);
            return Json(new { html = RenderPartialViewToString("_ServiceType", null), list }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public String AddServiceType()
        {
            string name = Request.Params["typeName"];
            string description = Request.Params["description"];
            int typeId;
            Int32.TryParse(Request.Params["typeId"], out typeId);

            string result = serviceBLO.AddServiceType(name, description);
            return result;
        }

        [HttpPost]
        public String UpdateServiceType()
        {
            int id;
            Int32.TryParse(Request.Params["typeId"], out id);
            string name = Request.Params["typeName"];
            string description = Request.Params["description"];

            string result = serviceBLO.UpdateServiceType(id, name, description);
            return result;
        }

        [HttpPost]
        public String DeleteServiceType()
        {
            int id;
            Int32.TryParse(Request.Params["typeId"], out id);
            string result = serviceBLO.DeleteServiceType(id);
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
