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

namespace FD_PaymentReconciliation_V2.Controllers
{
    public class BankRevalidationRequestDashboard_L2Controller : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Get_BankRevalidation_PendingApproval_Dashboard()
        {
            BankRevalidationRequestDAL objDAL = new BankRevalidationRequestDAL();
            try
            {
                int Approval_Stage = 2;
                SessionBO SBO = HttpContext.Session.GetObject<SessionBO>("UserSessionDetails");
                using (DataSet dataSet = objDAL.Get_BankRevalidation_PendingApproval_Dashboard(Approval_Stage,SBO))
                {
                    if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                    {
                        return Json(new { Status = "1", Message = "Success", Data = dataSet.Tables[0] });
                    }
                    else
                    {
                        throw new Exception("No Data Found..!");

                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionUtility.LogExceptionAsync(ex);
                return Json(new { Status = "0", Message = "Error", Data = "Something Went Wrong..!" });
            }

        }

        [HttpPost]
        public IActionResult SendData([FromBody] BankRevalidationRaiseRequestBO objBO)
        {
            try
            {
                if (objBO != null)
                {
                    TempData["P_Dep_NO"] = objBO.P_Dep_NO;
                    TempData["P_War_NO"] = objBO.P_War_NO;
                    TempData["Rev_Req_No"] = objBO.Rev_Req_No;
                    return Json(new { Status = "1", Message = "Success", Data = objBO });
                }
                else
                {
                    return Json(new { Status = "0", Message = "Success", Data = "Something Went Wrong..!" });
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