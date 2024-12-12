using System;
using System.Collections.Generic;
using System.Data;
using Extension;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FD_PaymentReconciliation_V2.BusinessObject;
using System.IO;
using FD_PaymentReconciliation_V2.App_Code.DataAccessLayer;
using FD_PaymentReconciliation_V2.App_Code.BusinessObject;
using FD_PaymentReconciliation_V2.Services;

namespace FD_PaymentReconciliation_V2.Controllers.UI
{
    public class RTGSReconciliationDashBoardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        
        public ActionResult GetReconciliationData()
        {
            

            RTGSRecoProcessDashBoardDAL objDAL = new RTGSRecoProcessDashBoardDAL();
            try
            {
                using (DataSet dataSet = objDAL.GetReconciliationData())
                {
                    if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                    {
                        
                        return Json (new { Status = "1", Message = "Success", Data = dataSet.Tables[0] });
                    }
                    else
                    {
                        
                        return Json(new { Status = "1", Message = "Success", Data = dataSet.Tables[0] });
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionUtility.LogExceptionAsync(ex);
                return Json(new { Status = "0", Message = "Error", Data = "Something went wrong !" });
            }
            
        }

        [HttpPost]
        public ActionResult GetReconUploadDataFileByHdrSeq([FromBody]RTGSReconciliationDashBoardBO objbo)
        {
            string res = string.Empty;
            string FilePath = "";
            RTGSRecoProcessDashBoardDAL objDAL = new RTGSRecoProcessDashBoardDAL();
            SessionBO objSessionBo = HttpContext.Session.GetObject<SessionBO>("UserSessionDetails");
            try
            {
                using (DataSet dataSet = objDAL.GetReconUploadData(objbo.HdrSeq))
                {
                    if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                    {

                        string filename = "UploadData_" + objbo.HdrSeq + "_" + DateTime.Now.Ticks.ToString();
                        FilePath = Startup.Configuration["AppSettings:RecoUploadPath"].ToString() + filename + ".xlsx";// ConfigurationManager.AppSettings["RecoUploadPath"].ToString() + filename + ".xlsx";
                        Export2Excel.DataTableToBytesArray(dataSet.Tables[0], filename, FilePath);
                        filename = filename + ".xlsx";
                        if (System.IO.File.Exists(FilePath))
                        {
                            objDAL.UpdateReconHdrUploadedFileDetails(objbo.HdrSeq, filename, FilePath, objSessionBo);
                            if (HttpContext.Session.TryGetValue("FilePath", out byte[] val))
                            {
                                HttpContext.Session.Remove("FilePath");
                            }
                            HttpContext.Session.SetObject("FilePath", FilePath);
                            if (HttpContext.Session.TryGetValue("Filename", out byte[] val1))
                            {
                                HttpContext.Session.Remove("Filename");
                            }
                            HttpContext.Session.SetObject("Filename", filename);
                        }

                        return Json(new { Status = "1", Message = "Success", Data = filename });
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
                return Json(new { Status = "0", Message = "Error", Data = "Something went wrong !" });
            }

        }

        [HttpPost]
        public ActionResult GetReconExceptionDataFileByHdrSeq([FromBody]RTGSReconciliationDashBoardBO objbo)
        {

            string res = string.Empty;
            string FilePath = "";
            RTGSRecoProcessDashBoardDAL objDAL = new RTGSRecoProcessDashBoardDAL();
            SessionBO objSessionBo = HttpContext.Session.GetObject<SessionBO>("UserSessionDetails");
            try
            {
                using (DataSet dataSet = objDAL.GetReconExceptionData(objbo.HdrSeq))
                {
                    if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                    {

                        string filename = "ExceptionData_" + objbo.HdrSeq + "_" + DateTime.Now.Ticks.ToString();
                        FilePath = Startup.Configuration["AppSettings:RecoUploadPath"].ToString() + filename + ".xlsx"; 
                        Export2Excel.DataTableToBytesArray(dataSet.Tables[0], filename, FilePath);
                        filename = filename + ".xlsx";
                        if (System.IO.File.Exists(FilePath))
                        {
                            objDAL.UpdateReconHdrExceptionFileDetails(objbo.HdrSeq, filename, FilePath, objSessionBo);
                            if (HttpContext.Session.TryGetValue("FilePath", out byte[] val))
                            {
                                HttpContext.Session.Remove("FilePath");
                            }
                            HttpContext.Session.SetObject("FilePath", FilePath);
                            if (HttpContext.Session.TryGetValue("Filename", out byte[] val1))
                            {
                                HttpContext.Session.Remove("Filename");
                            }
                            HttpContext.Session.SetObject("Filename", filename);
                        }


                        return Json(new { Status = "1", Message = "Success", Data = filename });
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
                return Json(new { Status = "0", Message = "Error", Data = "Something went wrong !" });
            }
            
        }

        [HttpPost]
        public ActionResult GetReconSuccessDataFileByHdrSeq([FromBody]RTGSReconciliationDashBoardBO objBO)
        {

            string res = string.Empty;
            string FilePath = "";
            RTGSRecoProcessDashBoardDAL objDAL = new RTGSRecoProcessDashBoardDAL();
            SessionBO objSessionBo = HttpContext.Session.GetObject<SessionBO>("UserSessionDetails");

            try
            {
                using (DataSet dataSet = objDAL.GetReconSuccessData(objBO.HdrSeq))
                {
                    if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                    {

                        string filename = "SuccessData_" + objBO.HdrSeq + "_" + DateTime.Now.Ticks.ToString();
                        FilePath = Startup.Configuration["AppSettings:RecoUploadPath"].ToString() + filename + ".xlsx";
                        Export2Excel.DataTableToBytesArray(dataSet.Tables[0], filename, FilePath);
                        filename = filename + ".xlsx";
                        if (System.IO.File.Exists(FilePath))
                        {
                            objDAL.UpdateReconHdrSuccessFileDetails(objBO.HdrSeq, filename, FilePath, objSessionBo);
                            if (HttpContext.Session.TryGetValue("FilePath", out byte[] val))
                            {
                                HttpContext.Session.Remove("FilePath");
                            }
                            HttpContext.Session.SetObject("FilePath", FilePath);
                            if (HttpContext.Session.TryGetValue("Filename", out byte[] val1))
                            {
                                HttpContext.Session.Remove("Filename");
                            }
                            HttpContext.Session.SetObject("Filename", filename);
                        }


                        return Json(new { Status = "1", Message = "Success", Data = filename });
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
                return Json(new { Status = "0", Message = "Error", Data = "Something went wrong !" });
            }
           
        }
        [HttpPost]
        public ActionResult CancelReconciliationProcess([FromBody]RTGSReconciliationDashBoardBO objBO)
        {
            string res = string.Empty;
            RTGSRecoProcessDashBoardDAL objDAL = new RTGSRecoProcessDashBoardDAL();
            SessionBO objSessionBo = HttpContext.Session.GetObject<SessionBO>("UserSessionDetails");
            try
            {

                int recordsAffected = objDAL.CancelReconciliationProcess(objBO.HdrSequence, objSessionBo);
                if (recordsAffected > 0)
                {
                    return Json(new { Status = "1", Message = "Success", Data = "Cancelled Reconciliation Process Successfully" });
                }
                else
                {
                    return Json(new { Status = "0", Message = "Error", Data = "Something Went Wrong..!" });
                }

            }
            catch (Exception ex)
            {
                ExceptionUtility.LogExceptionAsync(ex);
                return Json(new { Status = "0", Message = "Error", Data = "Something went wrong !" });
            }
            
        }

        [HttpPost]
        public ActionResult ProcessReconciliationData([FromBody]RTGSReconciliationDashBoardBO objBO)
        {
            string res = string.Empty;
            RTGSRecoProcessDashBoardDAL objDAL = new RTGSRecoProcessDashBoardDAL();
            SessionBO objSessionBo = HttpContext.Session.GetObject<SessionBO>("UserSessionDetails");
            try
            {

                int recordsAffected = objDAL.ProcessReconciliationData(objBO.HdrSequence, objSessionBo);
                DataTable dt = objDAL.GetProcessedReconciliationData(objBO.HdrSequence);
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        objDAL.UpdateFDNewPaid(dr["Appl_No"].ToString(), Convert.ToDateTime(dr["Payment_Date"]));
                    }
                }
                if (recordsAffected > 0)
                {
                    return Json(new { Status = "1", Message = "Success", Data = "Reconciliation Data Processed Successfully" });
                }
                else
                {
                    return Json(new { Status = "0", Message = "Error", Data = "Something Went Wrong..!" });
                }

            }
            catch (Exception ex)
            {
                ExceptionUtility.LogExceptionAsync(ex);
                return Json(new { Status = "0", Message = "Error", Data = "Something went wrong !" });
            }
            
        }


        [HttpGet]
        public FileResult GetReconUploadData()
        {
            try
            {
                string html = string.Empty;
                string FilePath = string.Empty;
                string FileName = string.Empty;
                //need to Use session But unable to do so.
                if (HttpContext.Session.TryGetValue("FilePath", out byte[] val))
                {
                    FilePath = HttpContext.Session.GetObject<string>("FilePath");
                    HttpContext.Session.Remove("FilePath");
                }
                if (HttpContext.Session.TryGetValue("Filename", out byte[] val1))
                {
                    FileName = HttpContext.Session.GetObject<string>("Filename");
                    HttpContext.Session.Remove("Filename");
                }
                if (FilePath != null)
                {
                    byte[] fileBytes = System.IO.File.ReadAllBytes(FilePath);
                    System.IO.File.WriteAllBytes(FilePath, fileBytes);
                    MemoryStream ms = new MemoryStream(fileBytes);
                    return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, FileName);

                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        [HttpGet]
        public FileResult GetReconUploadException()
        {
            try
            {
                string html = string.Empty;
                string FilePath = string.Empty;
                string FileName = string.Empty;
                //need to Use session But unable to do so.
                if (HttpContext.Session.TryGetValue("FilePath", out byte[] val))
                {
                    FilePath = HttpContext.Session.GetObject<string>("FilePath");
                    HttpContext.Session.Remove("FilePath");
                }
                if (HttpContext.Session.TryGetValue("Filename", out byte[] val1))
                {
                    FileName = HttpContext.Session.GetObject<string>("Filename");
                    HttpContext.Session.Remove("Filename");
                }
                if (FilePath != null)
                {
                    byte[] fileBytes = System.IO.File.ReadAllBytes(FilePath);
                    System.IO.File.WriteAllBytes(FilePath, fileBytes);
                    MemoryStream ms = new MemoryStream(fileBytes);
                    return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, FileName);

                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        [HttpGet]
        public FileResult GetReconUploadSuccess()
        {
            try
            {
                string html = string.Empty;
                string FilePath = string.Empty;
                string FileName = string.Empty;
                //need to Use session But unable to do so.
                if (HttpContext.Session.TryGetValue("FilePath", out byte[] val))
                {
                    FilePath = HttpContext.Session.GetObject<string>("FilePath");
                    HttpContext.Session.Remove("FilePath");
                }
                if (HttpContext.Session.TryGetValue("Filename", out byte[] val1))
                {
                    FileName = HttpContext.Session.GetObject<string>("Filename");
                    HttpContext.Session.Remove("Filename");
                }
                if (FilePath != null)
                {
                    byte[] fileBytes = System.IO.File.ReadAllBytes(FilePath);
                    System.IO.File.WriteAllBytes(FilePath, fileBytes);
                    MemoryStream ms = new MemoryStream(fileBytes);
                    return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, FileName);

                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        [HttpPost]
        public ActionResult SetFileName([FromBody] RTGSReconciliationDashBoardBO objBO)
        {

            string res = string.Empty;
            string FilePath = "";

            try
            {
                FilePath = Startup.Configuration["AppSettings:RecoUploadPath"].ToString() + objBO.FileName;

                if (HttpContext.Session.TryGetValue("FilePath", out byte[] val))
                {
                    HttpContext.Session.Remove("FilePath");
                }
                HttpContext.Session.SetObject("FilePath", FilePath);
                if (HttpContext.Session.TryGetValue("Filename", out byte[] val1))
                {
                    HttpContext.Session.Remove("Filename");
                }
                HttpContext.Session.SetObject("Filename", objBO.FileName);

                return Json(new { Status = "1", Message = "Success" });


            }
            catch (Exception ex)
            {
                throw ex;

            }

        }

    }
}