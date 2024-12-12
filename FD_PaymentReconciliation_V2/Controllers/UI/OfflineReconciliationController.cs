using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Extension;
using FD_PaymentReconciliation_V2.App_Code.BusinessLayer;
using FD_PaymentReconciliation_V2.App_Code.BusinessObject;
using FD_PaymentReconciliation_V2.App_Code.DataAccessLayer;
using FD_PaymentReconciliation_V2.BusinessObject;
using FD_PaymentReconciliation_V2.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FD_PaymentReconciliation_V2.Controllers.UI
{
    public class OfflineReconciliationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Get_TemplateType()
        {
            string res = string.Empty;
            ReconciliationDAL objDAL = new ReconciliationDAL();
            try
            {
                using (DataSet dataSet = objDAL.Get_Template_Type("Offline"))
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
                return Json(JsonConvert.SerializeObject(new { Status = "0", Message = "Error", Data = "Something went wrong !" }));
            }
           
        }

        [HttpPost]
        public ActionResult Process_Reco([FromBody] ReconciliationProcessBO objBO)
        {
            string res = string.Empty;
            SessionBO objSessionBo = HttpContext.Session.GetObject<SessionBO>("UserSessionDetails");
           
            ReconciliationProcessBAL objBAL = new ReconciliationProcessBAL();
            try
            {

                return Json(objBAL.Proccess_Reco(objBO, objSessionBo, "Offline"));

            }
            catch (Exception ex)
            {
                ExceptionUtility.LogExceptionAsync(ex);
                return Json(JsonConvert.SerializeObject(new { Status = "0", Message = "Error", Data = "Something went wrong !" }));
            }
            // return res;

        }


        //[HttpPost]
        //public ActionResult Upload_File()
        //{
        //    string result = string.Empty;
        //    string data = string.Empty;
        //    try
        //    {
        //        // long size = 0;
        //        var file = Request.Form.Files;
        //        var filename = ContentDispositionHeaderValue.Parse(file[0].ContentDisposition).FileName.Trim('"');
        //        string[] ext = filename.Split('.');

        //        if (file.Count > 0)
        //        {                    
        //            string FilePath = Startup.Configuration["AppSettings:RecoUploadPath"].ToString() + filename + "." + ext[1].ToLower();
        //            using (FileStream fs = System.IO.File.Create(FilePath))
        //            {
        //                file[0].CopyTo(fs);
        //                fs.Flush();
        //            }
        //            //return Json(new { Status = "1", Message = "Sucess", Data = filename});

        //            return Json(new { Status = "1", Message = "Sucess", Data = filename + "." + ext[1].ToLower() });
        //        }
        //        else
        //        {
        //            return Json(new { Status = "0", Message = "Error", Data = "No File found to upload..!" });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}

        [HttpPost]
        public ActionResult Upload_File()
        {
            string result = string.Empty;
            string data = string.Empty;
            try
            {
                var file = Request.Form.Files;
                var filename = ContentDispositionHeaderValue.Parse(file[0].ContentDisposition).FileName.Trim('"');
                string[] ext = filename.Split('.');

                if (file.Count > 0)
                {
                    string fileName = ("Reco_" + DateTime.Now.Ticks.ToString());
                    string FilePath = Startup.Configuration["AppSettings:RecoUploadPath"].ToString() + fileName + "." + ext[1].ToLower();/* ".xlsx"*/;

                    using (FileStream fs = System.IO.File.Create(FilePath))
                    {
                        file[0].CopyTo(fs);
                        fs.Flush();
                    }
                    //return Json(new { Status = "1", Message = "Sucess", Data = filename});
                    //result = "Success";
                    //data =filename;
                    return Json(new { Status = "1", Message = "Sucess", Data = fileName + "." + ext[1].ToLower() });
                }
                else
                {
                    return Json(new { Status = "0", Message = "Error", Data = "No File found to upload..!" });
                }



            }
            catch (Exception ex)
            {
                //ExceptionUtility.LogExceptionAsync(ex);
                return Json(new { Status = "0", Message = "Error", Data = "Something went wrong !" });
            }
            //return data ;

        }


    }


}
