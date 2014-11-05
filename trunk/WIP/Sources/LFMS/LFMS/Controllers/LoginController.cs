using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using LFMS.Models.BLO;

namespace LFMS.Controllers
{
    public class LoginController : Controller
    {
        UserBLO userBLO = new UserBLO();

        public ActionResult Login()
        {
            return View();
        }

        public String CheckUser(string username, string password)
        {
            var acc = userBLO.GetUser(username, password);
            if (acc != null && (bool) acc.Staff.Active)
            {
                Session["StaffId"] = acc.StaffId;
                Session["StaffName"] = acc.Staff.StaffName ;
                Session["Username"] = acc.Staff.Accounts.FirstOrDefault().Username;

                Session["Avatar"] = acc.Staff.Avatar;
                return "susscess";
            }
            if (acc != null && !((bool) acc.Staff.Active))
            {
                return "inactive";
            }
            return "fail";
        }

        public ActionResult PermissionError()
        {
            return View();
        }

        public string LogOut()
        {
            try
            {

                if (Session["StaffId"] != null) Session.Remove("StaffId");
                if (Session["UserAuthorize"] != null) Session.Remove("UserAuthorize");
                if (Session["RoleId"] != null) Session.Remove("RoleId");
                return "success";
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }
    }

    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //Check if a specific value of your Session is null
            if (Session["StaffId"] == null)
            {
                //If so - Redirect to your Login Action
                filterContext.Result = new RedirectResult(Url.Action("Login", "Login"));
            }
            else if (int.Parse(Session["StaffId"].ToString()) > 0)
            {
                var staffId = int.Parse(Session["StaffId"].ToString());
                UserBLO userBLO = new UserBLO();
                var authorize = userBLO.GetAuthorize(staffId);
                Session["UserAuthorize"] = authorize;
                Session["RoleId"] = userBLO.GetRole(staffId);
            }
        }
    }

    public class AdminController : BaseController
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //Check if a specific value of your Session is null
            if (Session["StaffId"] == null)
            {
                //If so - Redirect to your Login Action
                filterContext.Result = new RedirectResult(Url.Action("Login", "Login"));
            }
            else if (int.Parse(Session["StaffId"].ToString()) > 0 && int.Parse(Session["RoleId"].ToString()) != 1)
            {
                filterContext.Result = new RedirectResult(Url.Action("PermissionError", "Login"));
            }
        }
    }
}

