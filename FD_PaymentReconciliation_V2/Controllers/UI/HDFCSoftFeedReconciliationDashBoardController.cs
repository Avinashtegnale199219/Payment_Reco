using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Extension;
using FD_PaymentReconciliation_V2.App_Code.DataAccessLayer;
using FD_PaymentReconciliation_V2.BusinessObject;
using FD_PaymentReconciliation_V2.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FD_PaymentReconciliation_V2.Controllers.UI
{
    public class HDFCSoftFeedReconciliationDashBoardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GetReconciliationData()
        {
            HDFCSoftFeedReconProcessDashBoardDAL objDAL = new HDFCSoftFeedReconProcessDashBoardDAL();
            try
            {
                using (DataSet dataSet = objDAL.GetReconciliationData())
                {
                    if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                    {
                        return Json(new { Status = "1", Message = "Success", Data = dataSet.Tables[0] });
                    }
                    else
                    {
                        return Json(new { Status = "0", Message = "Error", Data = "No Data Found" });
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
        public ActionResult GetReconUploadDataFileByHdrSeq([FromBody] string HdrSeq)
        {
            string res = string.Empty;
            string FilePath = "";
            HDFCSoftFeedReconProcessDashBoardDAL objDAL = new HDFCSoftFeedReconProcessDashBoardDAL();
            SessionBO SBO = HttpContext.Session.GetObject<SessionBO>("UserSessionDetails");
            
            try
            {
                using (DataSet dataSet = objDAL.GetReconUploadData(HdrSeq))
                {
                    if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                    {

                        string filename = "UploadData_" + HdrSeq + "_" + DateTime.Now.Ticks.ToString();
                        FilePath = Startup.Configuration["AppSettings:RecoUploadPath"].ToString() + filename + ".xlsx";
                        Export2Excel.DataTableToBytesArray(dataSet.Tables[0], filename, FilePath);
                        filename = filename + ".xlsx";
                        if (System.IO.File.Exists(FilePath))
                        {
                            objDAL.UpdateReconHdrUploadedFileDetails(HdrSeq, filename, FilePath, SBO);
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
                return Json(new { Status = "0", Message = "Error", Data = "Something Went Wrong..!" });
            }
            
        }

        [HttpPost]
        public ActionResult GetReconSuccessDataFileByHdrSeq([FromBody] string HdrSeq)
        {

            string res = string.Empty;
            string FilePath = "";
            HDFCSoftFeedReconProcessDashBoardDAL objDAL = new HDFCSoftFeedReconProcessDashBoardDAL();
            SessionBO SBO = HttpContext.Session.GetObject<SessionBO>("UserSessionDetails");

            try
            {
                using (DataSet dataSet = objDAL.GetReconSuccessData(HdrSeq))
                {
                    if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                    {

                        string filename = "SuccessData_" + HdrSeq + "_" + DateTime.Now.Ticks.ToString();
                        FilePath = Startup.Configuration["AppSettings:RecoUploadPath"].ToString() + filename + ".xlsx";
                        Export2Excel.DataTableToBytesArray(dataSet.Tables[0], filename, FilePath);
                        filename = filename + ".xlsx";
                        if (System.IO.File.Exists(FilePath))
                        {
                            objDAL.UpdateReconHdrSuccessFileDetails(HdrSeq, filename, FilePath, SBO);
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
                return Json(new { Status = "0", Message = "Error", Data = "Something Went Wrong..!" });
            }
            
        }
        [HttpPost]
        public ActionResult GetReconExceptionDataFileByHdrSeq([FromBody] string HdrSeq)
        {
            string res = string.Empty;
            string FilePath = "";
            HDFCSoftFeedReconProcessDashBoardDAL objDAL = new HDFCSoftFeedReconProcessDashBoardDAL();
            SessionBO SBO = HttpContext.Session.GetObject<SessionBO>("UserSessionDetails");
            try
            {
                using (DataSet dataSet = objDAL.GetReconExceptionData(HdrSeq))
                {
                    if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                    {

                        string filename = "ExceptionData_" + HdrSeq + "_" + DateTime.Now.Ticks.ToString();
                        FilePath = Startup.Configuration["AppSettings:RecoUploadPath"].ToString() + filename + ".xlsx";
                        Export2Excel.DataTableToBytesArray(dataSet.Tables[0], filename, FilePath);
                        filename = filename + ".xlsx";
                        if (System.IO.File.Exists(FilePath))
                        {
                            objDAL.UpdateReconHdrExceptionFileDetails(HdrSeq, filename, FilePath, SBO);
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
                return Json(new { Status = "0", Message = "Error", Data = "Something Went Wrong..!" });
            }
           
        }

        [HttpPost]
        public ActionResult CancelReconciliationProcess([FromBody] string HdrSequence)
        {
            string res = string.Empty;
            HDFCSoftFeedReconProcessDashBoardDAL objDAL = new HDFCSoftFeedReconProcessDashBoardDAL();
            SessionBO SBO = HttpContext.Session.GetObject<SessionBO>("UserSessionDetails");
            
            try
            {

                int recordsAffected = objDAL.CancelReconciliationProcess(HdrSequence, SBO);
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
                return Json(new { Status = "0", Message = "Error", Data = "Something Went Wrong..!" });
            }
            
        }

        [HttpPost]
        public ActionResult ProcessReconciliationData([FromBody] string HdrSequence)
        {
            string res = string.Empty;
            HDFCSoftFeedReconProcessDashBoardDAL objDAL = new HDFCSoftFeedReconProcessDashBoardDAL();
            SessionBO SBO = HttpContext.Session.GetObject<SessionBO>("UserSessionDetails");
            
            try
            {
                int recordsAffected = objDAL.ProcessReconciliationData(HdrSequence, SBO);
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
                return Json(new { Status = "0", Message = "Error", Data = "Something Went Wrong..!" });
            }
            
        }
        [HttpPost]
        public ActionResult SetFileName([FromBody] string FileName)
        {

            string res = string.Empty;
            string FilePath = "";

            try
            {
                FilePath = Startup.Configuration["AppSettings:RecoUploadPath"].ToString() + FileName;

                if (HttpContext.Session.TryGetValue("FilePath", out byte[] val))
                {
                    HttpContext.Session.Remove("FilePath");
                }
                HttpContext.Session.SetObject("FilePath", FilePath);
                if (HttpContext.Session.TryGetValue("Filename", out byte[] val1))
                {
                    HttpContext.Session.Remove("Filename");
                }
                HttpContext.Session.SetObject("Filename", FileName);

                return Json(new { Status = "1", Message = "Success" });


            }
            catch (Exception ex)
            {
                ExceptionUtility.LogExceptionAsync(ex);
                return Json(new { Status = "0", Message = "Error", Data = "Something Went Wrong..!" });

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
    }
}