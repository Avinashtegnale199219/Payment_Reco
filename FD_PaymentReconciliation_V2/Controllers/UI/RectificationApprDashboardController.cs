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
using Newtonsoft.Json;

namespace FD_PaymentReconciliation_V2.Controllers.UI
{
    public class RectificationApprDashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GetRectificationApprovalList()
        {
            bool Success = false;

            try
            {
                SessionBO objSessionBo = HttpContext.Session.GetObject<SessionBO>("UserSessionDetails");

                RectificationApprDashboardDAL apprDAL = new RectificationApprDashboardDAL();
                DataSet ds = apprDAL.GetRectification_ApprovalDashboard(Convert.ToString(objSessionBo.Entity_Id));
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    Success = true;
                    return Json(new { Success, Headers = ds.Tables[0] });
                }
                else return null;
            }
            catch (Exception ex)
            {
                ExceptionUtility.LogExceptionAsync(ex);
                Success = false;
                return Json(new { Success });
            }
        }
        [HttpPost]
        public ActionResult GetRectificationApproverList([FromBody]RectificationApprDashboardBO objBO)
        {
            bool Success = false;
            try
            {
                SessionBO objSessionBo = HttpContext.Session.GetObject<SessionBO>("UserSessionDetails");

                RectificationApprDashboardDAL apprDAL = new RectificationApprDashboardDAL();
                if (objBO.HdrReq.Trim().Contains("LAFD"))
                {
                    using (DataSet ds = apprDAL.Get_LAFD_RectificationApproverList(objBO.HdrReq.Trim(), objBO.HdrRectReq.Trim()))
                    {
                        if (ds != null && ds.Tables[0].Rows.Count > 0)
                        {
                            Success = true;
                            return Json(new { Success, Headers = ds.Tables[0] });
                        }
                        else
                        {
                            Success = false;
                            return Json(new { Success });
                        }
                    }
                }
                else
                {
                    using (DataSet ds = apprDAL.Get_RectificationApproverList(objBO.HdrReq.Trim(), objBO.HdrRectReq.Trim()))
                    {
                        if (ds != null && ds.Tables[0].Rows.Count > 0)
                        {
                            Success = true;
                            return Json(new { Success, Headers = ds.Tables[0] });
                        }
                        else
                        {
                            Success = false;
                            return Json(new { Success });
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                ExceptionUtility.LogExceptionAsync(ex);
                Success = false;
                return Json(new { Success });
            }
        }
        [HttpPost]
        public ActionResult UpdateRectificationApprovalData([FromBody]RectificationApprDashboardBO objBO)
        {
            bool Success = false;
            try
            {
                SessionBO objSessionBo = HttpContext.Session.GetObject<SessionBO>("UserSessionDetails");

                RectificationApprDashboardDAL apprDAL = new RectificationApprDashboardDAL();
                if (objBO.HdrReq.Contains("LAFD"))
                {
                    int i = apprDAL.Update_LAFDRectificationApproverData(objBO.HdrRectReq, objBO.HdrReq, objBO.Applist, objBO.Remarks, objSessionBo, objBO.IsAllRequestApproved);
                    if (i == 1 || i == -1)
                    {
                        Success = true;
                        return Json(new { Success });
                    }
                    else
                    {
                        Success = false;
                        return Json(new { Success });
                    }
                }
                else
                {
                    int i = apprDAL.Update_RectificationApproverData(objBO.HdrRectReq, objBO.HdrReq, objBO.Applist, objBO.Remarks, objSessionBo, objBO.IsAllRequestApproved);
                    if (i == 1 || i == -1)
                    {
                        Success = true;
                        return Json(new { Success });
                    }
                    else
                    {
                        Success = false;
                        return Json(new { Success });
                    }
                }
                //using (DataSet ds = apprDAL.Update_RectificationApproverData(objBO.HdrRectReq, objBO.HdrReq,objBO.Applist, objBO.Remarks,objSessionBo, objBO.IsAllRequestApproved))
                //{
                //    if (ds != null && ds.Tables[0].Rows.Count > 0)
                //    {
                //        Success = true;
                //        return Json(new { Success, Headers = ds.Tables[0] });
                //    }
                //    else
                //    {
                //        Success = false;
                //        return Json(new { Success });
                //    }
                //}

            }
            catch (Exception ex)
            {
                ExceptionUtility.LogExceptionAsync(ex);
                Success = false;
                return Json(new { Success });
            }
        }

        [HttpPost]
        public ActionResult UpdateRectificationRejectionData([FromBody]RectificationApprDashboardBO objBO)
        {
            bool Success = false;
            try
            {
                SessionBO objSessionBo = HttpContext.Session.GetObject<SessionBO>("UserSessionDetails");

                RectificationApprDashboardDAL apprDAL = new RectificationApprDashboardDAL();
                using (DataSet ds = apprDAL.Get_RectificationApproverList(objBO.HdrReq, objBO.HdrRectReq))
                {
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        Success = true;
                        return Json(new { Success, Headers = ds.Tables[0] });
                    }
                    else
                    {
                        Success = false;
                        return Json(new { Success });
                    }
                }

            }
            catch (Exception ex)
            {
                ExceptionUtility.LogExceptionAsync(ex);
                Success = false;
                return Json(new { Success });
            }
        }



    }


}