using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using LFMS.Models.BLO;

namespace LFMS.Controllers
{
    public class ProxyController : BaseController
    {

        public static CookieContainer CookieContainer = new CookieContainer();
        public bool LoginWeb()
        {
            try
            {

                string url = "http://thuvienphapluat.vn";

                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);

                req.Method = HttpContext.Request.HttpMethod;
                req.ContentLength = 0;
                req.CookieContainer = CookieContainer;
                req.KeepAlive = true;
                req.Timeout = 10000;
                HttpWebResponse response = (HttpWebResponse)req.GetResponse();
                System.IO.Stream responseStream = response.GetResponseStream();
                System.IO.StreamReader reader = new System.IO.StreamReader(responseStream, Encoding.UTF8);
                string srcString = reader.ReadToEnd();
                response.Close();
                if (srcString.IndexOf("Thông tin cá nhân") != -1) return true;
                string viewState = GetStringBetween("id=\"__VIEWSTATE\" value=\"", "\"", srcString);
                string eventValidation = GetStringBetween("id=\"__EVENTVALIDATION\" value=\"", "\"", srcString);
                eventValidation = HttpUtility.UrlEncode(eventValidation);
                viewState = HttpUtility.UrlEncode(viewState);

                req = (HttpWebRequest)HttpWebRequest.Create(url);
                req.Method = "POST";
                string data = "__VIEWSTATE=" + viewState + "&__EVENTVALIDATION=" + eventValidation + "&serch=&serch=&serch=&serch=&serch=&__EVENTTARGET=&__EVENTARGUMENT=";
                data += "&Support%24usernameTextBox=vplsthuannguyen&Support%24passwordTextBox=0982098008&Support%24loginButton=%C4%90%C4%83ng+nh%E1%BA%ADp";
                byte[] buffer = Encoding.UTF8.GetBytes(data);
                req.ContentType = "application/x-www-form-urlencoded";
                req.KeepAlive = true;
                req.Timeout = 10000;
                req.CookieContainer = CookieContainer;
                req.CookieContainer.Add(response.Cookies);

                req.ContentLength = buffer.Length;
                Stream reqst = req.GetRequestStream();
                reqst.Write(buffer, 0, buffer.Length);
                reqst.Close();

                HttpWebResponse res = (HttpWebResponse)req.GetResponse();
                StreamReader sr = new StreamReader(res.GetResponseStream(), Encoding.UTF8);
                string source = sr.ReadToEnd();
                res.Close();
                if (source.IndexOf("Thông tin cá nhân") != -1) return true;
                return false;

            }
            catch (Exception)
            {
                return false;
            }
        }

