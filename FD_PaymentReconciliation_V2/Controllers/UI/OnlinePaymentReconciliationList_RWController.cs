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

namespace FD_PaymentReconciliation_V2.Controllers.UI
{
    public class OnlinePaymentReconciliationList_RWController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public ActionResult BindTechProcessGrid()
        {
            try
            {
                SessionBO objSessionBo = HttpContext.Session.GetObject<SessionBO>("UserSessionDetails");
                OnlinePaymentReconciliationList_RW_DAL objDAL = new OnlinePaymentReconciliationList_RW_DAL();
                DataSet dataSet = objDAL.GetTechProcessData(Convert.ToString(objSessionBo.Entity_Id));
                if (dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                {
                    return Json(new { Status = "1", Message = "Success", Data = dataSet.Tables[0] });
                }
                else
                {

                    return Json(new { Status = "0", Message = "Error", Data = "Not Found" });
                }

            }
            catch (Exception ex)
            {
                ExceptionUtility.LogExceptionAsync(ex);
                return Json(new { Status = "0", Message = "Error", Data = "Something went wrong !" });
            }
        }



        [HttpPost]
        public ActionResult GetUploadDataFileByHdrSeq([FromBody]OnlinePaymentReconciliationList_RWBO objbo)
        {
            string res = string.Empty;
            string FilePath = "";
            OnlinePaymentReconciliationList_RW_DAL objDAL = new OnlinePaymentReconciliationList_RW_DAL();
            SessionBO objSessionBo = HttpContext.Session.GetObject<SessionBO>("UserSessionDetails");
            try
            {
                using (DataSet dataSet = objDAL.GetTechprocessUploadedFileDownload(objbo.HdrSeq))
                {
                    if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                    {

                        string filename = "OnlinePayment_FileUploaded_" + objbo.HdrSeq + "_" + DateTime.Now.Ticks.ToString();
                        FilePath = Startup.Configuration["AppSettings:RecoUploadPath"].ToString() + filename + ".xlsx";// ConfigurationManager.AppSettings["RecoUploadPath"].ToString() + filename + ".xlsx";
                        Export2Excel.SaveDataTableToExcel(FilePath, dataSet.Tables[0]);
                        filename = filename + ".xlsx";
                        if (System.IO.File.Exists(FilePath))
                        {

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
        public ActionResult GetAlreadyPresentDataFileByHdrSeq([FromBody]OnlinePaymentReconciliationList_RWBO objbo)
        {
            string res = string.Empty;
            string FilePath = "";
            OnlinePaymentReconciliationList_RW_DAL objDAL = new OnlinePaymentReconciliationList_RW_DAL();
            SessionBO objSessionBo = HttpContext.Session.GetObject<SessionBO>("UserSessionDetails");
            try
            {
                using (DataSet dataSet = objDAL.GetTechProcess_AlreadyExistsData(objbo.HdrSeq))
                {
                    if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                    {

                        string filename = "OnlinePayment_AlreadyPresent_" + objbo.HdrSeq + "_" + DateTime.Now.Ticks.ToString();
                        FilePath = Startup.Configuration["AppSettings:RecoUploadPath"].ToString() + filename + ".xlsx";// ConfigurationManager.AppSettings["RecoUploadPath"].ToString() + filename + ".xlsx";
                        Export2Excel.SaveDataTableToExcel(FilePath, dataSet.Tables[0]);
                        filename = filename + ".xlsx";
                        if (System.IO.File.Exists(FilePath))
                        {

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
                return Json(new { Status = "0", Message = "Error", Data = "Something Went Wrong..!" });
            }

        }

        [HttpPost]
        public ActionResult GetReconExceptionDataFileByHdrSeq([FromBody]OnlinePaymentReconciliationList_RWBO objbo)
        {

            string res = string.Empty;
            string FilePath = "";
            OnlinePaymentReconciliationList_RW_DAL objDAL = new OnlinePaymentReconciliationList_RW_DAL();
            SessionBO objSessionBo = HttpContext.Session.GetObject<SessionBO>("UserSessionDetails");
            try
            {
                using (DataSet dataSet = objDAL.GetTechProcess_ExceptionData(objbo.HdrSeq))
                {
                    if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                    {

                        string filename = "TPSL_Exception_" + objbo.HdrSeq + "_" + DateTime.Now.Ticks.ToString();
                        FilePath = Startup.Configuration["AppSettings:RecoUploadPath"].ToString() + filename + ".xlsx";
                        Export2Excel.SaveDataTableToExcel(FilePath, dataSet.Tables[0]);
                        filename = filename + ".xlsx";
                        if (System.IO.File.Exists(FilePath))
                        {

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
                return Json(new { Status = "0", Message = "Error", Data = "Something Went Wrong..!" });
            }

        }

        [HttpPost]
        public ActionResult GetReconSuccessDataFileByHdrSeq([FromBody]OnlinePaymentReconciliationList_RWBO objbo)
        {

            string res = string.Empty;
            string FilePath = "";

            SessionBO objSessionBo = HttpContext.Session.GetObject<SessionBO>("UserSessionDetails");

            try
            {

                string filename = "SuccessData_" + objbo.HdrSeq + "_" + DateTime.Now.Ticks.ToString();
                FilePath = Startup.Configuration["AppSettings:RecoUploadPath"].ToString() + filename + ".xlsx";

                filename = filename + ".xlsx";
                if (System.IO.File.Exists(FilePath))
                {

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
                return Json(new { Status = "1", Message = "Success", Data = FilePath });

            }
            catch (Exception ex)
            {
                return Json(new { Status = "0", Message = "Error", Data = "Something Went Wrong..!" });
            }

        }
        [HttpPost]
        public ActionResult CancelReconciliationProcess([FromBody]OnlinePaymentReconciliationList_RWBO objBO)
        {
            string res = string.Empty;
            OnlinePaymentReconciliationList_RW_DAL objDAL = new OnlinePaymentReconciliationList_RW_DAL();
            SessionBO objSessionBo = HttpContext.Session.GetObject<SessionBO>("UserSessionDetails");
            try
            {

                int recordsAffected = Convert.ToInt32(objDAL.CancelFileUploadHdrSeq(objBO.HdrSequence, objSessionBo));
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
                return Json(new { Status = "0", Message = "Error", Data = "Something Went Wrong..!" });
            }

        }

        [HttpPost]
        public ActionResult ProcessReconciliationData([FromBody]OnlinePaymentReconciliationList_RWBO objbo)
        {
            string res = string.Empty;
            OnlinePaymentReconciliationList_RW_DAL objDAL = new OnlinePaymentReconciliationList_RW_DAL();
            SessionBO objSessionBo = HttpContext.Session.GetObject<SessionBO>("UserSessionDetails");
            try
            {

                DataSet ds = objDAL.updateIsProcesshdrSeq(objbo.HdrSequence, objSessionBo);
                //DataTable dt = objDAL.updateIsProcesshdrSeq(objBO.HdrSequence);
                //if (dt != null && dt.Rows.Count > 0)
                //{
                //    foreach (DataRow dr in dt.Rows)
                //    {
                //        objDAL.UpdateFDNewPaid(dr["Appl_No"].ToString(), Convert.ToDateTime(dr["Payment_Date"]));
                //    }
                //}

                return Json(new { Status = "1", Message = "Success", Data = "Moved Successfully" });


            }
            catch (Exception ex)
            {
                return Json(new { Status = "0", Message = "Error", Data = "Something Went Wrong..!" });
            }

        }

        [HttpPost]
        public ActionResult GetRecordRectificationByHdrSeq([FromBody]OnlinePaymentReconciliationList_RWBO objbo)
        {

            string res = string.Empty;
            string FilePath = "";
            OnlinePaymentReconciliationList_RW_DAL objDAL = new OnlinePaymentReconciliationList_RW_DAL();
            SessionBO objSessionBo = HttpContext.Session.GetObject<SessionBO>("UserSessionDetails");
            try
            {
                using (DataSet dataSet = objDAL.GetTechProcess_RectificationData(objbo.HdrSeq))
                {
                    if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                    {

                        string filename = "OnlinePayment_RectificationData_" + objbo.HdrSeq + "_" + DateTime.Now.Ticks.ToString();
                        FilePath = Startup.Configuration["AppSettings:RecoUploadPath"].ToString() + filename + ".xlsx";
                        Export2Excel.SaveDataTableToExcel(FilePath, dataSet.Tables[0]);
                        filename = filename + ".xlsx";
                        if (System.IO.File.Exists(FilePath))
                        {

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
                return Json(new { Status = "0", Message = "Error", Data = "Something Went Wrong..!" });
            }

        }






        [HttpGet]
        public FileResult GetUploadData()
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
                    if (System.IO.File.Exists(FilePath))
                    {
                        System.IO.File.Delete(FilePath);
                    }
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
        public FileResult GetUploadException()
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
                    if (System.IO.File.Exists(FilePath))
                    {
                        System.IO.File.Delete(FilePath);
                    }
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
        public FileResult GetAlreadyPresent()
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
                    if (System.IO.File.Exists(FilePath))
                    {
                        System.IO.File.Delete(FilePath);
                    }
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
        public FileResult GetRectification()
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
                    if (System.IO.File.Exists(FilePath))
                    {
                        System.IO.File.Delete(FilePath);
                    }
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
        public FileResult GetUploadSuccess()
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
                if (FilePath != null && FilePath != "")
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
