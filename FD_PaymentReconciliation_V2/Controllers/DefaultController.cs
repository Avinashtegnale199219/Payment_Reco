using Extension;
using FD_PaymentReconciliation_V2.BusinessObject;
using FD_PaymentReconciliation_V2.BussinessObject;
using FD_PaymentReconciliation_V2.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WA_FD_CP_AUTHENTICATION_MODEL;

namespace FD_PaymentReconciliation_V2.Controllers
{
    public class DefaultController : Controller
    {
       
        public IActionResult Index()
        {
            return View();
        }

        //[HttpGet, Route("Default/SaveMenuLog/{SubModeCode}")]
        //public void SaveMenuLog(string SubModeCode)
        //{

        //    if (HttpContext.Session.Keys.Contains("SubModeCode"))
        //    {
        //        HttpContext.Session.Remove("SubModeCode");
        //    }

        //    IList<UserMenuItem> UserMenuItemdtl = HttpContext.Session.GetObject<IList<UserMenuItem>>("UserAppMenus");

        //    UserSessionDetails objsession = HttpContext.Session.GetObject<UserSessionDetails>("UserSessionDetails");

        //    UserApplicationDtls.UserApplicationDtls _app = new UserApplicationDtls.UserApplicationDtls(HttpContext);

        //    if (UserMenuItemdtl != null && UserMenuItemdtl.Count > 0)
        //    {
        //        UserMenuItem selectedmenu = UserMenuItemdtl.Where(x => x.SubModCode.Equals(SubModeCode)).SingleOrDefault();

        //        if (selectedmenu != null)
        //        {

        //            HttpContext.Session.SetString("SubModeCode", SubModeCode);
        //            HttpContext.Session.SetString("SubModeName", selectedmenu.SubModName);
        //            ExceptionUtility.Session.FormCode = SubModeCode;
        //        }
        //    }
        //    return;


        //}

        [HttpGet, Route("Default/SaveMenuLog/{SubModeCode}")]
        public async void SaveMenuLog(string SubModeCode)
        {
            try
            {
                if (HttpContext.Session.Keys.Contains("SubModeCode"))
                {
                    HttpContext.Session.Remove("SubModeCode");
                }

                IList<WA_FD_CP_AUTHENTICATION_MODEL.UserMenuItem> UserMenuItemdtl = HttpContext.Session.GetObject<IList<UserMenuItem>>("UserAppMenus");

                //UserSessionDetails objsession = HttpContext.Session.GetObject<UserSessionDetails>("UserSessionDetails");

                SessionBO objsession = HttpContext.Session.GetObject<SessionBO>("UserSessionDetails");

                UserApplicationDtls.UserApplicationDtls _app = new UserApplicationDtls.UserApplicationDtls(HttpContext);

                if (UserMenuItemdtl != null && UserMenuItemdtl.Count > 0)
                {
                    UserMenuItem selectedmenu = UserMenuItemdtl.Where(x => x.SubModCode.Equals(SubModeCode)).SingleOrDefault();

                    if (selectedmenu != null)
                    {

                        HttpContext.Session.SetString("SubModeCode", SubModeCode);
                        HttpContext.Session.SetString("SubModeName", selectedmenu.SubModName);
                        ExceptionUtility.Session.FormCode = SubModeCode;

                        UserMenuLogBO menulog = new UserMenuLogBO()
                        {
                            MainModCode = selectedmenu.MainModCode,
                            Agency_Clustered_ID = objsession.Agency_Clustered_ID,
                            Agency_Usr_Clustered_ID = objsession.Agency_Usr_Clustered_ID,
                            Agency_Cd = objsession.Agency_Cd,
                            Agency_Type = objsession.Agency_Type,
                            Agency_Sub_Type = objsession.Agency_Sub_Type,
                            Agency_Name = objsession.Agency_Name,
                            Agency_Usr_Name = objsession.Agency_Usr_Name,
                            Agency_Usr_EmailID = objsession.Agency_Usr_EmailID,
                            Agency_Usr_MobileNo = objsession.Agency_Usr_MobileNo,
                            Agency_Usr_Base_Loc_cd = objsession.Agency_Usr_Base_Loc_cd,
                            Agency_Usr_Base_Loc_Desc = objsession.Agency_Usr_Base_Loc_Desc,
                            Agency_Usr_Base_Role_cd = objsession.Agency_Usr_Base_Role_cd,
                            Agency_Usr_Base_Role_Desc = objsession.Agency_Usr_Base_Role_Desc,
                            BrowserType = _app.BrowserType,
                            BrowserVersion = _app.BrowserVersion,
                            BrowserMajorVersion = _app.BrowserMajor,
                            BrowserMinorVersion = string.Empty,
                            Client_Mac_Address = _app.MacAddress,
                            ClientIP_Address = _app.CreatedIP,
                            SubModCode = selectedmenu.SubModCode,
                            AppCode = selectedmenu.AppCode,
                            Server_IP = _app.ServerIp,
                            Server_Domain = _app.DomainName,
                            ServerInstanceName = _app.InstanceName,
                            UserAgent = _app.UserAgent,
                            Agency_SessionID = objsession.Pk_Session_ID,
                            Agency_Session_Ref_ID = objsession.Pk_Session_Ref_ID,

                            // SubModName = selectedmenu.SubModName,
                            // PageName = selectedmenu.PageName,
                            // Location = selectedmenu.Location,
                            // AppName = selectedmenu.AppName,
                            // LogTime = DateTime.Now,
                            // SessionId = objsession.SessionId,
                            // Entity_Name = objsession.Entity_Name,
                            //  UserName = objsession.UserName,
                            //  Entity_Code = objsession.Entity_Code,
                            // Entity_SAPCode = objsession.Entity_SAPCode,
                            //Entity_Type = objsession.Entity_Type_Code,
                            //MainModName = selectedmenu.MainModName,
                            SysType = "A"
                        };

                        DefaultService service = new DefaultService();

                        //string Token = HttpContext.Session.GetString("Token");

                        //await service.SaveMenuLog(menulog, Token);
                        await service.SaveMenuLog(menulog);
                    }
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

    }


}