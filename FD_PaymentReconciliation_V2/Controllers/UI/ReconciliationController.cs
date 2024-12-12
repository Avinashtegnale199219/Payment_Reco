using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Threading.Tasks;
using Extension;
using FD_PaymentReconciliation_V2.App_Code.BusinessLayer;
using FD_PaymentReconciliation_V2.App_Code.BusinessObject;
using FD_PaymentReconciliation_V2.App_Code.DataAccessLayer;
using FD_PaymentReconciliation_V2.BusinessObject;
using FD_PaymentReconciliation_V2.Services;
//using Impersonate;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FD_PaymentReconciliation_V2.Controllers.UI
{
    public class ReconciliationController : Controller
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
                //ImpersonateUser _imp = new ImpersonateUser();
                if (file.Count > 0)
                {
                    var filename = ContentDispositionHeaderValue.Parse(file[0].ContentDisposition).FileName.Trim('"');
                    string[] ext = filename.Split('.');

                    if (ext[1].ToUpper() == "XLSX" || ext[1].ToUpper() == "XLS" || ext[1].ToUpper() == "CSV")
                    {
                        string fileName = ("Reco_" + DateTime.Now.Ticks.ToString());
                        string fileLocation = Startup.Configuration["AppSettings:RecoUploadPath"].ToString() + fileName + "." + ext[1].ToLower();
                        size += file[0].Length;
                        //         WindowsIdentity.RunImpersonated(_imp.Login("MMFSL", "1000003931", "Fddoc1234"),
                        //() =>
                        //{

                        using (var stream = new FileStream(fileLocation, FileMode.Create))
                        {
                            file[0].CopyTo(stream);
                        }
                        // });


                        //using (FileStream fs = System.IO.File.Create(fileLocation))
                        //{
                        //    file[0].CopyTo(fs);
                        //    fs.Flush();
                        //}
                       
                        return Json(new { Status = "1", Message = "Sucess", Data = fileName + "." + ext[1].ToLower() });
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
                ExceptionUtility.LogExceptionAsync(ex);
                return Json(new { Status = "0", Message = "Error", Data = "Something went wrong !" });
            }

        }

        [HttpGet]
        public ActionResult Get_TemplateType()
        {
            try
            {
                ReconciliationProcessDAL objDAL = new ReconciliationProcessDAL();
                using (DataSet dataSet = objDAL.Get_Template_Type("Online"))
                {
                    if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                    {
                        return Json(new { Status = "1", Message = "Success", Data = dataSet.Tables[0] });
                    }
                    else
                    {
                        return Json(new { Status = "0", msg = "No Record Found", DataSet = string.Empty });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult Proccess_Reco([FromBody] ReconciliationProcessBO objBO)
        {
            
            try
            {
                DataTable dt = null;
                SessionBO objSessionBo = HttpContext.Session.GetObject<SessionBO>("UserSessionDetails");
                ReconciliationProcessBAL objBAL = new ReconciliationProcessBAL();
                return Json(objBAL.Proccess_Reco(objBO, objSessionBo, "Online"));
                //res = objBAL.Proccess_Reco(objBO, objSessionBo, "Online");
            }
            catch (Exception ex)
            {
                //ExceptionUtility.LogExceptionAsync(ex);
                return Json(JsonConvert.SerializeObject(new { Status = "0", Message = "Error", Data = "Something went wrong !" }));

            }

        }
    }
}