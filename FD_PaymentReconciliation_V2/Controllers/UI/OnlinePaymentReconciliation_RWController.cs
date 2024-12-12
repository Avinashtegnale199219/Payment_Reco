using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Extension;
using FD_CP_MIS_VIEW.App_Code.BusinessObject;
using FD_CP_MIS_VIEW.App_Code.DataAccessLayer;
using FD_PaymentReconciliation_V2.App_Code.BusinessObject;
using FD_PaymentReconciliation_V2.App_Code.DataAccessLayer;
using FD_PaymentReconciliation_V2.BusinessObject;
using FD_PaymentReconciliation_V2.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FD_PaymentReconciliation_V2.Controllers.UI
{
    public class OnlinePaymentReconciliation_RWController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        //protected void btn_UploadFile_Click([FromHeader]fileinfo fileinfo)
        //{
        //   // string txt_remarks;
        //   // file FileUpload1;
        //    string Message = string.Empty;
        //    string Error = string.Empty;
        //    try
        //    {

        //        SessionBO objSessionBo = HttpContext.Session.GetObject<SessionBO>("UserSessionDetails");
        //      //  string fileExtension = Path.GetExtension(FileUpload1.PostedFile.FileName);
        //      //  if (FileUpload1.HasFile == true)
        //        {
        //        string fileExtension = Path.GetExtension(fileinfo.file.FileName);
        //        if (fileinfo.file.HasFile == true)
        //        {
        //            if (fileExtension.Trim().ToLower() == ".csv")
        //            {
        //                if (fileinfo.HasFile && FileUpload1.PostedFile.ContentLength > 0)
        //                {
        //                    if (!string.IsNullOrEmpty(txt_remarks.InnerText.Trim()))
        //                    {
        //                        Message = MessageText("");
                               


        //                        string AggrCode = string.Empty;
        //                        string AggrName = string.Empty;


        //                        AggrCode = AggrName = "Rewards";

        //                        string fileName = (AggrName + "_" + objSessionBo.Entity_Code + "_" + DateTime.Now.Ticks.ToString());

        //                        string fileLocation = Startup.Configuration["AppSettings:FileUploadPath"].ToString() + fileName + fileExtension;

        //                        FileUpload1.SaveAs(fileLocation);

        //                        OnlinePaymentReconciliation_RW_DAL objDAL = new OnlinePaymentReconciliation_RW_DAL();
        //                        string ErrorMessage = string.Empty;
        //                        DataTable dt = TransferXLToTable(fileLocation, ref ErrorMessage);

        //                        if (dt.Rows.Count > 0 && dt != null)
        //                        {
        //                            if (!string.IsNullOrEmpty(ErrorMessage))
        //                            {
        //                                Error = ErrorMessage;
        //                                return;
        //                            }
        //                            DataTable OrignalFileUploaddt = dt;
        //                            OrignalFileUploaddt.AcceptChanges();

        //                            //Count No. of Columns in Excel File

        //                            if (dt.Columns.Count == 11)
        //                            {

        //                                DataTable DuplicateDt = new DataTable();

        //                                //Inserting Request Hdr
        //                                string hdrSeq = objDAL.SAVE_REQUEST_QUEUE_HDR(AggrCode, AggrName, fileLocation, fileName + fileExtension, SBO, txt_remarks.InnerText.Trim());

        //                                string dbResponse = string.Empty;

        //                                // Save All columns data to the upload table to have the Back of file Uploaded.
        //                                objDAL.SaveOriginalFileUpload(OrignalFileUploaddt, hdrSeq, objSessionBo, ref dbResponse);


        //                                dt = CheckDuplicateRecord(dt, ref DuplicateDt);
        //                                //dt = CheckData(dt, ref DuplicateDt);


        //                                //if (dt != null && dt.Rows.Count > 0)
        //                                //{

        //                                //Save Data in Details table and Fetch Output Data 
        //                                DataTable dt1 = objDAL.SaveDetails(dt, hdrSeq, objSessionBo, ref dbResponse, DuplicateDt);

        //                                if (dt1.Rows.Count > 0 && dt1 != null)
        //                                {
        //                                    string DatetimeNow = DateTime.Now.Ticks.ToString();
        //                                    string OfileName = Startup.Configuration["AppSettings:FileDownloadPath"].ToString() + ("TPSL_Output_" + objSessionBo.Entity_Code) + "_" + hdrSeq + "_" + DatetimeNow + ".xls";
        //                                    //Create Excel
        //                                    CreateexcelFileforDownload(dt1, OfileName);

        //                                    //Update Output Filename in Hdr Table
        //                                    string Outfilename = ("TPSL_Output_" + objSessionBo.Entity_Code) + "_" + hdrSeq + "_" + DatetimeNow + ".xls";
        //                                    UpdateStatus(hdrSeq, Outfilename, dt1.Rows.Count);
        //                                }
        //                                if (!string.IsNullOrEmpty(dbResponse))
        //                                {
        //                                    Message= (dbResponse);
        //                                 //   txt_remarks.InnerText = "";
        //                                }
        //                                //}

        //                            }
        //                            else Error = "Please Upload Excel with all the columns.";
        //                        }
        //                        else
        //                        {
        //                            Error = "Something went wrong when Converting CSV file to DataTable";
        //                        }
        //                    }
        //                    else
        //                    {
        //                        Error = "Please enter Remarks before uploading file.";
        //                       // txt_remarks.Focus();
        //                        return;
        //                    }
        //                }
        //                else
        //                {
        //                    Error = "Please select file with length greater than Zero.";
        //                }
        //            }
        //            else
        //            {
        //                Error = "Please Check fileformat. File Format should be in .csv extension";
        //                return;
        //            }

        //        }
        //        else
        //        {
        //            Error = "Please Select File to Upload.";
        //            return;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //       // ExceptionUtility.LogException(ex, ex.Source);
        //    }
        //}

       [HttpPost]
        public JsonResult Upload_File()
        {
            string Message = string.Empty;
            string result = string.Empty;
            string data = string.Empty;
            try
            {
                SessionBO objSessionBo = HttpContext.Session.GetObject<SessionBO>("UserSessionDetails");
                var file = Request.Form.Files;
                //  string fileName = (AggrName + "_" + objSessionBo.Entity_Id + "_" + DateTime.Now.Ticks.ToString());
                var filenm = ContentDispositionHeaderValue.Parse(file[0].ContentDisposition).FileName.Trim('"');
                if (file.Count > 0)
                {
                   
                    string AggrCode = string.Empty;
                    string AggrName = string.Empty;
                    AggrCode = AggrName = "Rewards";
                    // string fileName = ("Reco_" + objSessionBo.Entity_Id + DateTime.Now.Ticks.ToString());
                    string fileName = (AggrName + "_" + objSessionBo.Entity_Id + "_" + DateTime.Now.Ticks.ToString());
                    string[] ext = filenm.Split('.');
                    //string fileName = ("Reco_" + DateTime.Now.Ticks.ToString());
                    string FilePath = Startup.Configuration["AppSettings:RecoUploadPath"].ToString() + fileName + "." + ext[1].ToLower();/* ".xlsx"*/;

                    using (FileStream fs = System.IO.File.Create(FilePath))
                    {
                        file[0].CopyTo(fs);
                        fs.Flush();
                    }

                    OnlinePaymentReconciliation_RW_DAL objDAL = new OnlinePaymentReconciliation_RW_DAL();
                    string ErrorMessage = string.Empty;
                    string txt_remarks = string.Empty;
                    DataTable dt = TransferXLToTable(FilePath, ref ErrorMessage);
                    DataTable OrignalFileUploaddt = dt;
                    OrignalFileUploaddt.AcceptChanges();

                    if (dt.Columns.Count == 11)
                    {
                        string dbResponse = string.Empty;
                        DataTable DuplicateDt = new DataTable();
                        string hdrSeq = objDAL.SAVE_REQUEST_QUEUE_HDR(AggrCode, AggrName, FilePath, fileName + ext, objSessionBo, txt_remarks);
                        objDAL.SaveOriginalFileUpload(OrignalFileUploaddt, hdrSeq, objSessionBo, ref dbResponse);

                        dt = CheckDuplicateRecord(dt, ref DuplicateDt);
                        //return Json(new { Status = "1", Message = "Sucess", Data = filename});
                        //result = "Success";
                        //data =filename;
                        DataTable dt1 = objDAL.SaveDetails(dt, hdrSeq, objSessionBo, ref dbResponse, DuplicateDt);
                        if (dt1.Rows.Count > 0 && dt1 != null)
                        {
                           string DatetimeNow = DateTime.Now.Ticks.ToString();
                           var filename  = ("TPSL_Output_" + objSessionBo.Entity_Id) + "_" + hdrSeq + "_" + DatetimeNow + ".xls";
                           string OfileName = Startup.Configuration["AppSettings:RecoUploadPath"].ToString() + ("TPSL_Output_" + objSessionBo.Entity_Id) + "_" + hdrSeq + "_" + DatetimeNow + ".xls";
                            //Create Excel
                            // CreateexcelFileforDownload(dt1, OfileName);
                            Export2Excel.SaveDataTableToExcel(dt, filename,OfileName);

                            //Update Output Filename in Hdr Table
                            string Outfilename = ("TPSL_Output_" + objSessionBo.Entity_Id) + "_" + hdrSeq + "_" + DatetimeNow + ".xls";
                            UpdateStatus(hdrSeq, Outfilename, dt1.Rows.Count);
                        }
                        //else { return Json(new { Status = "1", Message = "Sucess", Data="Count is not null" });}

                        if (!string.IsNullOrEmpty(dbResponse))
                        {
                            return Json(new { Status = "1", Message = "Sucess", Data = dbResponse, Filename = fileName + "." + ext[1].ToLower() });
                        }
                        else
                        {
                            return Json(new { Status = "0", Message = "Error", Data = "Records not found." });


                        }

                    }
                    else
                    {
                        return Json(new { Status = "0", Message = "Error", Data = "Please Upload Excel with all the columns." });


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

        public ActionResult Upload_File1()
        {
            string result = string.Empty;
            string data = string.Empty;
            try
            {
                SessionBO objSessionBo = HttpContext.Session.GetObject<SessionBO>("UserSessionDetails");
                var file = Request.Form.Files;
                var filename = ContentDispositionHeaderValue.Parse(file[0].ContentDisposition).FileName.Trim('"');
                string[] ext = filename.Split('.');

                if (file.Count > 0)
                {
                    string AggrCode = string.Empty;
                    string AggrName = string.Empty;


                    AggrCode = AggrName = "Rewards";


                    string fileName = (AggrName + "_" + objSessionBo.Entity_Id + "_" + DateTime.Now.Ticks.ToString());

                    //string fileName = ("Reco_" + DateTime.Now.Ticks.ToString());
                    string fileLocation = Startup.Configuration["AppSettings:RecoUploadPath"].ToString() + fileName + "." + ext[1].ToLower();/* ".xlsx"*/;

                    using (FileStream fs = System.IO.File.Create(fileLocation))
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
                ExceptionUtility.LogExceptionAsync(ex);
                return Json(new { Status = "0", Message = "Error", Data = "Something went wrong !" });
            }
            //return data ;

        }

        public DataTable TransferXLToTable(string fileLocation, ref string message)
        {
            try
            {
                DataTable dtCsv = new DataTable();

                string[] arr = new string[11];
                arr[0] = "SRNO";
                arr[1] = "BANKID";
                arr[2] = "BANKNAME";
                arr[3] = "TRANSID";
                arr[4] = "SMTXNID";
                arr[5] = "TRANAMT";
                arr[6] = "TXNDATE";
                arr[7] = "TXNTIME";
                arr[8] = "PAYMENTDATE";
                arr[9] = "SAPCODE";
                arr[10] = "EMPNAME";
                string Fulltext;
                using (StreamReader sr = new StreamReader(fileLocation))
                {
                    while (!sr.EndOfStream)
                    {
                        Fulltext = sr.ReadToEnd().ToString(); //read full file text  
                        string[] rows = Fulltext.Split('\n'); //split full file text into rows  
                        for (int i = 0; i < rows.Count() - 1; i++)
                        {
                            string[] rowValues = rows[i].Split(','); //split each row with comma to get individual values  
                            {
                                if (rowValues.Count() == 11)
                                {

                                    if (i == 0)
                                    {
                                        for (int j = 0; j < rowValues.Count(); j++)
                                        {
                                            if (Convert.ToString(arr[j]).Trim().ToUpper() != Convert.ToString(rowValues[j]).Trim().ToUpper().Replace("/R", ""))
                                            {
                                                message = "Column Name did Not Match " + rowValues[j] + " .";
                                                break;
                                            }
                                            dtCsv.Columns.Add(rowValues[j]); //add headers  
                                        }
                                    }
                                    else
                                    {
                                        DataRow dr = dtCsv.NewRow();

                                        //for (int k = 0; k < rowValues.Count(); k++)
                                        //foreach (string[] item in rowValues)
                                        {

                                            if (!string.IsNullOrEmpty(rowValues[0]))
                                            {
                                                dr[0] = Convert.ToInt32(Convert.ToString(rowValues[0]));
                                                dr[1] = Convert.ToInt32(Convert.ToString(rowValues[1]));
                                                dr[2] = Convert.ToString(rowValues[2]).Trim();
                                                dr[3] = Convert.ToString(rowValues[3]).Trim();
                                                dr[4] = Convert.ToString(rowValues[4]).Trim();
                                                dr[5] = Convert.ToInt32(Convert.ToString(rowValues[5]).Replace("\r", ""));

                                                dr[6] = Convert.ToString(rowValues[6]).Trim();
                                                dr[7] = Convert.ToString(rowValues[7]).Trim();
                                                dr[8] = Convert.ToString(rowValues[8]).Trim();
                                                dr[9] = Convert.ToString(rowValues[9]).Trim();
                                                dr[10] = Convert.ToString(rowValues[10]).Trim();


                                                dtCsv.Rows.Add(dr);
                                                dr = dtCsv.NewRow();
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    message = "No. of Columns to upload file should be 14 and should be in the given format.";
                                }
                            }
                        }
                    }
                }
                return dtCsv;
            }

            catch (Exception ex)
            {
                throw ex;
              //  ErrorText(ex.Message);
               // ExceptionUtility.LogException(ex, ex.Source);
               // return null;
            }
        }

        private DataTable CheckData(DataTable Dt, ref DataTable DuplicateDt)
        {
            try
            {
                for (int i = 0; i < Dt.Rows.Count; i++)
                {
                    // To Check Transaction Date Column in CSV file to empty
                    if (!string.IsNullOrEmpty(Convert.ToString(Dt.Rows[i][9])))
                    {
                        try
                        {
                            string[] ExcelValue = Dt.Rows[i][8].ToString().Split('-');

                            DateTime ActualDate = Convert.ToDateTime(ExcelValue[2] + "/" + ExcelValue[1] + "/" + ExcelValue[0]);

                            string date = (ActualDate).ToString("dd/MM/yyyy");

                            if (!ValidateDate(date))
                            {
                                DuplicateDt.Rows.Add(Dt.Rows[i].ItemArray);
                                Dt.Rows[i].Delete();

                                if (DuplicateDt.Rows.Count > 0 && DuplicateDt != null)
                                {
                                    foreach (DataRow row in DuplicateDt.Rows)
                                    {
                                        if (string.IsNullOrEmpty(Convert.ToString(row["Remarks"])))
                                        {
                                            row["Remarks"] = "Transaction Date is Not in the correct format.";
                                        }
                                    }
                                }
                                continue;
                            }
                        }
                        catch (Exception ex)
                        {

                        }

                    }
                    else
                    {
                        DuplicateDt.Rows.Add(Dt.Rows[i].ItemArray);
                        Dt.Rows[i].Delete();

                        if (DuplicateDt.Rows.Count > 0 && DuplicateDt != null)
                        {
                            foreach (DataRow row in DuplicateDt.Rows)
                            {
                                if (string.IsNullOrEmpty(Convert.ToString(row["Remarks"])))
                                {
                                    row["Remarks"] = "Transaction Date can not be empty.";
                                }
                            }
                        }
                        continue;
                    }

                    // To Check Transaction Date Column in CSV file to empty
                    if (!string.IsNullOrEmpty(Convert.ToString(Dt.Rows[i][11])))
                    {
                        try
                        {
                            string[] ExcelValue = Dt.Rows[i][11].ToString().Split('-');

                            DateTime ActualDate = Convert.ToDateTime(ExcelValue[2] + "/" + ExcelValue[1] + "/" + ExcelValue[0]);

                            string date = (ActualDate).ToString("dd/MM/yyyy");

                            if (!ValidateDate(date))
                            {
                                DuplicateDt.Rows.Add(Dt.Rows[i].ItemArray);
                                Dt.Rows[i].Delete();

                                if (DuplicateDt.Rows.Count > 0 && DuplicateDt != null)
                                {
                                    foreach (DataRow row in DuplicateDt.Rows)
                                    {
                                        if (string.IsNullOrEmpty(Convert.ToString(row["Remarks"])))
                                        {
                                            row["Remarks"] = "Payment Date is not in the correct format.";
                                        }
                                    }
                                }
                                continue;
                            }
                        }
                        catch (Exception ex)
                        {

                        }

                    }
                    else
                    {
                        DuplicateDt.Rows.Add(Dt.Rows[i].ItemArray);
                        Dt.Rows[i].Delete();

                        if (DuplicateDt.Rows.Count > 0 && DuplicateDt != null)
                        {
                            foreach (DataRow row in DuplicateDt.Rows)
                            {
                                if (string.IsNullOrEmpty(Convert.ToString(row["Remarks"])))
                                {
                                    row["Remarks"] = "Payment Date can not be empty.";
                                }
                            }
                        }
                        continue;
                    }

                    // To check TRANSID Column in Excel or Csv file is empty
                    if (string.IsNullOrEmpty(Convert.ToString(Dt.Rows[i][3])))
                    {
                        DuplicateDt.Rows.Add(Dt.Rows[i].ItemArray);
                        Dt.Rows[i].Delete();

                        if (DuplicateDt.Rows.Count > 0 && DuplicateDt != null)
                        {
                            foreach (DataRow row in DuplicateDt.Rows)
                            {
                                if (string.IsNullOrEmpty(Convert.ToString(row["Remarks"])))
                                {
                                    row["Remarks"] = "TRANSID Column is Empty.";
                                }
                            }
                        }
                        continue;
                    }

                    // To check SMTXNID (Appl No.) Column in Excel or Csv file is empty
                    if (string.IsNullOrEmpty(Convert.ToString(Dt.Rows[i][4])))
                    {
                        DuplicateDt.Rows.Add(Dt.Rows[i].ItemArray);
                        Dt.Rows[i].Delete();

                        if (DuplicateDt.Rows.Count > 0 && DuplicateDt != null)
                        {
                            foreach (DataRow row in DuplicateDt.Rows)
                            {
                                if (string.IsNullOrEmpty(Convert.ToString(row["Remarks"])))
                                {
                                    row["Remarks"] = "SMTXNID (Application No.) Column is Empty.";
                                }
                            }
                        }
                        continue;
                    }

                    // To check ACTUALTOTAMT (Trans Amount) Column in Excel or Csv file is empty
                    if (string.IsNullOrEmpty(Convert.ToString(Dt.Rows[i][5])))
                    {
                        DuplicateDt.Rows.Add(Dt.Rows[i].ItemArray);
                        Dt.Rows[i].Delete();

                        if (DuplicateDt.Rows.Count > 0 && DuplicateDt != null)
                        {
                            foreach (DataRow row in DuplicateDt.Rows)
                            {
                                if (string.IsNullOrEmpty(Convert.ToString(row["Remarks"])))
                                {
                                    row["Remarks"] = "TOTAMT (Trans Amount) Column is Empty.";
                                }
                            }
                        }
                        continue;
                    }

                    // To check Time Column in Excel or Csv file is empty
                    if (string.IsNullOrEmpty(Convert.ToString(Dt.Rows[i][7])))
                    {
                        DuplicateDt.Rows.Add(Dt.Rows[i].ItemArray);
                        Dt.Rows[i].Delete();

                        if (DuplicateDt.Rows.Count > 0 && DuplicateDt != null)
                        {
                            foreach (DataRow row in DuplicateDt.Rows)
                            {
                                if (string.IsNullOrEmpty(Convert.ToString(row["Remarks"])))
                                {
                                    row["Remarks"] = "Time Column is Empty.";
                                }
                            }
                        }
                        continue;
                    }
                }

                DuplicateDt.AcceptChanges();
                Dt.AcceptChanges();
                return Dt;
            }
            catch (Exception ex)
            {
                throw ex;
              //  ExceptionUtility.LogException(ex, ex.Source);
              //  return null;
            }
        }

        private DataTable CheckDuplicateRecord(DataTable Dt, ref DataTable DuplicateDt)
        {
            try
            {
                //DataTable dtCsv = new DataTable();
                DuplicateDt.Columns.Add("SRNO", typeof(int));
                DuplicateDt.Columns.Add("BANKID", typeof(int));
                DuplicateDt.Columns.Add("BANKNAME", typeof(string));
                DuplicateDt.Columns.Add("TRANSID", typeof(string));
                DuplicateDt.Columns.Add("SMTXNID", typeof(string));
                DuplicateDt.Columns.Add("TRANAMT", typeof(int));
                DuplicateDt.Columns.Add("TXNDATE", typeof(string));
                DuplicateDt.Columns.Add("TXNTIME", typeof(string));
                DuplicateDt.Columns.Add("PAYMENTDATE", typeof(string));
                DuplicateDt.Columns.Add("SAPCODE", typeof(string));
                DuplicateDt.Columns.Add("EMPNAME", typeof(string));
                DuplicateDt.Columns.Add("Remarks", typeof(string));
                DuplicateDt.AcceptChanges();

                ArrayList ApplNo = new ArrayList();
                ArrayList TranId = new ArrayList();
 

                for (int i = 0; i < Dt.Rows.Count; i++)
                {
                    string ApplicationNo = string.Empty;
                    string TransId = string.Empty;

                    ApplicationNo = Convert.ToString(Dt.Rows[i][4]);

                    if (!string.IsNullOrEmpty(ApplicationNo) && (ApplNo.Contains(Convert.ToString(ApplicationNo).Trim())))
                    {
                        int rowcount = 0;
                        if (DuplicateDt.Rows.Count > 0)
                        {
                            rowcount = DuplicateDt.Rows.Count;
                        }

                        DuplicateDt.Rows.Add((Dt.Rows[i]).ItemArray);

                        DuplicateDt.Rows[rowcount]["Remarks"] = "Duplicate Application No. Found.";

                        Dt.Rows[i].Delete();
                        Dt.AcceptChanges();
                        i--;
                        continue;
                    }
                    else
                    {
                        ApplNo.Add(ApplicationNo);
                    }

                    //TransId = Convert.ToString(Dt.Rows[i][3]);

                    //if (!string.IsNullOrEmpty(TransId) && (TranId.Contains(Convert.ToString(TransId).Trim())))
                    //{
                    //    int rowcount = 0;
                    //    if (DuplicateDt.Rows.Count > 0)
                    //    {
                    //        rowcount = DuplicateDt.Rows.Count;
                    //    }

                    //    DuplicateDt.Rows.Add((Dt.Rows[i]).ItemArray);

                    //    DuplicateDt.Rows[rowcount]["Remarks"] = "Duplicate Trans Id Found.";
                    //    Dt.Rows[i].Delete();

                    //    i--;
                    //    Dt.AcceptChanges();
                    //    ApplNo.Remove(ApplicationNo);
                    //}
                    //else
                    //{
                    //    TranId.Add(TransId);
                    //}
                }

                return Dt;
            }
            catch (Exception ex)
            {
                throw ex;
              //  ExceptionUtility.LogException(ex, ex.Source);
              //  return null;
            }
        }

        private bool ValidateDate(string date)
        {
            try
            {
                string[] dateParts = date.Split('/');
                DateTime testDate = new DateTime(Convert.ToInt32(dateParts[2]), Convert.ToInt32(dateParts[1]), Convert.ToInt32(dateParts[0]));
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //public static void CreateexcelFileforDownload(DataTable dt, string fileName)
        //{
        //    try
        //    {
        //        using (FileStream stream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
        //        {
        //            IWorkbook wb = new HSSFWorkbook();
        //            ISheet sheet = wb.CreateSheet("Sheet1");
        //            ICreationHelper cH = wb.GetCreationHelper();
        //            IRow row1 = sheet.CreateRow(0);

        //            //IFont font = wb.CreateFont();
        //            ////font.FontHeightInPoints = 11;
        //            ////font.FontName = "Calibri";
        //            //font.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Bold;

        //            for (int j = 0; j < dt.Columns.Count; j++)
        //            {

        //                ICell cell = row1.CreateCell(j);
        //                String columnName = dt.Columns[j].ToString();
        //                cell.SetCellValue(columnName);
        //                //cell.CellStyle.SetFont(font);
        //            }

        //            //loops through data 
        //            for (int i = 0; i < dt.Rows.Count; i++)
        //            {
        //                IRow row = sheet.CreateRow(i + 1);
        //                for (int j = 0; j < dt.Columns.Count; j++)
        //                {

        //                    ICell cell = row.CreateCell(j);
        //                    String columnName = dt.Columns[j].ToString();
        //                    cell.SetCellValue(dt.Rows[i][columnName].ToString());
        //                    cell.CellStyle.DataFormat = HSSFDataFormat.GetBuiltinFormat("@");

        //                }
        //            }
        //            wb.Write(stream);

        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public void UpdateStatus(string hdr, string Filename, int SucessRowCount)
        {
            try
            {
                string strerr = string.Empty;
                string FilePath = Startup.Configuration["AppSettings:RecoUploadPath"].ToString() + Filename;/* + "." + ext[1].ToLower();*/
                //  string FilePath = ConfigurationManager.AppSettings["FileDownloadPath"].ToString() + Filename;
                OnlinePaymentReconciliation_RW_DAL objDAL = new OnlinePaymentReconciliation_RW_DAL();
                objDAL.UpdateDownloadFilePath(hdr, FilePath, Filename, SucessRowCount, ref strerr);
            }
            catch (Exception ex)
            {
                throw ex;
              //  ExceptionUtility.LogException(ex, ex.Source);
             //   ErrorText(ex.Message.ToString());
            }

        }




        
            //  return res;
    }


}
