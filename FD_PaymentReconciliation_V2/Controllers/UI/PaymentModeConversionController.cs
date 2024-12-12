using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Extension;
using FD_PaymentReconciliation_V2.App_Code.BusinessObject;
using FD_PaymentReconciliation_V2.App_Code.DataAccessLayer;
using FD_PaymentReconciliation_V2.BusinessObject;
using FD_PaymentReconciliation_V2.Services;
using Microsoft.AspNetCore.Mvc;

namespace FD_PaymentReconciliation_V2.Controllers.UI
{
    public class PaymentModeConversionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetApplicationDetailsForConversion([FromBody] PaymentModeConversionBO objBO)
        {           
            try
            {
                SessionBO objSessionBo = HttpContext.Session.GetObject<SessionBO>("UserSessionDetails");

                PaymentModeConversionDAL objDAL = new PaymentModeConversionDAL();
                DataTable dt = objDAL.GetApplicationDetailsForConversion(objBO.Appl_No);
                if (dt != null && dt.Rows.Count > 0)
                {
                   return Json(new { Status = "1", Message = "Success", Data = dt });

                }
                else
                {
                    return Json(new { Status = "0", msg = "No Record Found", DataSet = string.Empty });
                }
            }
            catch (Exception ex)
            {
                ExceptionUtility.LogExceptionAsync(ex);
                return Json(new { Status = "0", Message = "Error", Data = "Something Went Wrong..!" });
            }
        }

        public ActionResult GetPaymentMode()
        {
            PaymentModeConversionDAL objDAL = new PaymentModeConversionDAL();
            using (DataTable dt = objDAL.GetPaymentMode())
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    return Ok(dt);
                }
                return NoContent();
            }
        }

        [HttpPost]
        public ActionResult ConvertPaymentMode([FromBody] PaymentModeConversionBO objBO)
        {
            int res = 0;
            try
            {
                SessionBO objSessionBo = HttpContext.Session.GetObject<SessionBO>("UserSessionDetails");

                PaymentModeConversionDAL objDAL = new PaymentModeConversionDAL();
                res = objDAL.ConvertPaymentMode(objSessionBo, objBO);
                objDAL.InsertPaymentModeConvLog(objSessionBo, objBO);
                if (res > 0)
                {
                    return Json(new { Status = "1", msg = "Success" });
                }
                else
                {
                    return Json(new { Status = "2", msg = "Something went wrong" });
                }
               
            }
            catch (Exception ex)
            {
                ExceptionUtility.LogExceptionAsync(ex);
                return Json(new { Status = "0", Message = "Error", Data = "Something Went Wrong..!" });
            }
        }
    }
}