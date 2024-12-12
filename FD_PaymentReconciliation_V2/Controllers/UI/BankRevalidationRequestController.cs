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
    public class BankRevalidationRequestController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetApplicationDetailsForConversion([FromBody] BankRevalidationRequestBO bankRevalidationRequestBO)
        {
            try
            {
                BankRevalidationRequestDAL objDAL = new BankRevalidationRequestDAL();
                DataTable dt = objDAL.Get_RevalidationData(bankRevalidationRequestBO.SearchType, bankRevalidationRequestBO.SearchValue);
                if (dt != null && dt.Rows.Count > 0)
                {
                    return Json(new { Status = "1", Message = "Success", Data = dt });

                }
                else
                {
                    return Json(new { Status = "0", msg = "No Data Found", DataSet = string.Empty });
                }
            }
            catch (Exception ex)
            {
                ExceptionUtility.LogExceptionAsync(ex);
                return Json(new { Status = "0", Message = "Error", Data = "Something Went Wrong..!" });
            }
        }

        [HttpPost]
        public ActionResult GetRaiseRequest([FromBody] string deP_NO)
        {
            try
            {
                string[] deP_NO_iW_NO;
                deP_NO_iW_NO = deP_NO.Split('_');

                BankRevalidationRequestDAL objDAL = new BankRevalidationRequestDAL();
                DataTable dt = objDAL.Get_BankRevalidationDepDtl(deP_NO_iW_NO[0], deP_NO_iW_NO[1]);
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


        [HttpPost]
        public ActionResult SaveRevalidation_RaiseRequest([FromBody] BankRevalidationRaiseRequestBO objBO)
        {
            try
            {
                BankRevalidationRequestDAL objDAL = new BankRevalidationRequestDAL();
                SessionBO sessionBO = HttpContext.Session.GetObject<SessionBO>("UserSessionDetails");
                string Chg_BankDtl_ReqRcd_BVL_Status = "", Chg_BankDtl_ReqRcd_Offln = "";

                if (!string.IsNullOrEmpty(objBO.P_IsChg_BankDtl_ReqRcd_ONL))
                {
                    Chg_BankDtl_ReqRcd_BVL_Status = objBO.P_IsChg_BankDtl_ReqRcd_ONL;

                    var Split_Str1 = Chg_BankDtl_ReqRcd_BVL_Status.Split(new[] { '-' }, 2);
                    if (Split_Str1.Length == 2)
                    {
                        objBO.P_IsChg_BankDtl_ReqRcd_ONL = Split_Str1[0];
                        objBO.P_Chg_BankDtl_ReqRcd_On_BVL = Convert.ToString(Split_Str1[1]);
                    }

                }

                if (!string.IsNullOrEmpty(objBO.P_IsChg_BankDtl_ReqRcd_Offln))
                {
                    Chg_BankDtl_ReqRcd_Offln = objBO.P_IsChg_BankDtl_ReqRcd_Offln;
                    var Split_Str2 = Chg_BankDtl_ReqRcd_Offln.Split(new[] { '-' }, 2);
                    if (Split_Str2.Length == 2)
                    {
                        objBO.P_IsChg_BankDtl_ReqRcd_Offln = Split_Str2[0];
                        objBO.P_Chg_BankDtl_ReqRcd_On_Offln = Convert.ToString(Split_Str2[1]);
                    }
                }

                string InsertData_Response = objDAL.SAVE_Revalidation_RaiseRequest(objBO, sessionBO);
                if (InsertData_Response == "Request Generated Successfully")
                {
                    return Json(new { Status = "1", Message = "Success", Data = InsertData_Response });

                }
                else
                {
                    return Json(new { Status = "0", Message = "Failure", Data = InsertData_Response });
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