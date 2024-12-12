using Extension;
using FD_PaymentReconciliation_V2.BusinessObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System.Linq;
using System.Threading.Tasks;

namespace FD_CP_BTP.App_Code.Filters
{
    public class AuthorizationFilter : IAsyncAuthorizationFilter
    {
        public async Task OnAuthorizationAsync(AuthorizationFilterContext filterContext)
        {
            if (filterContext.Filters.Any(item => item is IAllowAnonymousFilter))
            {
                return;
            }

            SessionBO session = filterContext.HttpContext.Session.GetObject<SessionBO>("UserSessionDetails");

            if (AjaxRequestExtensions.IsAjaxRequest(filterContext.HttpContext.Request))
            {
                await Task.Run(() =>
                {
                    #region Check Session

                    if (session == null)
                    {
                        filterContext.Result = new StatusCodeResult(440);
                        return ;
                    }
                    #endregion
                });
            }
            else
            {
                await Task.Run(() =>
                {
                    #region Check Session
                    if (session == null)
                    {
                        filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Error", action = "Expired" }));
                        return;
                    }
                    #endregion
                });
            }           
        }
    }
}
