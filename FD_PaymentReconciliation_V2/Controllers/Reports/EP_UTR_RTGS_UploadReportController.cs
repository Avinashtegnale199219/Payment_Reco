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

namespace FD_PaymentReconciliation_V2.Controllers.Reports
{
    public class EP_UTR_RTGS_UploadReportController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public ActionResult GetUTR_RTGS_UploadData()
        {
            string res = string.Empty;
            EP_UTR_RTGS_UploadReportDAL objDAL = new EP_UTR_RTGS_UploadReportDAL();
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

        //[HttpPost]
        //public ActionResult GetReconUploadData_File([FromBody]CMSCollectionRecoProcessDashBoardBO objBO)
        //{
        //    string res = string.Empty;
        //    string FilePath = "";
        //    EP_UTR_RTGS_UploadReportDAL objDAL = new EP_UTR_RTGS_UploadReportDAL();
        //    SessionBO objSessionBo = HttpContext.Session.GetObject<SessionBO>("UserSessionDetails");
        //    try
        //    {
        //        using (DataSet dataSet = objDAL.GetReconUploadData(objBO.HdrSeq))
        //        {
        //            if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
        //            {

        //                string filename = "UploadData_" + objBO.HdrSeq + "_" + DateTime.Now.Ticks.ToString();
        //                FilePath = Startup.Configuration["AppSettings:RecoUploadPath"].ToString() + filename + ".xlsx";// ConfigurationManager.AppSettings["RecoUploadPath"].ToString() + filename + ".xlsx";
        //                Export2Excel.DataTableToBytesArray(dataSet.Tables[0], filename, FilePath);
        //                filename = filename + ".xlsx";
        //                if (System.IO.File.Exists(FilePath))
        //                {
        //                    if (HttpContext.Session.TryGetValue("FilePath", out byte[] val))
        //                    {
        //                        HttpContext.Session.Remove("FilePath");
        //                    }
        //                    HttpContext.Session.SetObject("FilePath", FilePath);
        //                    if (HttpContext.Session.TryGetValue("Filename", out byte[] val1))
        //                    {
        //                        HttpContext.Session.Remove("Filename");
        //                    }
        //                    HttpContext.Session.SetObject("Filename", filename);
        //                }

        //                return Json(new { Status = "1", Message = "Success", Data = filename });
        //            }
        //            else
        //            {
        //                throw new Exception("No Data Found..!");

        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        ExceptionUtility.LogExceptionAsync(ex);
        //        return Json(new { Status = "0", Message = "Error", Data = "Something Went Wrong..!" });
        //    }

        //}

        public async Task<IActionResult> ViewFile(string HdrSeq)
        {
            try
            {

                EP_UTR_RTGS_UploadReportDAL objDAL = new EP_UTR_RTGS_UploadReportDAL();
                string base64String = string.Empty;
                using (DataTable dt = objDAL.GetReconUploadData(HdrSeq))
                {
                    if (dt != null && dt.Rows.Count > 0 && dt.Rows[0]["f_FILE_PATH"].ToString() != "")
                    {
                        byte[] imageBytes = System.IO.File.ReadAllBytes(dt.Rows[0]["f_FILE_PATH"].ToString()+ dt.Rows[0]["f_FileName"].ToString());
                        base64String = Convert.ToBase64String(imageBytes);
                        return Json(new { Status = "1", Message = "Success", Data = base64String });
                        //var path = dt.Rows[0]["f_FILE_PATH"].ToString() + dt.Rows[0]["f_FileName"].ToString();
                        //var memory = new MemoryStream();
                        //using (var stream = new FileStream(path, FileMode.Open))
                        //{
                        //    await stream.CopyToAsync(memory);
                        //}
                        //memory.Position = 0;
                        //return File(memory, GetContentType(path), Path.GetFileName(path));

                    }
                    return Json(new { Status = "1", Message = "Success", Data = base64String });
                }
            }
            catch(Exception ex)
            {
                ExceptionUtility.LogExceptionAsync(ex);
                return Json(new { Status = "0", Message = "Error", Data = "Something went wrong." });
            }
        }
        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                //{".doc", "application/vnd.ms-word"},
                //{".docx", "application/vnd.ms-word"},
                //{".xls", "application/vnd.ms-excel"},
                               {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                //{".csv", "text/csv"}
            };
        }
    }
}