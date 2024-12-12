using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Extension;
using FD_PaymentReconciliation_V2.App_Code.BusinessLayer;
using FD_PaymentReconciliation_V2.App_Code.BusinessObject;
using FD_PaymentReconciliation_V2.BusinessObject;
using Microsoft.AspNetCore.Mvc;

namespace FD_PaymentReconciliation_V2.Controllers.UI
{
    public class CMSCollectionReconciliationController : Controller
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

                    if (ext[1].ToUpper() == "XLSX" || ext[1].ToUpper() == "XLS" || ext[1].ToUpper() == "CSV")
                    {
                        string fileName = ("Reco_" + DateTime.Now.Ticks.ToString());
                        string fileLocation = Startup.Configuration["AppSettings:RecoUploadPath"].ToString() + fileName + "." + ext[1].ToLower();
                        size += file[0].Length;
                        using (FileStream fs = System.IO.File.Create(fileLocation))
                        {
                            file[0].CopyTo(fs);
                            fs.Flush();
                        }
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
                throw ex;
            }

        }

        [HttpPost]
        public ActionResult Process_Reco([FromBody] ReconciliationProcessBO objBO)
        {
            try
            {
                SessionBO objSessionBo = HttpContext.Session.GetObject<SessionBO>("UserSessionDetails");
                ReconciliationProcessBAL objBAL = new ReconciliationProcessBAL();
                return Json(objBAL.Proccess_Reco(objBO, objSessionBo, "CMS_Collection"));

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}