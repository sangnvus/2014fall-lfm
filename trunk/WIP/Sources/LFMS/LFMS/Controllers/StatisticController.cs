using LFMS.Models.BLO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LFMS.Controllers
{
    public class StatisticController : AdminController
    {
        StatisticBLO statisticBLO = new StatisticBLO();
        //
        // GET: /Statistic/

        public ActionResult Statistic()
        {
            return View();
        }

        public JsonResult RenderRevenueHTML()
        {
            return Json(new { html = RenderPartialViewToString("_Revenue", null) }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult RenderOfficeHTML()
        {
            return Json(new { html = RenderPartialViewToString("_Office", null) }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult RenderStaffHTML()
        {
            return Json(new { html = RenderPartialViewToString("_Staff", null) }, JsonRequestBehavior.AllowGet);
        }
        //============================================================
        public JsonResult Get12MonthRevenue()
        {
            int officeId = int.Parse(Request.Params["officeId"]);
            var chartData = statisticBLO.Get12MonthRevenue(officeId);
            return Json(new { chartData }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Get12MonthRevenuePieChart()
        {
            Dictionary<string, double> chartData = new Dictionary<string, double>();
            var rev1 = statisticBLO.Get12MonthRevenue(1);
            var rev2 = statisticBLO.Get12MonthRevenue(2);
            var rev3 = statisticBLO.Get12MonthRevenue(3);
            var res1 = rev1.feild1 + rev1.feild2 + rev1.feild3 + rev1.feild4 + rev1.feild5 + rev1.feild6 + rev1.feild7 + rev1.feild8 + rev1.feild9 + rev1.feild10 + rev1.feild11 + rev1.feild12;
            var res2 = rev2.feild1 + rev2.feild2 + rev2.feild3 + rev2.feild4 + rev2.feild5 + rev2.feild6 + rev2.feild7 + rev2.feild8 + rev2.feild9 + rev2.feild10 + rev2.feild11 + rev2.feild12;
            var res3 = rev3.feild1 + rev3.feild2 + rev3.feild3 + rev3.feild4 + rev3.feild5 + rev3.feild6 + rev3.feild7 + rev3.feild8 + rev3.feild9 + rev3.feild10 + rev3.feild11 + rev3.feild12;
            if (res1 < 0) res1 = 0;
            if (res2 < 0) res2 = 0;
            if (res3 < 0) res3 = 0;
            chartData.Add("1", res1);
            chartData.Add("2", res2);
            chartData.Add("3", res3);
            return Json(new { chartData }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Get12MonthRevenueSS()
        {
            int officeId1 = int.Parse(Request.Params["officeId1"]);
            var chartData1 = statisticBLO.Get12MonthRevenue(officeId1);
            int officeId2 = int.Parse(Request.Params["officeId2"]);
            var chartData2 = statisticBLO.Get12MonthRevenue(officeId2);
            return Json(new { chartData1, chartData2 }, JsonRequestBehavior.AllowGet);
        }
        //-------------------------------------------------
        public JsonResult GetLastYearRevenue()
        {
            int officeId = int.Parse(Request.Params["officeId"]);
            var chartData = statisticBLO.GetLastYearRevenue(officeId);
            return Json(new { chartData }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetLastYearRevenuePieChart()
        {
            Dictionary<string, double> chartData = new Dictionary<string, double>();
            var rev1 = statisticBLO.GetLastYearRevenue(1);
            var rev2 = statisticBLO.GetLastYearRevenue(2);
            var rev3 = statisticBLO.GetLastYearRevenue(3);
            var res1 = rev1.feild1 + rev1.feild2 + rev1.feild3 + rev1.feild4 + rev1.feild5 + rev1.feild6 + rev1.feild7 + rev1.feild8 + rev1.feild9 + rev1.feild10 + rev1.feild11 + rev1.feild12;
            var res2 = rev2.feild1 + rev2.feild2 + rev2.feild3 + rev2.feild4 + rev2.feild5 + rev2.feild6 + rev2.feild7 + rev2.feild8 + rev2.feild9 + rev2.feild10 + rev2.feild11 + rev2.feild12;
            var res3 = rev3.feild1 + rev3.feild2 + rev3.feild3 + rev3.feild4 + rev3.feild5 + rev3.feild6 + rev3.feild7 + rev3.feild8 + rev3.feild9 + rev3.feild10 + rev3.feild11 + rev3.feild12;
            if (res1 < 0) res1 = 0;
            if (res2 < 0) res2 = 0;
            if (res3 < 0) res3 = 0;
            chartData.Add("1", res1);
            chartData.Add("2", res2);
            chartData.Add("3", res3);
            return Json(new { chartData }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetLastYearRevenueSS()
        {
            int officeId1 = int.Parse(Request.Params["officeId1"]);
            var chartData1 = statisticBLO.GetLastYearRevenue(officeId1);
            int officeId2 = int.Parse(Request.Params["officeId2"]);
            var chartData2 = statisticBLO.GetLastYearRevenue(officeId2);
            return Json(new { chartData1, chartData2 }, JsonRequestBehavior.AllowGet);
        }
        //---------------------------------------------
        public JsonResult GetYearsRevenue()
        {
            int from = int.Parse(Request.Params["from"]);
            int officeId = int.Parse(Request.Params["officeId"]);
            int to = int.Parse(Request.Params["to"]);
            Dictionary<string, double> chartData = statisticBLO.GetYearsRevenue(officeId, from, to);
            JsonResult result = Json(chartData, JsonRequestBehavior.AllowGet);
            return result;
        }

        public JsonResult GetYearsRevenuePieChart()
        {
            int from = int.Parse(Request.Params["from"]);
            int to = int.Parse(Request.Params["to"]);
            Dictionary<string, double> revenue1 = statisticBLO.GetYearsRevenue(1, from, to);
            double res1 = 0;
            foreach (var item in revenue1)
            {
                res1 += item.Value;
            }
            Dictionary<string, double> revenue2 = statisticBLO.GetYearsRevenue(2, from, to);
            double res2 = 0;
            foreach (var item in revenue2)
            {
                res2 += item.Value;
            }
            Dictionary<string, double> revenue3 = statisticBLO.GetYearsRevenue(3, from, to);
            double res3 = 0;
            foreach (var item in revenue3)
            {
                res3 += item.Value;
            }

            Dictionary<string, double> chartData = new Dictionary<string, double>();
            if (res1 < 0) res1 = 0;
            if (res2 < 0) res2 = 0;
            if (res3 < 0) res3 = 0;
            chartData.Add("1", res1);
            chartData.Add("2", res2);
            chartData.Add("3", res3);
            return Json(new { chartData }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetYearsRevenueSS()
        {
            int officeId1 = int.Parse(Request.Params["officeId1"]);
            int officeId2 = int.Parse(Request.Params["officeId2"]);
            int from = int.Parse(Request.Params["from"]);
            int to = int.Parse(Request.Params["to"]);
            Dictionary<string, double> chartData1 = statisticBLO.GetYearsRevenue(officeId1, from, to);
            Dictionary<string, double> chartData2 = statisticBLO.GetYearsRevenue(officeId2, from, to);
            return Json(new { chartData1, chartData2 }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetFromYearRevenue()
        {
            var year = statisticBLO.GetFromYearRevenue();
            return Json(new { year }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetToYearRevenue()
        {
            int y = int.Parse(Request.Params["year"]);
            var year = statisticBLO.GetToYearRevenue(y);
            return Json(new { year }, JsonRequestBehavior.AllowGet);
        }

        //============================================================
        public JsonResult Get12MonthStaffRevenue()
        {
            int staffId = int.Parse(Request.Params["staffId"]);
            var chartData = statisticBLO.Get12MonthStaffRevenue(staffId);
            return Json(new { chartData }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Get12MonthStaffSS()
        {
            int staffId1 = int.Parse(Request.Params["staffId1"]);
            var chartData1 = statisticBLO.Get12MonthStaffRevenue(staffId1);
            int staffId2 = int.Parse(Request.Params["staffId2"]);
            var chartData2 = statisticBLO.Get12MonthStaffRevenue(staffId2);
            return Json(new { chartData1, chartData2 }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetLastYearStaffRevenue()
        {
            int staffId = int.Parse(Request.Params["staffId"]);
            var chartData = statisticBLO.GetLastYearStaffRevenue(staffId);
            return Json(new { chartData }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetLastYearStaffSS()
        {
            int staffId1 = int.Parse(Request.Params["staffId1"]);
            var chartData1 = statisticBLO.GetLastYearStaffRevenue(staffId1);
            int staffId2 = int.Parse(Request.Params["staffId2"]);
            var chartData2 = statisticBLO.GetLastYearStaffRevenue(staffId2);
            return Json(new { chartData1, chartData2 }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetYearsStaffRevenue()
        {
            int from = int.Parse(Request.Params["from"]);
            int staffId = int.Parse(Request.Params["staffId"]);
            int to = int.Parse(Request.Params["to"]);
            Dictionary<string, double> chartData = statisticBLO.GetYearsStaffRevenue(staffId, from, to);
            JsonResult result = Json(chartData, JsonRequestBehavior.AllowGet);
            return result;
        }
        public JsonResult GetYearsStaffRevenueSS()
        {
            int staffId1 = int.Parse(Request.Params["staffId1"]);
            int staffId2 = int.Parse(Request.Params["staffId2"]);
            int from = int.Parse(Request.Params["from"]);
            int to = int.Parse(Request.Params["to"]);
            Dictionary<string, double> chartData1 = statisticBLO.GetYearsStaffRevenue(staffId1, from, to);
            Dictionary<string, double> chartData2 = statisticBLO.GetYearsStaffRevenue(staffId2, from, to);
            return Json(new { chartData1, chartData2 }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetFromYearStaffRevenue()
        {
            var year = statisticBLO.GetFromYearStaffRevenue();
            return Json(new { year }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetToYearStaffRevenue()
        {
            int y = int.Parse(Request.Params["year"]);
            var year = statisticBLO.GetToYearStaffRevenue(y);
            return Json(new { year }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllSelectStaff()
        {
            var staff = statisticBLO.GetAllSelectStaff();
            return Json(new { staff }, JsonRequestBehavior.AllowGet);
        }
        //============================================================
        public JsonResult Get12MonthCase()
        {
            int officeId = int.Parse(Request.Params["officeId"]);
            var chartData = statisticBLO.Get12MonthCase(officeId);
            return Json(new { chartData }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Get12MonthCasePieChart()
        {
            Dictionary<string, double> chartData = new Dictionary<string, double>();
            var rev1 = statisticBLO.Get12MonthCase(1);
            var rev2 = statisticBLO.Get12MonthCase(2);
            var rev3 = statisticBLO.Get12MonthCase(3);
            var res1 = rev1.feild1 + rev1.feild2 + rev1.feild3 + rev1.feild4 + rev1.feild5 + rev1.feild6 + rev1.feild7 + rev1.feild8 + rev1.feild9 + rev1.feild10 + rev1.feild11 + rev1.feild12;
            var res2 = rev2.feild1 + rev2.feild2 + rev2.feild3 + rev2.feild4 + rev2.feild5 + rev2.feild6 + rev2.feild7 + rev2.feild8 + rev2.feild9 + rev2.feild10 + rev2.feild11 + rev2.feild12;
            var res3 = rev3.feild1 + rev3.feild2 + rev3.feild3 + rev3.feild4 + rev3.feild5 + rev3.feild6 + rev3.feild7 + rev3.feild8 + rev3.feild9 + rev3.feild10 + rev3.feild11 + rev3.feild12;
            if (res1 < 0) res1 = 0;
            if (res2 < 0) res2 = 0;
            if (res3 < 0) res3 = 0;
            chartData.Add("1", res1);
            chartData.Add("2", res2);
            chartData.Add("3", res3);
            return Json(new { chartData }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Get12MonthCaseSS()
        {
            int officeId1 = int.Parse(Request.Params["officeId1"]);
            var chartData1 = statisticBLO.Get12MonthCase(officeId1);
            int officeId2 = int.Parse(Request.Params["officeId2"]);
            var chartData2 = statisticBLO.Get12MonthCase(officeId2);
            return Json(new { chartData1, chartData2 }, JsonRequestBehavior.AllowGet);
        }
        //----------------------------------------------------------------------
        public JsonResult GetLastYearCase()
        {
            int officeId = int.Parse(Request.Params["officeId"]);
            var chartData = statisticBLO.GetLastYearCase(officeId);
            return Json(new { chartData }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetLastYearCasePieChart()
        {
            Dictionary<string, double> chartData = new Dictionary<string, double>();
            var rev1 = statisticBLO.GetLastYearCase(1);
            var rev2 = statisticBLO.GetLastYearCase(2);
            var rev3 = statisticBLO.GetLastYearCase(3);
            var res1 = rev1.feild1 + rev1.feild2 + rev1.feild3 + rev1.feild4 + rev1.feild5 + rev1.feild6 + rev1.feild7 + rev1.feild8 + rev1.feild9 + rev1.feild10 + rev1.feild11 + rev1.feild12;
            var res2 = rev2.feild1 + rev2.feild2 + rev2.feild3 + rev2.feild4 + rev2.feild5 + rev2.feild6 + rev2.feild7 + rev2.feild8 + rev2.feild9 + rev2.feild10 + rev2.feild11 + rev2.feild12;
            var res3 = rev3.feild1 + rev3.feild2 + rev3.feild3 + rev3.feild4 + rev3.feild5 + rev3.feild6 + rev3.feild7 + rev3.feild8 + rev3.feild9 + rev3.feild10 + rev3.feild11 + rev3.feild12;
            if (res1 < 0) res1 = 0;
            if (res2 < 0) res2 = 0;
            if (res3 < 0) res3 = 0;
            chartData.Add("1", res1);
            chartData.Add("2", res2);
            chartData.Add("3", res3);
            return Json(new { chartData }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetLastYearCaseSS()
        {
            int officeId1 = int.Parse(Request.Params["officeId1"]);
            var chartData1 = statisticBLO.GetLastYearCase(officeId1);
            int officeId2 = int.Parse(Request.Params["officeId2"]);
            var chartData2 = statisticBLO.GetLastYearCase(officeId2);
            return Json(new { chartData1, chartData2 }, JsonRequestBehavior.AllowGet);
        }
        //--------------------------------------------
        public JsonResult GetYearsCase()
        {
            int from = int.Parse(Request.Params["from"]);
            int officeId = int.Parse(Request.Params["officeId"]);
            int to = int.Parse(Request.Params["to"]);
            Dictionary<string, double> chartData = statisticBLO.GetYearsCase(officeId, from, to);
            JsonResult result = Json(chartData, JsonRequestBehavior.AllowGet);
            return result;
        }
        public JsonResult GetYearsCasePieChart()
        {
            int from = int.Parse(Request.Params["from"]);
            int to = int.Parse(Request.Params["to"]);
            Dictionary<string, double> revenue1 = statisticBLO.GetYearsCase(1, from, to);
            double res1 = 0;
            foreach (var item in revenue1)
            {
                res1 += item.Value;
            }
            Dictionary<string, double> revenue2 = statisticBLO.GetYearsCase(2, from, to);
            double res2 = 0;
            foreach (var item in revenue2)
            {
                res2 += item.Value;
            }
            Dictionary<string, double> revenue3 = statisticBLO.GetYearsCase(3, from, to);
            double res3 = 0;
            foreach (var item in revenue3)
            {
                res3 += item.Value;
            }

            Dictionary<string, double> chartData = new Dictionary<string, double>();
            if (res1 < 0) res1 = 0;
            if (res2 < 0) res2 = 0;
            if (res3 < 0) res3 = 0;
            chartData.Add("1", res1);
            chartData.Add("2", res2);
            chartData.Add("3", res3);
            return Json(new { chartData }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetYearsCaseSS()
        {
            int officeId1 = int.Parse(Request.Params["officeId1"]);
            int officeId2 = int.Parse(Request.Params["officeId2"]);
            int from = int.Parse(Request.Params["from"]);
            int to = int.Parse(Request.Params["to"]);
            Dictionary<string, double> chartData1 = statisticBLO.GetYearsCase(officeId1, from, to);
            Dictionary<string, double> chartData2 = statisticBLO.GetYearsCase(officeId2, from, to);
            return Json(new { chartData1, chartData2 }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetFromYearCase()
        {
            var year = statisticBLO.GetFromYearCase();
            return Json(new { year }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetToYearCase()
        {
            int y = int.Parse(Request.Params["year"]);
            var year = statisticBLO.GetToYearCase(y);
            return Json(new { year }, JsonRequestBehavior.AllowGet);
        }

        //============================================================
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

        //public String ExportExcel()
        //{
        //    var resultExport = string.Empty;
        //    try
        //    {
        //        IList<object> listStatisticResult = new List<object>();
        //        var listStatistic = statisticBLO.Get12MonthRevenue(0);
        //        object[] headObject = { listStatistic };
        //        listStatisticResult.Add(listStatistic);
        //        string excelTemplatePath = HttpContext.Server.MapPath("~/ExcelReport/StatisticTemplate.xlsx");
        //        string xmlTemplatePath = HttpContext.Server.MapPath("~/ExcelReport/StatisticTemplate.xml");
        //        string savePath = HttpContext.Server.MapPath("~/ExcelReport/ExportStatistic (" + DateTime.Now.ToString("HH-mm-ss tt") + ")" + ".xlsx");
        //        using (ExcelBLO excel = new ExcelBLO(TemplatePattern.FULL, xmlTemplatePath, excelTemplatePath, "Salary"))
        //        {
        //            excel.ExportObjectsToFullArea(savePath, listStatisticResult, headObject);
        //            resultExport = "Successful";
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        resultExport = "Error";
        //    }
        //    return resultExport;
        //}
    }
}
