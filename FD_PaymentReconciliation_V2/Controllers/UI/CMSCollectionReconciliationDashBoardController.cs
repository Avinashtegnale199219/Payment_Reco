using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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
    public class CMSCollectionReconciliationDashBoardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public ActionResult GetReconciliationData()
        {
            string res = string.Empty;
            CMSCollectionRecoProcessDashBoardDAL objDAL = new CMSCollectionRecoProcessDashBoardDAL();
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
        public ActionResult GetReconUploadDataFileByHdrSeq([FromBody]CMSCollectionRecoProcessDashBoardBO objBO)
        {
            string res = string.Empty;
            string FilePath = "";
            CMSCollectionRecoProcessDashBoardDAL objDAL = new CMSCollectionRecoProcessDashBoardDAL();
            SessionBO objSessionBo = HttpContext.Session.GetObject<SessionBO>("UserSessionDetails");
            try
            {
                using (DataSet dataSet = objDAL.GetReconUploadData(objBO.HdrSeq))
                {
                    if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                    {

                        string filename = "UploadData_" + objBO.HdrSeq + "_" + DateTime.Now.Ticks.ToString();
                        FilePath = Startup.Configuration["AppSettings:RecoUploadPath"].ToString() + filename + ".xlsx";// ConfigurationManager.AppSettings["RecoUploadPath"].ToString() + filename + ".xlsx";
                        Export2Excel.DataTableToBytesArray(dataSet.Tables[0], filename, FilePath);
                        filename = filename + ".xlsx";
                        if (System.IO.File.Exists(FilePath))
                        {
                            objDAL.UpdateReconHdrUploadedFileDetails(objBO.HdrSeq, filename, FilePath, objSessionBo);
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
        public ActionResult GetReconExceptionDataFileByHdrSeq([FromBody]CMSCollectionRecoProcessDashBoardBO ObjBO)
        {

            string res = string.Empty;
            string FilePath = "";
            CMSCollectionRecoProcessDashBoardDAL objDAL = new CMSCollectionRecoProcessDashBoardDAL();
            SessionBO objSessionBo = HttpContext.Session.GetObject<SessionBO>("UserSessionDetails");
            try
            {
                using (DataSet dataSet = objDAL.GetReconExceptionData(ObjBO.HdrSeq))
                {
                    if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                    {

                        string filename = "ExceptionData_" + ObjBO.HdrSeq + "_" + DateTime.Now.Ticks.ToString();
                        FilePath = Startup.Configuration["AppSettings:RecoUploadPath"].ToString() + filename + ".xlsx";// ConfigurationManager.AppSettings["RecoUploadPath"].ToString() + filename + ".xlsx";
                        Export2Excel.DataTableToBytesArray(dataSet.Tables[0], filename, FilePath);
                        filename = filename + ".xlsx";
                        if (System.IO.File.Exists(FilePath))
                        {
                            objDAL.UpdateReconHdrExceptionFileDetails(ObjBO.HdrSeq, filename, FilePath, objSessionBo);
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
        public ActionResult GetReconSuccessDataFileByHdrSeq([FromBody]CMSCollectionRecoProcessDashBoardBO ObjBO)
        {

            string res = string.Empty;
            string FilePath = "";
            CMSCollectionRecoProcessDashBoardDAL objDAL = new CMSCollectionRecoProcessDashBoardDAL();
            SessionBO objSessionBo = HttpContext.Session.GetObject<SessionBO>("UserSessionDetails");

            try
            {
                using (DataSet dataSet = objDAL.GetReconSuccessData(ObjBO.HdrSeq))
                {
                    if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                    {

                        string filename = "SuccessData_" + ObjBO.HdrSeq + "_" + DateTime.Now.Ticks.ToString();
                        FilePath = Startup.Configuration["AppSettings:RecoUploadPath"].ToString() + filename + ".xlsx";// ConfigurationManager.AppSettings["RecoUploadPath"].ToString() + filename + ".xlsx";
                        Export2Excel.DataTableToBytesArray(dataSet.Tables[0], filename, FilePath);
                        filename = filename + ".xlsx";
                        if (System.IO.File.Exists(FilePath))
                        {
                            objDAL.UpdateReconHdrSuccessFileDetails(ObjBO.HdrSeq, filename, FilePath, objSessionBo);
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
        public ActionResult CancelReconciliationProcess([FromBody]CMSCollectionRecoProcessDashBoardBO ObjBO)
        {
            string res = string.Empty;
            CMSCollectionRecoProcessDashBoardDAL objDAL = new CMSCollectionRecoProcessDashBoardDAL();
            SessionBO objSessionBo = HttpContext.Session.GetObject<SessionBO>("UserSessionDetails");
           
            try
            {

                int recordsAffected = objDAL.CancelReconciliationProcess(ObjBO.HdrSequence, objSessionBo);
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
        public ActionResult ProcessReconciliationData([FromBody]CMSCollectionRecoProcessDashBoardBO ObjBO)
        {
            string res = string.Empty;
            SessionBO objSessionBo = HttpContext.Session.GetObject<SessionBO>("UserSessionDetails");
            CMSCollectionRecoProcessDashBoardDAL objDAL = new CMSCollectionRecoProcessDashBoardDAL();            
           
            try
            {

                int recordsAffected = objDAL.ProcessReconciliationData(ObjBO.HdrSequence, objSessionBo);
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
        public ActionResult SetFileName([FromBody] CMSCollectionRecoProcessDashBoardBO objBO)
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