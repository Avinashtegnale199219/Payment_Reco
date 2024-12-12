using FD_PaymentReconciliation_V2.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FD_PaymentReconciliation_V2.Controllers
{
    [AllowAnonymous]
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            //return await Task<IActionResult>.Run(() =>
            //{
            //    IExceptionHandlerPathFeature exceptionFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            //    if (exceptionFeature != null)
            //    {
            //        ExceptionUtility.LogExceptionAsync(exceptionFeature.Error, exceptionFeature.Path);
            //    }

                return View();
            //});
        }

        public IActionResult Authorization()
        {
            return View();
        }

        public IActionResult Expired()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ExceptionLog(string ErrorMsg)
        {
            ExceptionUtility.ClientSideErrorLog(ErrorMsg);
            return Ok();
        }
    }
}
