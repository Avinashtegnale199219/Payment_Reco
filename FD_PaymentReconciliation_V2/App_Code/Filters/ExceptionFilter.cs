using Extension;
using FD_PaymentReconciliation_V2.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System.Threading.Tasks;

namespace FD_CP_BTP.App_Code.Filters
{
    public class ExceptionFilter : IAsyncExceptionFilter
    {
        public async Task OnExceptionAsync(ExceptionContext context)
        {
            if (!context.ExceptionHandled)
            {
                await Task.Run(() =>
                {
                    ExceptionUtility.LogExceptionAsync(context.Exception, context.ActionDescriptor as ControllerActionDescriptor);

                    context.ExceptionHandled = true;

                    if (AjaxRequestExtensions.IsAjaxRequest(context.HttpContext.Request))
                    {
                        context.Result = new StatusCodeResult(500);
                    }
                    else
                    {
                        context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Error", action = "Index" }));
                    }
                });
            }      
            
        }
    }
}
