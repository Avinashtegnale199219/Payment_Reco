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
    public class RectificationRequestListController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetRectificationList([FromBody] RectificationRequestListBO objBO)
        {
            bool Success = false;
            try
            {
                SessionBO objSessionBo = HttpContext.Session.GetObject<SessionBO>("UserSessionDetails");

                RectificationRequestListDAL apprDAL = new RectificationRequestListDAL();
                if (objBO.HdrReq.Contains("LAFD"))
                {
                    DataSet ds = apprDAL.Get_LAFDRectificationRequestList(objBO.HdrReq);
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
                else
                {
                    DataSet ds = apprDAL.Get_RectificationRequestList(objBO.HdrReq);
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
        [HttpPost]
        public ActionResult InsertRectificationRequest([FromBody] RectificationRequestListBO objBO)
        {           
            try
            {
                SessionBO objSessionBo = HttpContext.Session.GetObject<SessionBO>("UserSessionDetails");

                RectificationRequestListDAL objDAL = new RectificationRequestListDAL();
                if (objBO.HdrReq.Trim().Contains("LAFD"))
                {
                    using (DataSet ds = objDAL.InsertLAFDRectificationRequest_Hdr(objBO.HdrReq.Trim(), objBO.Remarks, objSessionBo))
                    {
                        if (ds != null && ds.Tables[0].Rows.Count > 0)
                        {
                            string RecthdrSeq = string.Empty;

                            if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0][0])))
                            {
                                RecthdrSeq = Convert.ToString(ds.Tables[0].Rows[0][0]);

                                DataSet ds1 = objDAL.InsertLAFDRectificationRequest_Dtl(RecthdrSeq, objBO.HdrReq.Trim(), objBO.Applist, objSessionBo);
                                {
                                    return Json(new { Headers = "Success" });
                                }
                            }
                            else
                            {
                                return Json(new { Headers = "Error" });
                            }
                        }
                        else return null;
                    }
                }
                else
                {
                    using (DataSet ds = objDAL.InsertRectificationRequest_Hdr(objBO.HdrReq.Trim(), objBO.Remarks, objSessionBo))
                    {
                        if (ds != null && ds.Tables[0].Rows.Count > 0)
                        {
                            string RecthdrSeq = string.Empty;

                            if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0][0])))
                            {
                                RecthdrSeq = Convert.ToString(ds.Tables[0].Rows[0][0]);

                                DataSet ds1 = objDAL.InsertRectificationRequest_Dtl(RecthdrSeq, objBO.HdrReq.Trim(), objBO.Applist, objSessionBo);
                                {
                                    return Json(new { Headers = "Success" });
                                }
                            }
                            else
                            {
                                return Json(new { Headers = "Error" });
                            }
                        }
                        else return null;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionUtility.LogExceptionAsync(ex);
                return Json(new { Headers = "Error" });
            }
        }
    }
}