using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WA_FD_CP_AUTHENTICATION_MODEL;
using Microsoft.AspNetCore.Http.Features;
using Extension;
using FD_PaymentReconciliation_V2.App_Code.BusinessObject;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using FD_PaymentReconciliation_V2.Services;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using FD_PaymentReconciliation_V2.BusinessObject;

namespace FD_PaymentReconciliation_V2.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        [Route("UI/Home.aspx")]
        public IActionResult Index(string UserId, string SysCode)
        {
            //try
            //{
            ClearSession();
            HttpContext.Session.SetString("UserId", UserId);
            HttpContext.Session.SetString("SysCode", SysCode);
            UserId = DecryptToken(UserId.Replace(' ', '+'));
            SysCode = DecryptToken(SysCode.Replace(' ', '+'));

            if (Startup.Configuration["Envoirnment"].ToString() == "Development")
            {
                UserId = "64294"; //bhushan
                //UserId = "25060"; //sailaja
                SysCode = "MFEAPP00102";
            }


            if (string.IsNullOrEmpty(UserId) || string.IsNullOrEmpty(SysCode))
            {
                return RedirectPermanent(Startup.Configuration["AppSettings:LogOut"].ToString());
            }
            else
            {
                UserApplicationDtls.UserApplicationDtls _app = new UserApplicationDtls.UserApplicationDtls(HttpContext);
                HomeService home = new HomeService();
                string Msg = string.Empty;

                UserCredentials crd = new UserCredentials()
                {
                    SysCode = SysCode,
                    UserId = UserId,
                    DomainName = _app.DomainName,
                    IPAddress = _app.IPAddress,
                    MacAddress = _app.MacAddress,
                    ServerIP = _app.ServerIp,
                    IsLogged = true,
                    IsMobile = _app.IsMobile,
                    UserAgent = _app.UserAgent,
                    BrowserMajor = _app.BrowserMajor,
                    BrowserMinor = string.Empty,
                    BrowserType = _app.BrowserType,
                    BrowserVersion = _app.BrowserVersion,
                    RequestURL = _app.RequestUrl,
                };

                SessionBO objsession = home.AuthenticateApp(crd, ref Msg);


                if (objsession != null && string.IsNullOrEmpty(Msg))
                {
                    objsession.CreatedByUName = objsession.CreatedByUName ?? objsession.Entity_Name;
                    objsession.CreatedIP = objsession.CreatedIP ?? GetIPAddress();
                    objsession.CreatedType = objsession.CreatedType ?? objsession.Entity_Type_Code;

                    HttpContext.Session.SetObject("UserSessionDetails", objsession);
                    ExceptionUtility.Session = objsession;

                    IList<UserMenuItem> menus = home.GetMenus(objsession.Entity_Id, SysCode);

                    if (menus != null)
                    {


                        HttpContext.Session.SetObject("UserAppMenus", menus);
                    }
                    return RedirectToAction("Index", "Default");
                }
                else
                {
                    ViewBag.Message = "Something went wrong while processing your request !!";
                    return RedirectPermanent(Startup.Configuration["AppSettings:LogOut"].ToString());
                }
            }
            //}
            //catch (Exception ex)
            //{

            //    ViewBag.Message = "Error - 10003 : " + ex.Message.ToString();
            //    return RedirectPermanent(Startup.Configuration["AppSettings:LogOut"].ToString());
            //}
        }

        void ClearSession()
        {
            try
            {
                HttpContext.Session.Clear();

                if (TempData.Keys.Count > 0)
                {
                    TempData.Clear();
                }
            }
            catch (Exception)
            {
            }
        }

        public IActionResult LogOut()
        {
            string LogOutUrl = Startup.Configuration["AppSettings:LogOut"].ToString();

            if (!string.IsNullOrEmpty(LogOutUrl))
            {
                ClearSession();
                return RedirectPermanent(LogOutUrl);
            }
            return View();
        }

        public IActionResult Home()
        {

            string UserId = HttpContext.Session.GetString("UserId");
            string SysCode = HttpContext.Session.GetString("SysCode");
            SessionBO dtl = HttpContext.Session.GetObject<SessionBO>("UserSessionDetails");
            if (!string.IsNullOrEmpty(UserId) && !string.IsNullOrEmpty(SysCode) && dtl != null)
            {
                string HomeUrl = Startup.Configuration["AppSettings:Home"].ToString() + "?UserId=" + UserId + "&SysCode=" + SysCode;
                //string HomeUrl = Startup.Configuration["AppSettings:Home"].ToString();
                HttpContext.Response.Headers.Add("UserSessionDetails", dtl.ToString());
                if (!string.IsNullOrEmpty(HomeUrl))
                {
                    ClearSession();
                    return Redirect(HomeUrl);
                }
            }
            return View();


            //string HomeUrl = Startup.Configuration["AppSettings:Home"].ToString();

            //if (!string.IsNullOrEmpty(HomeUrl))
            //{
            //    ClearSession();
            //    return RedirectPermanent(HomeUrl);
            //}
            //return View();
        }

        public static string DecryptToken(string cipherText)
        {
            try
            {

                MFCipherBO objCipher = new MFCipherBO
                {
                    Key = Startup.Configuration["AppSettings:API_Private_Key"].ToString(), //key;
                    IV = Startup.Configuration["AppSettings:API_Vector_Key"].ToString(),// IV;
                    Text = cipherText
                };


                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string jsonInString = JsonConvert.SerializeObject(objCipher);
                HttpResponseMessage response = client.PostAsync(new Uri(Startup.Configuration["DeCipherUri"].ToString()), new StringContent(jsonInString, Encoding.UTF8, "application/json")).Result;
                string decryptedToken = string.Empty;
                Token token = new Token();
                if (response.IsSuccessStatusCode)
                {
                    decryptedToken = response.Content.ReadAsStringAsync().Result;
                    token = JsonConvert.DeserializeObject<Token>(decryptedToken);
                }
                else
                {
                    token.Result = null;
                }
                return token.Result;
            }
            catch (Exception EX)
            {
                ExceptionUtility.LogExceptionAsync(EX);
                return null;
            }

        }

        public string GetIPAddress()
        {
            var remoteIpAddress = HttpContext.Features.Get<IHttpConnectionFeature>()?.RemoteIpAddress;
            return remoteIpAddress.ToString();
        }

        public class Token
        {
            public string Result { get; set; }
        }
    }
}
