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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FD_PaymentReconciliation_V2.Controllers.UI
{
    public class BankRevalidationRequest_FirstApprovalController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                if (TempData["P_Dep_NO"] != null || TempData["P_War_NO"] != null)
                {
                    string P_Dep_NO = TempData["P_Dep_NO"].ToString();
                    string P_War_NO = TempData["P_War_NO"].ToString();
                    TempData["f_DEP_NO_War_NO"] = P_Dep_NO + "_" + P_War_NO;
                    HttpContext.Session.SetString("f_DEP_NO_War_NO", P_Dep_NO + "_" + P_War_NO);

                }

            }
            catch (Exception ex)
            {
                ExceptionUtility.LogExceptionAsync(ex);
            }
            return View();
        }

        [HttpPost]
        public ActionResult btnApproval_Click([FromBody] BankRevalidationRaiseRequestBO revalidationRaiseRequestBO)
        {
            try
            {
                BankRevalidationRequestDAL objDAL = new BankRevalidationRequestDAL();
                SessionBO sessionBO = HttpContext.Session.GetObject<SessionBO>("UserSessionDetails");

                string FirstApproval_Status = objDAL.FirstApproval_Rejection(revalidationRaiseRequestBO, sessionBO);
                if (!string.IsNullOrEmpty(FirstApproval_Status))
                {
                    return Json(new { Status = "1", Message = "Success", Data = FirstApproval_Status });
                }
                else
                {
                    return Json(new { Status = "0", msg = "No Record Found", Data = FirstApproval_Status });
                }
            }
            catch (Exception ex)
            {
                ExceptionUtility.LogExceptionAsync(ex);
                return Json(new { Status = "0", Message = "Error", Data = "Something Went Wrong..!" });
            }
        }

        [HttpPost]
        public ActionResult btnReject_Click([FromBody] BankRevalidationRaiseRequestBO revalidationRaiseRequestBO)
        {
            try
            {
                BankRevalidationRequestDAL objDAL = new BankRevalidationRequestDAL();
                SessionBO sessionBO = HttpContext.Session.GetObject<SessionBO>("UserSessionDetails");
                string FirstRejection_Status = objDAL.FirstApproval_Rejection(revalidationRaiseRequestBO, sessionBO);
                if (!string.IsNullOrEmpty(FirstRejection_Status))
                {
                    return Json(new { Status = "1", Message = "Success", Data = FirstRejection_Status });

                }
                else
                {
                    return Json(new { Status = "0", msg = "No Record Found", Data = FirstRejection_Status });
                }
            }
            catch (Exception ex)
            {
                ExceptionUtility.LogExceptionAsync(ex);
                return Json(new { Status = "0", Message = "Error", Data = "Something Went Wrong..!" });
            }
        }

        [HttpPost]
        public IActionResult GetdeP_NO()
        {
            try
            {
                string Dep_No = HttpContext.Session.GetString("f_DEP_NO_War_NO");
                return Json(new { Status = "1", Message = "Success", Data = Dep_No });
            }
            catch (Exception ex)
            {
                ExceptionUtility.LogExceptionAsync(ex);
                return Json(new { Status = "0", Message = "Error", Data = "Something Went Wrong..!" });
            }
        }
    }
}