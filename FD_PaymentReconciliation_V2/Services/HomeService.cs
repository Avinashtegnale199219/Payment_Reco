using ApiRequestor;
using FD_PaymentReconciliation_V2.BusinessObject;
using System;
using System.Collections.Generic;
using System.Net.Http;
using WA_FD_CP_AUTHENTICATION_MODEL;

namespace FD_PaymentReconciliation_V2.Services
{
    public class HomeService
    {
        public SessionBO AuthenticateApp(UserCredentials _crd, ref string Message)
        {
            SessionBO _obj = null;

            try
            {
                
                string requestUri = Convert.ToString(Startup.Configuration["APIServices:AppAuthenticationAPI"]);

                if (string.IsNullOrEmpty(requestUri))
                {
                    throw new Exception("No Console Api found to request !!");
                }

                using (HttpResponseMessage response = HttpRequestFactory.Post(requestUri, _crd).Result)
                {
                    if (response.IsSuccessStatusCode)
                    {
                        _obj = response.ContentAsType<SessionBO>();
                    }
                    else
                    {
                        if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                        {
                            Message = response.ContentAsType<string>();
                        }
                        if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                        {
                            Message = response.ContentAsType<string>();
                        }
                        else
                        {
                            Message = "Something went wrong while processing your request !!";
                        }
                    }

                    return _obj;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public IList<UserMenuItem> GetMenus(string UserID, string App_Code)
        {
            IList<UserMenuItem> _obj = null;
            try
            {

                string requestUri = Convert.ToString(Startup.Configuration["APIServices:AppMenusAPI"]);

                if (string.IsNullOrEmpty(requestUri))
                {
                    throw new Exception("No Console Api found to request !!");
                }
                //Commented By ramsingh
                requestUri += "/" + UserID + "/" + App_Code;

                //requestUri += "?UserID=" + SapCode + "&App_Code=" + SysCode + "";

                using (HttpResponseMessage response = HttpRequestFactory.Get(requestUri).Result)
                {
                    if (response.IsSuccessStatusCode)
                    {
                        _obj = response.ContentAsType<IList<UserMenuItem>>();
                    }

                    return _obj;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
