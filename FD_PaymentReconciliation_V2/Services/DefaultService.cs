using ApiRequestor;
using FD_PaymentReconciliation_V2.BussinessObject;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace FD_PaymentReconciliation_V2.Services
{
    public class DefaultService
    {
        public async Task<bool> SaveMenuLog(UserMenuLogBO menuLog)//, string Token)
        {
            try
            {


                AuthenticationHeaderValue bearerToken = null;

                string requestUri = Convert.ToString(Startup.Configuration["APIServices:AppMenusAPI"]);

                if (string.IsNullOrEmpty(requestUri))
                {
                    throw new Exception("No Console Api found to request !!");
                }

                //if (!string.IsNullOrEmpty(Token))
                //{
                //    bearerToken = new AuthenticationHeaderValue("Bearer", Token);
                //}

                using (HttpResponseMessage response = await HttpRequestFactory.Post(requestUri, menuLog, bearerToken))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return response.ContentAsType<bool>();
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


    }
}