        public string SearchDoc()
        {
            try
            {
                if (LoginWeb())
                {
                    string keyword = Request.Params["keyword"].Trim();
                    string area = Request.Params["area"].Trim();
                    string type = Request.Params["type"].Trim();
                    string status = Request.Params["status"].Trim();
                    string lan = Request.Params["lan"].Trim();
                    string org = Request.Params["org"].Trim();
                    string signer = Request.Params["signer"];
                    string match = Request.Params["match"].Trim();
                    string sort = Request.Params["sort"].Trim();
                    string bdate = Request.Params["bdate"].Trim();
                    string edate = Request.Params["edate"].Trim();

                    string url = "http://thuvienphapluat.vn/phap-luat/tim-van-ban.aspx?keyword=" + keyword + "&area=" + area +
                                 "&type=" + type + "&status=" + status + "&lan=" + lan + "&org=" + org + "&signer=" + signer + "&match=" + match +
                                 "&sort=" + sort + "&bdate=" + bdate + "&edate=" + edate;


                    string content = "";
                    HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                    req.KeepAlive = true;
                    req.Method = "GET";
                    req.Timeout = 10000;
                    req.CookieContainer = CookieContainer;
                    HttpWebResponse res = (HttpWebResponse)req.GetResponse();
                    if (res.StatusCode.ToString().ToLower() == "ok")
                    {
                        string contentType = res.ContentType;
                        Stream resst = res.GetResponseStream();
                        StreamReader sr = new StreamReader(resst);
                        Response.ContentType = contentType;
                        content = sr.ReadToEnd();
                        res.Close();
                    }
                    content = GetStringBetween("<td valign=\"top\" width=\"760px\"", "</td>", content);
                    int cut = content.IndexOf("<p class=\"info-top\">Chú thích</p>");
                    content = content.Substring(0, cut);
                    content = "<td valign=\"top\" width=\"100%\"" + content + "</td>";
                    content = content.Replace("/images/icon-rss.gif", "/Content/css/images/icon-rss.gif");
                    return content;

                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
            }
            return ("<div style=\"text-align: center;\"><br/><b>Không thể login vào thuvienphapluat.vn, hãy kiểm tra kết nối mạng!</b></div>");
        }

        public string SearchFieldAndPage()
        {
            try
            {
                if (LoginWeb())
                {
                    string searchStr = Request.Params["searchString"].Trim();

                    string url = "http://thuvienphapluat.vn/phap-luat/tim-van-ban.aspx?" + searchStr;


                    string content = "";
                    HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                    req.KeepAlive = true;
                    req.Method = "GET";
                    req.Timeout = 10000;
                    req.CookieContainer = CookieContainer;
                    HttpWebResponse res = (HttpWebResponse)req.GetResponse();
                    if (res.StatusCode.ToString().ToLower() == "ok")
                    {
                        string contentType = res.ContentType;
                        Stream resst = res.GetResponseStream();
                        StreamReader sr = new StreamReader(resst);
                        Response.ContentType = contentType;
                        content = sr.ReadToEnd();
                        res.Close();
                    }
                    content = GetStringBetween("<td valign=\"top\" width=\"760px\"", "</td>", content);
                    int cut = content.IndexOf("<p class=\"info-top\">Chú thích</p>");
                    content = content.Substring(0, cut);
                    content = "<td valign=\"top\" width=\"100%\"" + content + "</td>";
                    content = content.Replace("/images/icon-rss.gif", "/Content/css/images/icon-rss.gif");
                    return content;

                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
            }
            return ("<div style=\"text-align: center;\"><br/><b>Không thể login vào thuvienphapluat.vn, hãy kiểm tra kết nối mạng!</b></div>");
        }

        public string ReviewDoc(string url)
        {
            try
            {
                if (LoginWeb())
                {
                    string content = "";
                    HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                    req.KeepAlive = true;
                    req.Method = "GET";
                    req.Timeout = 10000;
                    req.CookieContainer = CookieContainer;
                    HttpWebResponse res = (HttpWebResponse)req.GetResponse();
                    if (res.StatusCode.ToString().ToLower() == "ok")
                    {
                        string contentType = res.ContentType;
                        Stream resst = res.GetResponseStream();
                        StreamReader sr = new StreamReader(resst);
                        Response.ContentType = contentType;

                        content = sr.ReadToEnd();
                        res.Close();
                    }
                    content = GetStringBetween("<div class=\"posttext\"", "<div id=\"bttop\"", content);

                    return "<div class=\"posttext\"" + content;

                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
            }
            return ("LoginError");
        }


        public string DownloadFile(string url, string caseCode)
        {
            try
            {
                CaseBLO caseBLO = new CaseBLO();
                Case cases = caseBLO.GetCaseByCaseCode(caseCode);
                var status = "";
                var caseid = cases.CaseId;
                if (cases != null)
                {
                    status = cases.Status;
                }
                if ("Đã thụ lý".Equals(status))
                {
                    return ("DoneCase");
                }
                var authorList = (List<string>)Session["UserAuthorize"];
                if (authorList.IndexOf(caseid.ToString()) == -1 && Session["RoleId"].ToString()!="1")
                {
                    return ("PermissionError");
                }
                if (LoginWeb())
                {

                    HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);

                    req.Method = HttpContext.Request.HttpMethod;
                    req.ContentLength = 0;
                    req.CookieContainer = CookieContainer;
                    req.KeepAlive = true;
                    req.Timeout = 10000;
                    HttpWebResponse response = (HttpWebResponse)req.GetResponse();
                    Stream responseStream = response.GetResponseStream();
                    var reader = new StreamReader(responseStream, Encoding.UTF8);
                    string srcString = reader.ReadToEnd();
                    response.Close();

                    string viewState = HttpUtility.UrlEncode(GetStringBetween("id=\"__VIEWSTATE\" value=\"", "\"", srcString));
                    string eventValidation = HttpUtility.UrlEncode(GetStringBetween("id=\"__EVENTVALIDATION\" value=\"", "\"", srcString));
                    string tmp = GetStringBetween("title=\"Tải văn bản tiếng Việt\" href=\"javascript:__doPostBack(", ")\">Tải Văn bản tiếng Việt</a>", srcString);
                    string eventTarget = HttpUtility.UrlEncode(tmp.Split(',')[0].Replace("&#39;", ""));
                    string eventArgument = HttpUtility.UrlEncode(tmp.Split(',')[1].Replace("&#39;", ""));
                    string data = "__VIEWSTATE=" + viewState + "&__EVENTVALIDATION=" + eventValidation + "&__EVENTTARGET=" + eventTarget + "&__EVENTARGUMENT=" + eventArgument;

                    req = (HttpWebRequest)HttpWebRequest.Create(url.Split('?')[0]);
                    req.Method = "POST";
                    byte[] bufferReqBytes = Encoding.UTF8.GetBytes(data);
                    req.ContentType = "application/x-www-form-urlencoded";
                    req.KeepAlive = true;
                    req.Timeout = 10000;
                    req.CookieContainer = CookieContainer;
                    req.CookieContainer.Add(response.Cookies);

                    req.ContentLength = bufferReqBytes.Length;
                    Stream reqst = req.GetRequestStream();
                    reqst.Write(bufferReqBytes, 0, bufferReqBytes.Length);
                    reqst.Close();
                    HttpWebResponse res = (HttpWebResponse)req.GetResponse();
                    string fileName = res.ResponseUri.LocalPath.Split('/').LastOrDefault();
                    string baseDir = System.Web.HttpContext.Current.Server.MapPath("/Files/" + caseCode + "/files/");
                    if (fileName != null && !fileName.Contains(".doc"))
                    {
                        fileName = Regex.Split(res.GetResponseHeader("Content-disposition"), "filename=")[1];
                    }
                    //StreamReader sr = new StreamReader(res.GetResponseStream(), Encoding.UTF8);
                    //string source = sr.ReadToEnd();

                    //using (StreamWriter file = new StreamWriter(baseDir + fileName))
                    //{
                    //    file.Write(source);
                    //}
                    //res.Close();
                    if (fileName == "")
                    {
                        return "Maintain";
                    }
                    string[] filePaths = Directory.GetFiles(baseDir);
                    if (filePaths.Contains(baseDir + fileName))
                    {
                        return "Exist";
                    }
                    using (var file = System.IO.File.Create(baseDir + fileName))
                    {
                        var buffer = new byte[4096];
                        int bytesReceived;
                        while ((bytesReceived = res.GetResponseStream().Read(buffer, 0, buffer.Length)) != 0)
                        {
                            file.Write(buffer, 0, bytesReceived);
                        }
                        res.Close();
                    }
                    return "success," + fileName;
                }
                else return ("LoginError");
            }
            catch (Exception)
            {
            }
            return ("Error");

        }




        public string GetStringBetween(string start, string end, string source)
        {
            int i = source.IndexOf(start) + start.Length;
            int j = source.IndexOf(end, i);
            return source.Substring(i, j - i);
        }
    }
}
