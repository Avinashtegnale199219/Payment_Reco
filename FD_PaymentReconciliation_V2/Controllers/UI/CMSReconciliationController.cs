using System;
using System.Collections.Generic;
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
    public class CMSReconciliationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Process_Reco([FromBody]ReconciliationProcessBO objBO)
        {
            string res = string.Empty;
            SessionBO objSessionBo = HttpContext.Session.GetObject<SessionBO>("UserSessionDetails");
            //SessionBO objSessionBo = HttpContext.Session.GetObject<SessionBO>("UserSessionDetails");
            //objSessionBo.FormCode = objSessionBo["SubModCode"] != null ? objSessionBo["SubModCode"].ToString() : "";
            ReconciliationProcessBAL objBAL = new ReconciliationProcessBAL();
            try
            {
                return Json(objBAL.Proccess_Reco(objBO, objSessionBo, "CMS"));
                
            }
            catch (Exception ex)
            {
                //ExceptionUtility.LogExceptionAsync(ex);
                return Json(JsonConvert.SerializeObject(new { Status = "0", Message = "Error", Data = "Something went wrong !" }));
                
            }
        }

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
                throw ex;
            }
            //return data ;

        }

    }
}