﻿using System;
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

namespace FD_PaymentReconciliation_V2.Controllers.UI
{
    public class BankRevalidationReconciliationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Upload_File()
        {
            string result = string.Empty;
            try
            {
                long size = 0;
                var file = Request.Form.Files;
                if (file.Count > 0)
                {
                    var filename = ContentDispositionHeaderValue.Parse(file[0].ContentDisposition).FileName.Trim('"');
                    string[] ext = filename.Split('.');

                    if (ext[1].ToUpper() == "XLSX" || ext[1].ToUpper() == "XLS")
                    {
                        string fileName = ("Reco_" + DateTime.Now.Ticks.ToString());
                        string fileLocation = Startup.Configuration["AppSettings:RecoUploadPath"].ToString() + fileName + "." + ext[1].ToLower();
                        size += file[0].Length;
                        using (FileStream fs = System.IO.File.Create(fileLocation))
                        {
                            file[0].CopyTo(fs);
                            fs.Flush();
                        }
                        return Json(new { Status = "1", Message = "Success", Data = fileName + "." + ext[1].ToLower() });
                    }
                    else
                    {
                        return Json(new { Status = "0", Message = "Error", Data = "File type is not valid..!" });
                    }
                }
                else
                {
                    return Json(new { Status = "0", Message = "Error", Data = "No File found to upload..!" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public ActionResult Get_TemplateType()
        {
            string res = string.Empty;
            ReconciliationDAL objDAL = new ReconciliationDAL();
            try
            {
                using (DataSet dataSet = objDAL.Get_Template_Type("BankRevalidation"))
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
        public ActionResult Proccess_Reco([FromBody] ReconciliationProcessBO objBO)
        {           
            SessionBO SBO = HttpContext.Session.GetObject<SessionBO>("UserSessionDetails");
            SBO.FormCode = Convert.ToString(SBO.FormCode);
            ReconciliationProcessBAL objBAL = new ReconciliationProcessBAL();
            try
            {
                return Json(objBAL.Proccess_Reco(objBO, SBO, "BankRevalidation"));
            }
            catch (Exception ex)
            {
                ExceptionUtility.LogExceptionAsync(ex);
                return Json(new { Status = "0", Message = "Error", Data = "Something went wrong..!" });
            }
          
        }

        [HttpPost]
        public ActionResult ProcessBulkFile([FromBody] ReconciliationProcessBO objBO)
        {
            SessionBO SBO = HttpContext.Session.GetObject<SessionBO>("UserSessionDetails");
            SBO.FormCode = Convert.ToString(SBO.FormCode);
            ReconciliationProcessBAL objBAL = new ReconciliationProcessBAL();
            try
            {
                return Json(objBAL.Proccess_Reco(objBO, SBO, "BankRevalidation"));
            }
            catch (Exception ex)
            {
                ExceptionUtility.LogExceptionAsync(ex);
                return Json(new { Status = "0", Message = "Error", Data = "Something went wrong..!" });
            }

        }
    }
}